def get_mode(status: int) -> int:
    return (status >> 5) & 0x07

def get_fault(status: int) -> bool:
    return (status & 0x10) != 0

def get_sequence(status: int) -> int:
    return status & 0x0F

def set_sequence(status: int, sequence: int) -> int:
    if not 0 <= sequence <= 15:
        raise ValueError("sequence must be 0..15")
    return (status & 0xF0) | sequence

status = 0b01101100
print(f"mode={get_mode(status)} fault={int(get_fault(status))} sequence={get_sequence(status)}")
print(f"updated=0x{set_sequence(status, 2):02X}")
