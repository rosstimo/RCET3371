# RCET 3371 Final Program: HVAC Smart Home Controller

## Overview

Build a GUI to control various aspects of an HVAC smart home controller. The interface must communicate with the QY@ board to read sensor data and control devices.  
Refer to the schematic PDF for hardware wiring and IO details.

---

## Requirements

### 1. GUI Design

- Use the ISU color palette for backgrounds and controls (see [ISU Color Palette Values](#isu-color-palette-values) below).
- Use Segoe UI Bold font.
- Display the ISU Robotics Logo.

### 2. Controls & Indicators

- Heating, Cooling, and Fan controls are set/disabled via physical switches connected to the QY@ board (see schematic PDF for wiring).
- GUI controls mirror physical switch functionality.
- GUI provides operational and diagnostic data.
- GUI indicators must visually match the state of the corresponding IO (input/output) indicators (see schematic PDF for IO mapping).
- GUI Display:
  - Time of day clock (HH:MM AM/PM)
  - Current temperature
  - Current temperature setpoint
  - Indicators for enabled mode heating/cooling/off
  - Fan Mode On/Auto
  - Fan On/Off status
  - Fault indicator
  - Communication status with QY@ board

---

## Example GUI Interface

<div style="display: flex; gap: 20px; flex-wrap: wrap;">
  <img src="https://www.micro-air.com/images/products/branding/easytouch_rv/EasyTouch_RV_Thermostat.jpg" alt="Example HVAC GUI 1" width="300"/>
  <img src="https://www.bobvila.com/wp-content/uploads/2019/02/Smart_Thermostat.jpg?quality=85" alt="Example HVAC GUI 2" width="300"/>
  <img src="https://d36aiwq7h8e0h3.cloudfront.net/userfiles/inriver/images/sy/st/xc/systxccitc01-b_001_large.jpg" alt="Example HVAC GUI 3" width="300"/>
  <img src="https://cdn11.bigcommerce.com/s-fjw7wdq58d/images/stencil/1280x1280/products/275/1030/Honeywell-Home-Thermostat-1000x1000__40553.1655395344.386.513__13756.1656517970__49851__58075.1697479857.1280.1280__45904.1711660375.1280.1280__61714.1759443576.png?c=1" alt="Example HVAC GUI 4" width="300"/>
</div>

---

## ISU Color Palette Values

```vbnet
' ISU Color Palette
Public GrowlGreyLight As Color = Color.FromArgb(230, 231, 232)
Public GrowlGreyMed As Color = Color.FromArgb(167, 167, 167)
Public GrowlGrey As Color = Color.FromArgb(130, 130, 130)
Public Roarange As Color = Color.FromArgb(244, 121, 32)
Public RoarangeL As Color = Color.FromArgb(246, 146, 64)
Public BengalBlack As Color = Color.FromArgb(0, 0, 0)
```

### 3. Settings Management

- Save serial port and operational temperature settings in a text file named `HVAC Settings` in the project folder.
- Load previous session settings on startup.
- Toolstrip menu for:
  - Communication setup for QY@ Board
  - Exit program
  - Save settings

### 4. Temperature Setpoints

- Buttons to increase/decrease high (AC mode) and low (heating mode) setpoints in 0.5° increments.
- Setpoints must be in the range 50–90°F.
- Separate labels for high and low setpoints.
- Use a +2° hysteresis curve to determine heating/cooling activation.
- Fan must continue to run for 5 seconds after heating or cooling is disabled.

### 5. Sensor Inputs

- **Analog Input 1:** LM34 sensor for overall home temperature. (assume gain adjusted for 40–100°F range)
- **Analog Input 2:** LM34 sensor for heating/cooling unit air temperature. (assume gain adjusted for 40–100°F range)
  - ADC 1023 = 100°F 
- If heating/cooling is active, check every 30 seconds.
  - Verify system temperature is higher than room temp when heating, or cooler than room temp when cooling.
  - If verification fails, indicate error and request system maintenance.

### 6. Digital Inputs & Outputs

- **Digital Input 1:** Safety interlock switch.
  - If LOW, inhibit heating/cooling/fan operation and show warning.
  - Disable heating/cooling/fan controls until switch is HIGH.
  - Enable warning LED via Digital Output 1.
- **Digital Input 2:** Heating control.
  - If HIGH, enable fan (Digital Output 4) for 5 seconds, then engage heating element (Output 2).
- **Digital Input 3:** Fan-only mode.
  - Enable fan via Digital Output 3 as needed for all modes.
  - When in fan-only mode, the fan runs continuously regardless of temperature or mode.
- **Digital Input 4:** Differential pressure sensor.
  - Check after initial 5 seconds of fan operation and every 2 minutes when fan is enabled.
  - If verification fails, indicate error and request system maintenance.
  - Disable heating/cooling operation while pressure is LOW.
- **Digital Input 5:** Cooling control.
  - If HIGH, enable fan (Digital Output 4) for 5 seconds, then engage cooling element (Output 5).

### 7. Error Logging

- Log any system error/fault with detail and include time/date stamp YYmmDD-HHMMSS in a text file named `HVACSystem-YYmmDD.log` in the project folder.

Example log entries:

```
YYmmDD-HHMMSS: [Error Description]
251103-141530: Safety interlock switch LOW - Heating/Cooling disabled
251103-142000: Differential pressure sensor LOW - Heating/Cooling disabled
251103-143000: System temperature verification failed - Maintenance required
251103-144500: Communication lost with QY@ board - Attempting reconnect

```

### 8. Communication
- Iterate through all available COM ports to find the QY@ board.
- Check each available COM port to verify if it is connected to the QY@ board.
- Once QY@ board is confirmed, establish a serial communication link with it.
- Establish a serial communication link with the QY@ board automatically on startup.
- Continuously monitor communication status with the QY@ board.
- Display communication status indicator on the GUI.
- If communication is lost, indicate error and attempt to reconnect every 10 seconds.

### 9. User Experience

- All GUI controls should blend into the interface for a professional look.
- Program must run without errors or crashes.

