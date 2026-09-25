from dataclasses import dataclass
from pathlib import Path
import csv
import tempfile

@dataclass(frozen=True)
class Sample:
    sequence: int
    raw: int
    volts: float

samples = [
    Sample(1, 0, 0.0),
    Sample(2, 32768, 32768 * 5.0 / 65535.0),
    Sample(3, 65535, 5.0),
]

with tempfile.TemporaryDirectory() as tmp:
    path = Path(tmp) / "samples.csv"
    with path.open("w", newline="", encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(["sequence", "raw", "volts"])
        for sample in samples:
            writer.writerow([sample.sequence, sample.raw, repr(sample.volts)])

    restored = []
    with path.open(newline="", encoding="utf-8") as f:
        for row in csv.DictReader(f):
            restored.append(Sample(int(row["sequence"]), int(row["raw"]), float(row["volts"])))

    assert restored == samples
    print(f"round-trip ok: {len(restored)} records")
