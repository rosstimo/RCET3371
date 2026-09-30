MASK = 0xFF


def multiply8(x_register: int, q_register: int) -> int:
    """Model the 8-bit X/A/Q shift-and-add register algorithm."""
    x_register &= MASK
    q_register &= MASK
    a_register = 0
    carry = 0

    for _ in range(8):
        if q_register & 0x01:
            total = a_register + x_register
            a_register = total & MASK
            carry = 1 if total > MASK else 0
        else:
            carry = 0

        next_q = ((a_register & 0x01) << 7) | (q_register >> 1)
        a_register = ((carry & 0x01) << 7) | (a_register >> 1)
        q_register = next_q

    return (a_register << 8) | q_register


def divide8(a_register: int, q_register: int, divisor: int):
    """Model the RCET X:A:Q division algorithm.

    Returns (status, remainder_A, quotient_Q):
      0 = success
      1 = divide by zero
      2 = quotient overflow
    """
    a_register &= MASK
    q_register &= MASK
    divisor &= MASK

    if divisor == 0:
        return 1, a_register, q_register

    x_register = (~divisor) & MASK

    size_trial = a_register + x_register
    if size_trial > MASK or (size_trial & MASK) == MASK:
        return 2, a_register, q_register

    for _ in range(8):
        c1 = (a_register >> 7) & 1

        a_register = (
            ((a_register << 1) & MASK)
            | ((q_register >> 7) & 1)
        )
        q_register = (q_register << 1) & MASK

        shifted_a = a_register

        trial = a_register + x_register
        c2 = 1 if trial > MASK else 0
        a_register = trial & MASK

        c3 = bool(c1 or c2 or a_register == MASK)

        if c3:
            a_register = (a_register + 1) & MASK
            q_register = (q_register + 1) & MASK
        else:
            a_register = shifted_a

    return 0, a_register, q_register


def main() -> None:
    # Named vectors from the course guides.
    assert multiply8(5, 3) == 15
    assert multiply8(13, 11) == 143
    assert multiply8(128, 2) == 0x0100
    assert multiply8(255, 255) == 0xFE01

    # Every unsigned 8-bit multiplication pair.
    for x_register in range(256):
        for q_register in range(256):
            assert multiply8(x_register, q_register) == x_register * q_register

    # Named division vectors.
    assert divide8(0, 15, 5) == (0, 0, 3)
    assert divide8(0, 29, 8) == (0, 5, 3)
    assert divide8(0, 255, 13) == (0, 8, 19)
    assert divide8(0, 128, 3) == (0, 2, 42)
    assert divide8(0, 99, 0)[0] == 1
    assert divide8(5, 0, 5)[0] == 2

    # Every 8-bit dividend / nonzero 8-bit divisor pair.
    for dividend in range(256):
        for divisor in range(1, 256):
            status, remainder, quotient = divide8(0, dividend, divisor)
            assert status == 0
            assert quotient == dividend // divisor
            assert remainder == dividend % divisor

    # Representative full A:Q dividends for every legal A < divisor.
    for divisor in range(1, 256):
        for high in range(divisor):
            for low in (0x00, 0x01, 0x55, 0xAA, 0xFE, 0xFF):
                dividend = (high << 8) | low
                status, remainder, quotient = divide8(high, low, divisor)
                assert status == 0
                assert quotient == dividend // divisor
                assert remainder == dividend % divisor

    print("Binary arithmetic register algorithms verified.")


if __name__ == "__main__":
    main()
