# Assignment: Dart Game Simulation (WinForms, C#)

## Objective
Create a **C# Windows Forms** application that simulates a dart game. The program will allow users to throw darts at a target by pressing the **Spacebar**, log each round to a **text file**, and review past rounds. This assignment reinforces skills in **event handling, graphics, file I/O, and UI design**.

---

## Requirements

### 1. Game Play
- The main form must include a **PictureBox** representing the dartboard.
- When the **Spacebar** is pressed, a “dart” is thrown:
  - Use a method (or function) to generate random **X, Y** coordinates **within the bounds** of the PictureBox.
  - Draw a dart at that position as a **circle with a center mark** using **System.Drawing.Graphics**.
  <!-- - Recommended approach: store dart positions in a list and draw them in the PictureBox **Paint** event (call `Invalidate()` after each throw). -->
- Each round consists of **three** dart throws.
- The program must allow the user to:
  - **Start a new round**
  - **Quit** the game
- After each round, log the **round number** and the **X, Y coordinates of all three darts** to a text file.

### 2. Review Mode
- The application must provide a way to review past rounds:
  - Display a list of previous rounds (loaded from the text file).
  - When a round is selected, display the **three dart positions** for that round on the PictureBox.
- **Play mode** and **review mode** are **mutually exclusive** (only one may be active at a time).

### 3. User Interface
- Use meaningful names for all controls (examples):
  - `dartboardPictureBox`
  - `startRoundButton`
  - `reviewButton`
  - `quitButton`
- Provide **tooltips** for all buttons and interactive controls.
- Set proper **TabIndex** order and include **access keys** (use `&` in control text, e.g., `&Start Round`).
- Use a descriptive form title (e.g., **“Dart Game Simulator”**).
- Display:
  - the **current round number**
  - the **dart throw count** (1–3)
  - whether the application is in **Play** or **Review** mode

### 4. Graphics
- Use the **Graphics** class to draw each dart as a circle with a center mark.
- Clear the PictureBox:
  - between rounds
  - when switching modes
- Ensure the dart's center is **always drawn within the bounds** of the PictureBox.

### 5. File I/O
- Log each round to a text file (example filename: `DartGame.log`).
- If the log file does not exist, create it.
- Use a **relative path** so the log file is created alongside the program’s source code.
- Each entry must include:
  - round number
  - X, Y coordinates for all three darts
- Use **error handling** for file operations (read/write), such as `try/catch` around:
  - `StreamWriter` / `File.AppendAllText`
  - `StreamReader` / `File.ReadAllLines`
- Do not allow duplicate round numbers in the log file.
- When starting a new round, check the log file for the last round number and continue from there.

**Log Format:**
- Example: `round,x1,y1,x2,y2,x3,y3`
```
1,150,200,120,180,170,220
2,130,210,160,190,140,230
3,110,220,180,200,150,240
```
### 6. Code Quality
- Use meaningful names for variables (camelCase), methods (PascalCase), and classes (PascalCase).
- Organize code with comments and proper indentation.
- Handle errors gracefully (file access issues, corrupt log entries, invalid selections, etc.).

### 7. Testing
Test the following:
- Dart throws always land **within the PictureBox** bounds.
- Logging is correct for multiple rounds.
- Review mode displays the correct darts for the selected round.
- Switching between Play and Review modes works cleanly (no mixed state).
- The program runs without errors or warnings.

