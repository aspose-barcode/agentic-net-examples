// Title: Code128 Checksum Disabled Exception Demo
// Description: Demonstrates that disabling the checksum for a Code 128 barcode triggers an exception, illustrating proper validation of required symbology settings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode symbology constraints and error handling. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to show how developers can test invalid configuration scenarios, such as disabling mandatory checksums, and capture the resulting exceptions. Ideal for integration testing of barcode generation logic.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: code128, checksum, exception, integration-test, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that attempts to generate a Code128 barcode with checksum disabled,
/// expecting an exception to be thrown because the checksum is mandatory for this symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the test and reports the outcome.
    /// </summary>
    static void Main()
    {
        // Attempt to generate a Code128 barcode with checksum disabled.
        // Code128 requires a checksum, so an exception is expected.
        try
        {
            // Initialize the generator with Code128 symbology and sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Disable checksum – this should cause an exception when saving.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save to a memory stream to trigger barcode generation.
                using (var stream = new MemoryStream())
                {
                    generator.Save(stream, BarCodeImageFormat.Png);
                }

                // If no exception occurs, the test has failed.
                Console.WriteLine("Test failed: No exception was thrown.");
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception indicating checksum cannot be disabled for Code128.
            Console.WriteLine($"Test passed: Caught expected exception -> {ex.GetType().Name}: {ex.Message}");
        }
    }
}