#include <xc.h>
#include <stdint.h>

#include "status.h"

void main(void)
{
    ANSEL = 0x00u;
    ANSELH = 0x00u;
    TRISB = 0xFFu;
    TRISC = 0x00u;

    while (1)
    {
        uint8_t raw_status = PORTB;
        status_word_t status = status_decode(raw_status);

        PORTC = (uint8_t)(
            (status.fault ? 0x80u : 0x00u) |
            (status.enabled ? 0x40u : 0x00u) |
            ((status.mode & 0x07u) << 3u) |
            (status.level & 0x07u));
    }
}
