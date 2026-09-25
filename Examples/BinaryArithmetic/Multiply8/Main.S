;==============================================================================
; Name:        8-Bit Shift-and-Add Multiplication
; Course:      RCET 3371
; Device:      PIC16F883
; Toolchain:   MPLAB X + pic-as
; Linker Options: -Wl,-presetVect=0000h,-pcode=0008h
;==============================================================================
;
; Unsigned multiplication using the C:X:A:Q teaching model.
;
; Conceptual registers:
;   X = multiplicand
;   A = accumulator / product high byte
;   Q = multiplier / product low byte
;   C = carry extension during the combined shift
;
; Input:
;   x_register = 8-bit multiplicand
;   q_register = 8-bit multiplier
;
; Output:
;   a_register:q_register = 16-bit product
;
; Default simulator test: 5 * 3 = 15 = 0x000F.
;==============================================================================

RADIX dec
PROCESSOR 16F883

#include <xc.inc>

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

PSECT udata_shr

x_register:
    DS 1

a_register:
    DS 1

q_register:
    DS 1

loop_count:
    DS 1

PSECT resetVect,class=CODE,delta=2

ResetVector:
    goto    Setup

PSECT code,class=CODE,delta=2

Setup:
    ; Same values as the four-bit hand example: 5 * 3 = 15.
    movlw   5
    movwf   x_register

    movlw   3
    movwf   q_register

    call    Multiply8

Stop:
    goto    Stop

;------------------------------------------------------------------------------
; Multiply8
; Input:       X = x_register, Q = q_register
; Output:      A:Q = a_register:q_register
; Uses:        WREG, STATUS.C, loop_count
; Stack Depth: 1
;
; Algorithm:
;   A = 0
;   repeat 8 times:
;       if Q0 = 1:
;           A = A + X, preserving carry in C
;       else:
;           C = 0
;       shift C:A:Q right
;------------------------------------------------------------------------------
Multiply8:
    clrf    a_register

    movlw   8
    movwf   loop_count

multiplyLoop:
    btfss   q_register,0
    goto    noAdd

    movf    x_register,w
    addwf   a_register,f
    goto    shiftProduct

noAdd:
    bcf     STATUS,C

shiftProduct:
    rrf     a_register,f
    rrf     q_register,f

    decfsz  loop_count,f
    goto    multiplyLoop

    return

END
