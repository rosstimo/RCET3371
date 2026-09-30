;==============================================================================
; Name:        8-Bit Register Division
; Course:      RCET 3371
; Device:      PIC16F883
; Toolchain:   MPLAB X + pic-as
; Linker Options: -Wl,-presetVect=0000h,-pcode=0008h
;==============================================================================
;
; Unsigned division using the RCET X:A:Q register algorithm.
;
; Conceptual registers:
;   X = one's complement of divisor
;   A = high dividend / partial remainder / final remainder
;   Q = low dividend / developing quotient / final quotient
;
; Inputs:
;   a_register:q_register = 16-bit dividend
;   divisor               = 8-bit nonzero divisor
;
; Outputs on success:
;   q_register    = 8-bit quotient
;   a_register    = 8-bit remainder
;   divide_status = 0
;
; Error status:
;   divide_status = 1 : divide by zero
;   divide_status = 2 : quotient does not fit in 8 bits
;
; Default simulator test: 15 / 5 = 3 remainder 0.
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

DIVIDE_BY_ZERO     EQU 1
QUOTIENT_OVERFLOW EQU 2

;------------------------------------------------------------------------------
; Shared RAM
;------------------------------------------------------------------------------
PSECT udata_shr

divisor:
    DS 1

x_register:
    DS 1

a_register:
    DS 1

q_register:
    DS 1

shifted_a:
    DS 1

shift_carry:
    DS 1

trial_a:
    DS 1

loop_count:
    DS 1

divide_status:
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
    ; Default simulator test:
    ; A:Q = 0x000F = 15
    ; divisor = 5
    ; expected Q = 3, A = 0, divide_status = 0
    clrf    a_register

    movlw   15
    movwf   q_register

    movlw   5
    movwf   divisor

    call    Divide8

Stop:
    goto    Stop

;------------------------------------------------------------------------------
; Divide8
; Input:       A:Q = dividend, divisor = divisor
; Output:      Q = quotient, A = remainder
; Status:      0 success, 1 divide by zero, 2 quotient overflow
; Uses:        WREG, STATUS.C/Z, X, shifted_a, shift_carry, trial_a,
;              loop_count
; Stack Depth: 1
;
; Algorithm:
;   X = NOT divisor
;   reject divisor = 0
;   reject A >= divisor because quotient would need > 8 bits
;   repeat 8 times:
;       shift A:Q left, shifting zero into Q0; save shift carry C1
;       save shifted A
;       A = A + X; save ADD carry C2
;       success C3 when C1=1, C2=1, or A=0xFF
;       if C3=0: restore shifted A
;       if C3=1: increment A to complete subtraction,
;                increment Q to set quotient bit
;------------------------------------------------------------------------------
Divide8:
    clrf    divide_status

    ; Divisor zero is never a valid trial.
    movf    divisor,w
    btfsc   STATUS,Z
    goto    divideByZero

    ; X is the one's complement of the divisor.
    comf    divisor,w
    movwf   x_register

    ; Initial register-size test.
    ; Quotient fits in 8 bits only when initial A < divisor.
    ; A + NOT(divisor) carries when A > divisor.
    ; A + NOT(divisor) = 0xFF when A == divisor.
    movf    a_register,w
    movwf   trial_a

    movf    x_register,w
    addwf   trial_a,f
    btfsc   STATUS,C
    goto    quotientOverflow

    movf    trial_a,w
    xorlw   0xFF
    btfsc   STATUS,Z
    goto    quotientOverflow

    movlw   8
    movwf   loop_count

divideLoop:
    ; Shift A:Q left as one register, always shifting 0 into Q0.
    ; After the second RLF, STATUS.C contains C1 = old A7.
    bcf     STATUS,C
    rlf     q_register,f
    rlf     a_register,f

    bcf     shift_carry,0
    btfsc   STATUS,C
    bsf     shift_carry,0

    ; Save the shifted remainder so a failed trial can restore it.
    movf    a_register,w
    movwf   shifted_a

    ; Trial subtraction stops one step short:
    ; A + X = A + NOT(divisor)
    movf    x_register,w
    addwf   a_register,f

    ; C2 = 1 means the trial succeeded.
    btfsc   STATUS,C
    goto    successfulTrial

    ; Exact equality produces all ones before the final +1.
    movf    a_register,w
    xorlw   0xFF
    btfsc   STATUS,Z
    goto    successfulTrial

    ; C1 = 1 also means the shifted partial dividend was large enough.
    btfsc   shift_carry,0
    goto    successfulTrial

failedTrial:
    ; Restore the shifted A. Q0 remains 0.
    movf    shifted_a,w
    movwf   a_register
    goto    nextIteration

successfulTrial:
    ; Complete A + NOT(divisor) + 1.
    incf    a_register,f

    ; Q0 is guaranteed to be 0 after the shift. Incrementing Q sets it to 1.
    incf    q_register,f

nextIteration:
    decfsz  loop_count,f
    goto    divideLoop

    clrf    divide_status
    return

divideByZero:
    movlw   DIVIDE_BY_ZERO
    movwf   divide_status
    return

quotientOverflow:
    movlw   QUOTIENT_OVERFLOW
    movwf   divide_status
    return

END
