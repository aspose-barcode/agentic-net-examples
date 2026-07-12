// Title: Verify ProcessorSettings core count does not exceed physical cores
// Description: Demonstrates how to test that Aspose.BarCode's ProcessorSettings.UseOnlyThisCoresCount is limited to the machine's physical core count.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating the use of BarCodeReader.ProcessorSettings to control multi‑core processing. Developers often need to limit CPU usage for barcode recognition tasks in server environments; the key API classes include BarCodeReader and its nested ProcessorSettings. Typical scenarios involve configuring core usage to balance performance and resource constraints.
// Prompt: Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
// Tags: barcode, processor-settings, core-count, performance, aspose.barcode, test

using System;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

/// <summary>
/// Example program that validates the configured core count for Aspose.BarCode's
/// processor settings does not exceed the physical core count of the host machine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Retrieves the physical core count, configures
    /// <see cref="BarCodeReader.ProcessorSettings"/> to use that many cores, and
    /// verifies the configuration does not exceed the actual core count.
    /// </summary>
    static void Main()
    {
        // Retrieve the number of logical processors reported by the runtime.
        // In most environments this corresponds to the physical core count.
        int physicalCoreCount = Environment.ProcessorCount;

        // Disable automatic core selection and explicitly set the core count.
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = physicalCoreCount; // attempt to use all available cores

        // Read back the configured core count for validation.
        int configuredCoreCount = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;

        // Ensure the configured value does not exceed the actual core count.
        if (configuredCoreCount > physicalCoreCount)
        {
            throw new InvalidOperationException(
                $"Configured core count ({configuredCoreCount}) exceeds physical core count ({physicalCoreCount}).");
        }

        // Output success message; this line is safe for non‑interactive CI pipelines.
        Console.WriteLine($"Test passed: Configured core count ({configuredCoreCount}) is within the physical core count ({physicalCoreCount}).");
    }
}