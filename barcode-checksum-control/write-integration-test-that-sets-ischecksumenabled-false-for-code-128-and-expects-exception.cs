// Title: Code128 Checksum Disabled Exception Test
// Description: Demonstrates disabling the checksum for a Code 128 barcode and verifies that the Aspose.BarCode library throws an exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as checksum handling. It uses BarcodeGenerator, EncodeTypes, and the IsChecksumEnabled property to show typical validation scenarios developers encounter when working with Code 128 symbology. Useful for integration testing and error‑handling patterns in barcode generation workflows.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: code128, checksum, exception, generation, aspose.barcode, aspose.barcode.generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the Code128 checksum disabling test.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with checksum disabled and expects an exception.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            try
            {
                // Attempt to disable checksum – this should trigger an exception for Code128
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate the barcode image (won't be reached if exception is thrown)
                using (var image = generator.GenerateBarCodeImage())
                {
                    Console.WriteLine("Test Failed: No exception thrown when disabling checksum for Code128.");
                }
            }
            catch (Exception ex)
            {
                // Expected path: exception caught confirming correct behavior
                Console.WriteLine("Test Passed: Caught expected exception.");
                Console.WriteLine("Exception message: " + ex.Message);
            }
        }
    }
}