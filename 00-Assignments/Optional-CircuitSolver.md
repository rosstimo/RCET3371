# Assignment: AC Series–Parallel Circuit Solver (UI Edition)

## Objective
Build a Windows Forms program that lets the user configure and solve the given AC circuit using **graphical controls** (combo boxes, text boxes, sliders, etc.). The program will display voltages, currents, impedances, and power in either **polar** or **rectangular** form.

---


<p align="center">
  <img src="../resources/AC-SeriesParallel.png" alt="AC Circuit" style="width:60%; max-width:60%;">
  <br>
  <em>Figure 1: AC Series–Parallel Circuit to be solved by your program. You can find the image in the resources folder.</em>
</p>


## User Interface Requirements

### 1. Source Voltage
- Use a **slider (track bar)** to set generator voltage `Vgen`.
- Range: **0–10 Vp** (volts peak).
- Display the current value next to the slider.

### 2. Source Frequency
- Input with a **text box**.
- Must be a **whole number only** (integer).
- Allowed range: **1 Hz to 1,000,000 Hz (1 MHz)**.
- Validate input—reject values outside this range.

### 3. Source Resistance
- Use a **combo box** labeled `Rgen`.
- Choices: **50 Ω** or **400 Ω** only.

### 4. R1, C1, C2, and L1 values
- Each component value is selected using:
  - **Combo box** for *standard/common values* (e.g., 1, 2.2, 4.7, 10, 22, 47, 100).
  - **Combo box** for **metric prefixes**:  
    - **p** (pico, 10⁻¹²)  
    - **n** (nano, 10⁻⁹)  
    - **µ** (micro, 10⁻⁶)  
    - **m** (milli, 10⁻³)  
    - **(unit)** (no prefix)  
    - **k** (kilo, 10³)  
    - **M** (mega, 10⁶)
  - **Note:**
    - For inductance (L1), only use prefixes **µ** (micro) and **m** (milli).
    - For capacitance (C1, C2), only use prefixes **p** (pico) and **µ** (micro).
    - For resistance (R1), use only **(unit)** (no prefix), **k** (kilo), and **M** (mega).

- Example: `47` + `µ` → **47 µF**

### 5. Inductor Winding Resistance
- Use a **text box** for `Rwinding` of L1.
- Must accept whole numbers only.
- Range: **0–1000 Ω**.

### 6. Output Display
- Use a **radio button group** to toggle between:
  - **Rectangular form** (a + jb).
  - **Polar form** (magnitude ∠ angle°).

- Use a **list box** to display results.
- Each line should show one calculated value with appropriate units and formatting.
- Columns should be aligned for readability.
- All values should be in engineering notation (e.g., 1.00 kΩ, 47.0 µF).
- All values should be displayed 6 significant figures. (use full resolution in calculations, but round for display)
- Output should include:
  - Total impedance (Ztotal)
  - Source current (Igen)
  - Resistance of R1
  - Capacitance of C1 and C2
  - Inductance of L1
  - Reactance of C1, C2, and L1 (XC1, XC2, XL1)
  - Impedance of L1 (ZL1)
  - Impedance of the parallel combination of C2 and L1 (ZEQ)
  - Voltage across each component (VRgen, VR1, VC1, VC2, VL1)
  - Current through each component (IRgen, IR1, IC1, IC2, IL1)
  - Real power (P) Watts
  - Reactive power (Q) VARs
  - Apparent power (S) VA

- Use **proper electrical symbols**:
  - Ohm: `Ω` (`\u03A9`)  
  - Micro: `µ` (`\u00B5`)  
  - Angle: `∠` (`\u2220`)  
  - Degree: `°` (`\u00B0`)
  - j (imaginary unit)
  - Pi: `π` (`\u03C0`)
  - E (Euler's number): `e` (`\u0065`)
  - Infinity: `∞` (`\u221E`)  

In .NET and VB, use Unicode escape sequences in strings.
Example:
```vb
Dim ohms As String = ChrW(&H3A9) ' Ω
```
### 7. Controls
- Use a **"Calculate" button** to perform calculations and update the output display.
- Use a **"Clear" button** to reset all inputs and outputs to default values.
  - Default values:
    - Vgen: 5 Vp
    - Frequency: 1000 Hz
    - Rgen: 50 Ω
    - R1: 100 Ω
    - C1: 1 µF
    - C2: 1 µF
    - L1: 10 mH
    - Rwinding: 20 Ω
    - Output format: Rectangular form
- Use a **"Close" button** to exit the application.
