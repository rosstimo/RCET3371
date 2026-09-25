# C# / .NET Toolchain Guide

[Toolchain setup index](README.md)

## Goal

Starting from a Windows computer, install the course C# environment and get both a console program and a basic Windows Forms program running.

## Course baseline

- Visual Studio Community 2026
- .NET 10 LTS SDK
- **.NET desktop development** workload

The .NET command-line SDK is also used so you can prove a project outside the IDE.

## 1. Download Visual Studio Community

Official download:

https://visualstudio.microsoft.com/vs/community/

Choose **Visual Studio Community**.

If your lab computer already has the correct Visual Studio 2026 installation, skip to verification rather than reinstalling.

## 2. Install the required workload

Run the downloaded Visual Studio installer.

In the workload selection screen, select:

- **.NET desktop development**

Keep the default recommended components for that workload unless the instructor provides a course `.vsconfig`.

Complete the installation and launch Visual Studio.

## 3. Verify the .NET SDK

Open PowerShell, Command Prompt, or the Visual Studio terminal:

```text
dotnet --version
dotnet --info
```

The major version should be 10 for the S27 course baseline.

If `dotnet` is missing or reports the wrong major version, install the .NET 10 SDK from:

https://dotnet.microsoft.com/en-us/download/dotnet/10.0

Then reopen the terminal and verify again.

## 4. Create your first Console App in Visual Studio

1. Launch Visual Studio.
2. Select **Create a new project**.
3. Search for **Console App**.
4. Choose the C# Console App template.
5. Select **Next**.
6. Project name: `Hello3371`.
7. Choose a location you can find again.
8. Select the .NET 10 framework when the template asks.
9. Create the project.

Replace the starter code with:

```csharp
string course = "RCET 3371";
int section = 2;

Console.WriteLine($"Hello, {course}!");
Console.WriteLine($"2 + 3 = {2 + 3}");
Console.WriteLine($"Section = {section}");
```

Build with **Build > Build Solution**.

Run without the debugger with **Ctrl+F5**.

Expected output includes:

```text
Hello, RCET 3371!
2 + 3 = 5
Section = 2
```

## 5. Use the debugger once

1. Click the margin beside a `Console.WriteLine` line to set a breakpoint.
2. Start debugging with **F5**.
3. When execution stops, hover over `course` and `section`.
4. Use **Step Over** once.
5. Stop debugging.

At this point you have proved editor + build + runtime + debugger at a practical level.

## 6. Prove the same project from the command line

Open a terminal in the directory containing the project file:

```text
dotnet restore
dotnet build
dotnet run
```

The IDE is convenient, but the command-line path gives a reproducible baseline.

## 7. Create a first Windows Forms program

1. Create another project.
2. Search for **Windows Forms App**.
3. Choose the C# template targeting modern .NET, not an old .NET Framework template.
4. Name it `HelloForms3371`.
5. Add a Button and Label in the designer.
6. Double-click the Button to create its Click handler.
7. In the handler, set the label text:

```csharp
private void helloButton_Click(object sender, EventArgs e)
{
    outputLabel.Text = "Hello from a button click!";
}
```

Run the program and click the button.

This is intentionally familiar RCET 2265 event-driven programming. Repaint architecture comes later.

## 8. Troubleshooting

### Console App template is missing

Open **Visual Studio Installer > Modify** and confirm **.NET desktop development** is installed.

### Wrong `dotnet` version

Run `dotnet --list-sdks`. Confirm a 10.x SDK exists.

### Project builds in Visual Studio but not terminal

Make sure the terminal is in the project directory and run `dotnet --info`. Record the exact error before changing settings.

### WinForms template is missing

Modify the Visual Studio installation and confirm the .NET desktop workload is installed.

## References

- Visual Studio Community: https://visualstudio.microsoft.com/vs/community/
- Visual Studio installation: https://learn.microsoft.com/visualstudio/install/
- .NET 10 download: https://dotnet.microsoft.com/en-us/download/dotnet/10.0
- .NET CLI: https://learn.microsoft.com/en-us/dotnet/core/tools/
