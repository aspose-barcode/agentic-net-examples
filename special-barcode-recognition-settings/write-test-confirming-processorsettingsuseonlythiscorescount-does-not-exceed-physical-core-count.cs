// Title: Validate ProcessorSettings Core Count Does Not Exceed Physical Cores
// Description: Demonstrates how to verify that the Aspose.BarCode ProcessorSettings.UseOnlyThisCoresCount property is not set higher than the machine's physical core count.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating the use of the BarCodeReader.ProcessorSettings class to control multithreading. Developers often need to limit CPU usage when processing large batches of barcodes, ensuring that the configured core count respects the actual hardware limits.
// Prompt: Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
// Tags: barcode, processor settings, core count, performance, aspose.barcode, test

using System;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that checks whether the configured core count for barcode processing
/// does not exceed the physical number of processor cores available on the machine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Retrieves the physical core count, configures
    /// ProcessorSettings to use an excessive number of cores, and validates that the
    /// resulting setting does not surpass the actual core count.
    /// </summary>
    static void Main()
    {
        // Get the number of physical processor cores reported by the environment.
        int physicalCoreCount = Environment.ProcessorCount;

        // ------------------------------------------------------------
        // Configure processor settings to intentionally exceed available cores.
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false; // Disable automatic core usage.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = physicalCoreCount + 5; // Attempt to set more cores than exist.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0; // No extra threads beyond the specified count.

        // Retrieve the actually configured core count after the assignment.
        int configuredCores = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;

        // Determine if the test passes: configured cores must be less than or equal to physical cores.
        bool testPassed = configuredCores <= physicalCoreCount;

        // Output the results to the console.
        Console.WriteLine($"Physical core count: {physicalCoreCount}");
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount: {configuredCores}");
        Console.WriteLine($"Test {(testPassed ? "PASSED" : "FAILED")}");
    }
}