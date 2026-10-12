# Assignment: Better Etch-O-Sketch (PIC Edition)

## Objective
Create a Windows Forms application that simulates an Etch-A-Sketch toy, integrating your PIC microcontroller knowledge. The program should allow drawing on a picture box using either mouse or external controls (potentiometers via PIC). Implement all required UI features and drawing modes.

---

## Requirements

### 1. Drawing Controls
- Use potentiometers (via PIC microcontroller) to control X and Y movement for drawing.
- Provide radio buttons to select the draw mode: Mouse or External (PIC). Only one mode can be active at a time; the modes are mutually exclusive.
- Drawing occurs only within the DisplayPictureBox.

### 2. User Interface
- DisplayPictureBox for drawing.
- Buttons: Select Color, Draw Waveforms, Clear, Exit.
- Top menu and context menu:
  - File: Exit
  - Edit: Select Color, Draw Waveforms, Clear
  - Help: About
- Tooltips for all buttons and the picture box.
- Proper tab order and access keys for all controls.
- DrawWaveformsButton as the accept button (Enter).
- Clear as the cancel button (Esc).

### 3. Drawing and Interaction
- Drawing occurs when the left mouse button is held and moved over the picture box (mouse mode).
- In external mode, drawing is controlled by PIC potentiometer input.
- Color dialog appears for color selection via:
  - Select Color button
  - Select Color menu/context menu item
  - Mouse middle click on the picture box
- Clear Button visually "shakes" the drawing area and plays the provided shaker sound from the Resources folder when clicked (simulating an Etch-A-Sketch erase).
- DrawWaveformsButton:
  - Erases the picture box
  - Draws a 10x10 scope graticule
  - Draws sine, cosine, and tangent waves in different colors, filling the box one cycle wide

### 4. Code Quality
- Use meaningful names for all controls and variables.
- Organize code with comments and proper indentation.
- Handle errors gracefully (e.g., PIC connection issues, especially COM port connectivity problems).
- All specified color selection triggers (Select Color button, menu/context menu item, and mouse middle click) must be implemented and functional.

---

