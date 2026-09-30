RADIX dec
PROCESSOR 16F883

#include <xc.inc>

PSECT resetVect,class=CODE,delta=2
ResetVector:
    goto Main

PSECT code,class=CODE,delta=2
Main:
    movlw   2
    addlw   3
    goto    Main

END
