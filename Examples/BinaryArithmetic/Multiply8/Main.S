;==============================================================================
; Name:        8-Bit Shift-and-Add Multiplication
; Course:      RCET 3371
; Device:      PIC16F883
; Toolchain:   MPLAB X + pic-as
; Linker Options: -Wl,-presetVect=0000h,-pcode=0008h
;==============================================================================
;
; Unsigned multiplication using the C:A:Q register algorithm.
;
; Input:
;   multiplicand = M, 8-bit multiplicand
;   product_low  = Q, 8-bit multiplier
;
; Output:
;   product_high:product_low = 16-bit product
;
; The example setup loads 13 * 11. Expected result: 0x008F.
;==============================================================================

RADIX dec
PROCESSOR 16F883

#include <xc.inc>

;------------------------------------------------------------------------------
; Device Configuration
;------------------------------------------------------------------------------
CONFIG "FOSC = XT"
CONFIG "WDTE = OFF"
CONFIG "PWRTE = OFF"
CONFIG "MCLRE = ON"
CONFIG "CP = OFF"
CONFIG "CPD = OFF"
CONFIG "BOREN = OFF"
CONFIG "IESO = OFF"
CONFIG "FCMEN = OFF"
CONFIG "LVP = OFF"
CONFIG "BOR4V = BOR40V"
CONFIG "WRT = OFF"

;------------------------------------------------------------------------------
; Shared RAM
;------------------------------------------------------------------------------
PSECT udata_shr

multiplicand:
    DS 1

product_high:
    DS 1

product_low:
    DS 1

loop_count:
    DS 1

;------------------------------------------------------------------------------
; Reset Vector
;------------------------------------------------------------------------------
PSECT resetVect,class=CODE,delta=2

ResetVector:
    goto    Setup

;------------------------------------------------------------------------------
; Main Code
;------------------------------------------------------------------------------
PSECT code,class=CODE,delta=2

Setup:
    ; Default simulator test: 13 * 11 = 143 = 0x008F
    movlw   13
    movwf   multiplicand

    movlw   11
    movwf   product_low

    call    Multiply8

Stop:
    goto    Stop

;------------------------------------------------------------------------------
; Multiply8
; Input:       multiplicand = M, product_low = Q
; Output:      product_high:product_low = 16-bit product
; Uses:        WREG, STATUS.C, loop_count
; Stack Depth: 1
;
; Algorithm:
;   A = 0
;   repeat 8 times:
;       if Q0 = 1:
;           A = A + M, preserving carry in C
;       else:
;           C = 0
;       shift C:A:Q right
;------------------------------------------------------------------------------
Multiply8:
    clrf    product_high

    movlw   8
    movwf   loop_count

multiplyLoop:
    btfss   product_low,0
    goto    noAdd

    movf    multiplicand,w
    addwf   product_high,f
    goto    shiftProduct

noAdd:
    bcf     C

shiftProduct:
    rrf     product_high,f
    rrf     product_low,f

    decfsz  loop_count,f
    goto    multiplyLoop

    return

END
