// Title: Code128 checksum disabled integration test
// Description: Demonstrates setting IsChecksumEnabled to false for a Code 128 barcode and verifies that an exception is thrown.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as checksum handling using the BarcodeGenerator and its Parameters.Barcode properties. Developers often need to test invalid configurations to ensure proper error handling, making this pattern useful for integration testing of barcode generation scenarios.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: code128, checksum, integration-test, exception-handling, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates an integration‑style test that disables checksum for a Code 128 barcode and expects an exception during generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the test, writes result to console, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "code128_test.png");

        try
        {
            // Create a BarcodeGenerator for Code128 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Disable checksum calculation – this configuration is expected to cause an exception
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Attempt to save the barcode image; an exception should be thrown
                generator.Save(tempFile, BarCodeImageFormat.Png);
            }

            // If no exception occurs, the test has failed
            Console.WriteLine("Test failed: no exception was thrown.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception was caught, indicating the test passed
            Console.WriteLine("Test passed: expected exception caught.");
            Console.WriteLine("Exception message: " + ex.Message);
        }
        finally
        {
            // Clean up the temporary file if it was created
            if (File.Exists(tempFile))
            {
                try
                {
                    File.Delete(tempFile);
                }
                catch
                {
                    // Ignore any errors during cleanup
                }
            }
        }
    }
}