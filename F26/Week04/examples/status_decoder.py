from dataclasses import dataclass


@dataclass(frozen=True, slots=True)
class StatusWord:
    fault: bool
    enabled: bool
    mode: int
    level: int


def decode_status(value: int) -> StatusWord:
    if not 0 <= value <= 0xFF:
        raise ValueError("status byte must be in the range 0..255")

    return StatusWord(
        fault=bool(value & 0x80),
        enabled=bool(value & 0x40),
        mode=(value >> 3) & 0x07,
        level=value & 0x07,
    )


if __name__ == "__main__":
    for value in (0x00, 0xFF, 0x48, 0xB5):
        status = decode_status(value)
        print(
            f"0x{value:02X} fault={status.fault} enabled={status.enabled} "
            f"mode={status.mode} level={status.level}"
        )
