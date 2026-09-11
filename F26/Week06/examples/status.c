#include "status.h"

status_word_t status_decode(uint8_t value)
{
    status_word_t result;

    result.fault = (value & 0x80u) != 0u;
    result.enabled = (value & 0x40u) != 0u;
    result.mode = (uint8_t)((value >> 3u) & 0x07u);
    result.level = (uint8_t)(value & 0x07u);

    return result;
}
