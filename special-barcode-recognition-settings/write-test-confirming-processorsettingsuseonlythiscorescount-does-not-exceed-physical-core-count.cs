using System;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

/// <summary>
/// Demonstrates how to configure Aspose.BarCode processor settings
/// to limit the number of CPU cores used for barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Retrieves the physical core count, attempts to set a higher core usage,
    /// and validates that the configured core count does not exceed the actual cores.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Get the number of physical processor cores available on the machine.
        int physicalCores = Environment.ProcessorCount;

        // Disable automatic use of all cores; we will set a specific core count manually.
        BarCodeReader.ProcessorSettings.UseAllCores = false;

        // Attempt to assign a core count greater than the actual number of physical cores.
        // This should be clamped by the library to the maximum available cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = physicalCores + 5;

        // Retrieve the value that was actually set after the library's internal validation.
        int configuredCores = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;

        // Check whether the configured core count respects the physical core limit.
        if (configuredCores <= physicalCores)
        {
            // Success: the library correctly limited the core count.
            Console.WriteLine($"PASS: UseOnlyThisCoresCount ({configuredCores}) does not exceed physical cores ({physicalCores}).");
        }
        else
        {
            // Failure: the configured core count is higher than the physical core count.
            Console.WriteLine($"FAIL: UseOnlyThisCoresCount ({configuredCores}) exceeds physical cores ({physicalCores}).");
        }
    }
}