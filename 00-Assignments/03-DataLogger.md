# Datalogger Application

## Objective
Create a VB.NET Windows Forms application to collect, visualize, and save analog input data from a PIC microcontroller.

## Requirements

### 1. User Interface
- Design a form with:
   - Top menu for file operations and settings
   - Right-click context menu for common actions
   - Start, Stop, and Save buttons
   - Status strip showing the current log file path, sampling rate, and comport status.
- Use meaningful control names in PascalCase with a type suffix (e.g., `StartButton`, `StopButton`)
- Set proper tab order and tab stops
- Add access keys and tool tips for all buttons
- Use a descriptive form title



### 2. Sampling Configuration
- Collect analog data from the PIC microcontroller at selectable sample rates:
   - 10, 5, and 1 minute intervals
   - 30, 10, 5, and 1 second intervals
   - 500, 200, and 100 millisecond intervals
- Allow users to choose from these sample rates.
- Validate the sampling rate input to prevent invalid values.


### 3. Data Visualization
- Use a PictureBox control to display sampled data as a graph.
- Provide options to view:
   - The entire history of sampled data
   - Only the last 30 seconds of sampled data
- The graph must update in real time as new data is collected.
- Ensure the graph is accurate, clear, and easy to understand.
- Make the graph resizable with the form and readable at all sizes.


### 4. Saving Sampled Data
- When the Save button is clicked, save the sampled data to a text file in the same directory as the application.
- Format each line as:
   ```
   "$$AN1",<HighByte>,<LowByte>,<timestamp>
   ```
- Field explanations:
   - `$$`: Start of a sample
   - `$$ANn`: Analog input channel
   - `<HighByte>` and `<LowByte>`: High and low byte values of the sampled data
   - `<timestamp>`: Time the sample was taken, formatted as YYMMDDHHMMSSmm (Year, Month, Day, Hour, Minute, Second, Millisecond)
- Name the file properly and include a descriptive assignment header.


### 5. Log File Management
- Allow users to open and save log files containing sampled data.
- Use open and save file dialogs with filters set to `.log` files.
- Default filename: `log_YYMMDDHH.log` (reflects the hour of data collection; a new file is created every hour and each file contains data for only that hour).


### 6. Code Quality
- Use meaningful names for all classes, subs, and functions (PascalCase).
- Use camelCase for variable names and manage variable scope properly.
- Include informative comments throughout the code.
- Maintain proper indentation, line spacing, and line length for readability.
- Enable Option Explicit and Option Strict to enforce variable declarations and type safety.
- Implement error handling for issues such as PIC disconnections or invalid sampling rates, with user-friendly messages.


### 7. Testing
- Test real-time graph updates at different sampling rates.
- Ensure the application meets all requirements: starting/stopping sampling, graphing data, and saving data to a file.
- The program must compile without errors or warnings and function according to the specifications.

**Additional Notes:**
- Ensure the application interface is intuitive, allowing users to easily start/stop sampling and view/save the collected data.
- Award extra credit for implementing additional creative features or optimizations, such as using more than one analog input (up to 4 channels).
- Pay attention to error handling, especially in cases where the PIC microcontroller may disconnect or the sampling rate is incorrectly configured.
