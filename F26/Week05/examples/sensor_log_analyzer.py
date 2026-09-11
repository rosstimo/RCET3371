from __future__ import annotations

import csv
from dataclasses import dataclass
from pathlib import Path
from statistics import fmean


@dataclass(frozen=True, slots=True)
class Sample:
    timestamp: str
    channel: str
    value: float
    status: int

    @property
    def fault(self) -> bool:
        return bool(self.status & 0x80)


def parse_status(text: str) -> int:
    value = int(text, 0)
    if not 0 <= value <= 0xFF:
        raise ValueError("status must be an 8-bit value")
    return value


def parse_row(row: dict[str, str]) -> Sample:
    channel = row["channel"].strip()
    if not channel:
        raise ValueError("channel is empty")

    return Sample(
        timestamp=row["timestamp"].strip(),
        channel=channel,
        value=float(row["value"]),
        status=parse_status(row["status"].strip()),
    )


def read_samples(path: Path) -> tuple[list[Sample], list[str]]:
    samples: list[Sample] = []
    errors: list[str] = []

    with path.open(newline="", encoding="utf-8") as source:
        reader = csv.DictReader(source)
        for line_number, row in enumerate(reader, start=2):
            try:
                samples.append(parse_row(row))
            except (KeyError, TypeError, ValueError) as error:
                errors.append(f"line {line_number}: {error}")

    return samples, errors


def summarize(samples: list[Sample]) -> dict[str, dict[str, float | int]]:
    by_channel: dict[str, list[Sample]] = {}
    for sample in samples:
        by_channel.setdefault(sample.channel, []).append(sample)

    result: dict[str, dict[str, float | int]] = {}
    for channel, group in by_channel.items():
        values = [sample.value for sample in group]
        result[channel] = {
            "count": len(group),
            "minimum": min(values),
            "maximum": max(values),
            "average": fmean(values),
            "faults": sum(sample.fault for sample in group),
        }
    return result


def main() -> None:
    samples, errors = read_samples(Path(__file__).with_name("sensor-data.csv"))

    print(f"valid records: {len(samples)}")
    print(f"invalid records: {len(errors)}")
    for error in errors:
        print(f"  {error}")

    for channel, stats in summarize(samples).items():
        print(f"\n{channel}")
        for name, value in stats.items():
            print(f"  {name}: {value}")


if __name__ == "__main__":
    main()
