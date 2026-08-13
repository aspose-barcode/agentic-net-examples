// Title: Integration test for disabling checksum in Code128 barcode generation
// Description: Demonstrates how to configure a Code 128 barcode generator to disable checksum validation and verify that an exception is thrown when saving an invalid barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and generator parameters such as IsChecksumEnabled and ThrowExceptionWhenCodeTextIncorrect. Developers often need to validate barcode data integrity or test error handling for unsupported configurations, making this pattern useful for unit and integration testing of barcode generation logic.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: barcode, code128, checksum, exception, integration-test, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an integration test that disables checksum for a Code 128 barcode and expects an exception during save.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "code128_test.png");

        try
        {
            // Initialize a BarcodeGenerator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Disable checksum generation for Code128.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Configure the generator to throw an exception if the code text is incorrect.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to save the barcode; an exception is expected due to disabled checksum.
                generator.Save(outputPath);

                // If execution reaches this point, no exception was thrown and the test fails.
                Console.WriteLine("FAIL: No exception was thrown.");
            }
        }
        catch (Exception ex)
        {
            // Expected outcome: an exception should be caught, indicating the test passed.
            Console.WriteLine($"PASS: Caught expected exception: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it was created.
            if (File.Exists(outputPath))
            {
                try
                {
                    File.Delete(outputPath);
                }
                catch
                {
                    // Suppress any errors during cleanup.
                }
            }
        }
    }
}