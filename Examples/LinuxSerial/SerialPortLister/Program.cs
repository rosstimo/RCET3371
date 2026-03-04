// Program.cs
// dotnet add package System.IO.Ports

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;

internal static class Program
{
    private static int Main(string[] args)
    {
        var dotnetPorts = SerialPort.GetPortNames()
            .Select(NormalizeToDevPath)
            .Where(File.Exists)
            .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
            .ToList();
        var devPorts = DiscoverDevPorts().ToList();

        if (dotnetPorts.Count == 0 && devPorts.Count == 0)
        {
            Console.WriteLine("No serial ports found by .NET or /dev scan.");
            Console.WriteLine("If you expected some, check permissions (uucp/dialout) and that the device is plugged in.");
            return 1;
        }

        Console.WriteLine(".NET reported ports:");
        foreach (var p in dotnetPorts)
            Console.WriteLine($"  {p}");

        Console.WriteLine("\n/dev scan ports:");
        foreach (var p in devPorts)
        {
            var target = TryResolveSymlink(p);
            if (!string.IsNullOrEmpty(target))
                Console.WriteLine($"  {p} -> {target}");
            else
                Console.WriteLine($"  {p}");
        }

        // Show differences
        var onlyDotnet = dotnetPorts.Except(devPorts).ToList();
        var onlyDev = devPorts.Except(dotnetPorts).ToList();
        var both = dotnetPorts.Intersect(devPorts).ToList();

        Console.WriteLine("\nSummary of differences:");
        if (onlyDotnet.Count > 0)
        {
            Console.WriteLine("  Only in .NET:");
            foreach (var p in onlyDotnet) Console.WriteLine($"    {p}");
        }
        if (onlyDev.Count > 0)
        {
            Console.WriteLine("  Only in /dev scan:");
            foreach (var p in onlyDev) Console.WriteLine($"    {p}");
        }
        if (both.Count > 0)
        {
            Console.WriteLine("  In both:");
            foreach (var p in both) Console.WriteLine($"    {p}");
        }

        return 0;
    }

    private static IEnumerable<string> DiscoverDevPorts()
    {
        var ports = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // What Linux exposes under /dev
        foreach (var pattern in new[]
                 {
                     "ttyS*",     // classic PC serial
                     "ttyUSB*",   // USB-serial adapters
                     "ttyACM*",   // many dev boards (Arduino-style)
                     "ttyAMA*",   // ARM UARTs (common on SBCs)
                     "rfcomm*",   // Bluetooth serial
                     "tty.*"      // macOS style isn't Linux, but harmless; also catches some oddballs
                 })
        {
            foreach (var path in SafeEnumerateFiles("/dev", pattern))
                ports.Add(path);
        }

        // Stable names (symlinks) if present
        foreach (var path in SafeEnumerateFiles("/dev/serial/by-id", "*"))
            ports.Add(path);

        // Filter out anything that doesn't exist (paranoia, but cheap)
        return ports
            .Where(File.Exists)
            .OrderBy(p => p, StringComparer.OrdinalIgnoreCase);
    }

    private static IEnumerable<string> SafeEnumerateFiles(string dir, string pattern)
    {
        try
        {
            if (!Directory.Exists(dir))
                return Array.Empty<string>();

            // EnumerateFiles returns full paths already for absolute dir, but normalize anyway
            return Directory.EnumerateFiles(dir, pattern)
                .Select(Path.GetFullPath);
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private static string NormalizeToDevPath(string name)
    {
        // On Linux, GetPortNames() often returns "/dev/ttyUSB0" etc,
        // but some runtimes may return "ttyUSB0". Normalize.
        if (string.IsNullOrWhiteSpace(name))
            return name;

        return name.StartsWith("/", StringComparison.Ordinal)
            ? name
            : "/dev/" + name;
    }

    private static string? TryResolveSymlink(string path)
    {
        try
        {
            var fi = new FileInfo(path);

#if NET6_0_OR_GREATER
            // If it's a symlink, LinkTarget is non-null/non-empty
            var linkTarget = fi.LinkTarget;
            if (string.IsNullOrEmpty(linkTarget))
                return null;

            // LinkTarget can be relative to the link's directory
            if (!Path.IsPathRooted(linkTarget))
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir))
                    linkTarget = Path.GetFullPath(Path.Combine(dir, linkTarget));
            }

            return linkTarget;
#else
            return null;
#endif
        }
        catch
        {
            return null;
        }
    }
}