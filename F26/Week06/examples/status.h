#ifndef STATUS_H
#define STATUS_H

#include <stdbool.h>
#include <stdint.h>

typedef struct
{
    bool fault;
    bool enabled;
    uint8_t mode;
    uint8_t level;
} status_word_t;

status_word_t status_decode(uint8_t value);

#endif
