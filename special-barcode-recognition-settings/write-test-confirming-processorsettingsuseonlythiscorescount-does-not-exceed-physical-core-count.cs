// Title: Validate ProcessorSettings Core Count Limitation
// Description: Demonstrates how to verify that Aspose.BarCode's ProcessorSettings.UseOnlyThisCoresCount never exceeds the physical core count of the machine.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating the use of ProcessorSettings to control multithreading. It shows how to query the system's core count, attempt to set a higher core usage, and confirm the library caps the value. Developers working with barcode generation and recognition often need to limit CPU usage for scalability and resource management.
// Prompt: Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
// Tags: barcode, performance, multithreading, processor settings, core count, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates testing that ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the core count validation and optionally generates and reads a barcode.
    /// </summary>
    static void Main()
    {
        // Determine the number of logical processors available on the machine.
        int coreCount = Environment.ProcessorCount;

        // Attempt to set UseOnlyThisCoresCount to a value greater than the available cores.
        int attemptedCores = coreCount + 5;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = attemptedCores;

        // Retrieve the actual value after the assignment; the library should cap it.
        int actualCores = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;

        // Verify that the actual value does not exceed the physical core count.
        bool testPassed = actualCores <= coreCount;

        // Output the test results.
        Console.WriteLine($"Physical core count: {coreCount}");
        Console.WriteLine($"Attempted to set UseOnlyThisCoresCount to: {attemptedCores}");
        Console.WriteLine($"Actual UseOnlyThisCoresCount value: {actualCores}");
        Console.WriteLine($"Test passed: {testPassed}");

        // Optional: generate a simple barcode and read it to demonstrate usage.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode image to a memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Read the barcode from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Read barcode: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
        }

        // Reset ProcessorSettings to default values (optional cleanup).
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
    }
}