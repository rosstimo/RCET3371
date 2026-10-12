# Engineering Notation and Prefixes

[References index](README.md)

Engineering notation uses powers of ten in multiples of three.

| Prefix | Symbol | Factor |
| --- | --- | ---: |
| giga | G | 10^9 |
| mega | M | 10^6 |
| kilo | k | 10^3 |
| base |  | 10^0 |
| milli | m | 10^-3 |
| micro | µ / u in plain ASCII | 10^-6 |
| nano | n | 10^-9 |
| pico | p | 10^-12 |

Examples:

    0.0047 A = 4.7 mA
    4700 ohm = 4.7 kohm
    0.00000220 F = 2.20 uF

## Course rule

Keep internal calculations at sufficient precision. Apply engineering notation and significant-figure formatting at the presentation boundary unless the specification requires quantization earlier.

Do not confuse a display rounded to three significant figures with the internal stored value.
