// Title: Integration test for disabling checksum on Code 128 barcode
// Description: Demonstrates how to configure a Code 128 barcode generator to disable checksum validation and verifies that the operation throws an exception.
// Category-Description: This example belongs to the Aspose.BarCode generation and validation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as IsChecksumEnabled. Developers often need to test error handling when configuring unsupported barcode settings, especially in automated integration tests.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: barcode, code128, checksum, integration-test, exception, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point for the integration test that verifies disabling checksum on a Code 128 barcode throws an exception.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the test, attempting to set <c>IsChecksumEnabled</c> to <c>EnableChecksum.No</c> for a Code 128 generator and confirming that an exception is raised.
    /// </summary>
    static void Main()
    {
        // Flag to track whether the expected exception was caught.
        bool exceptionCaught = false;

        // Create a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            try
            {
                // Attempt to disable checksum calculation – this configuration is not supported for Code128 and should throw.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate the barcode image (execution should not reach this point).
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    // No operation needed; exception is expected before image generation.
                }
            }
            catch (Exception ex)
            {
                // Expected path: capture the exception and mark the test as passed.
                exceptionCaught = true;
                Console.WriteLine("Expected exception caught: " + ex.Message);
            }
        }

        // Evaluate test result based on whether the exception was observed.
        if (!exceptionCaught)
        {
            Console.WriteLine("Test failed: No exception was thrown when disabling checksum for Code128.");
            Environment.Exit(1);
        }
        else
        {
            Console.WriteLine("Test passed.");
        }
    }
}