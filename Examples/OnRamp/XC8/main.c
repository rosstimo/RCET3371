#include <xc.h>
#include <stdint.h>

static uint8_t add(uint8_t first, uint8_t second)
{
    return (uint8_t)(first + second);
}

void main(void)
{
    volatile uint8_t answer = add(2u, 3u);

    while (1)
    {
        (void)answer;
    }
}
