using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates disabling checksum for Code128 barcode generation, which should raise an exception.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Attempts to generate a Code128 barcode with checksum disabled,
    /// expecting an exception, and reports the result.
    /// </summary>
    static void Main()
    {
        try
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Disable checksum for Code128 (this operation is expected to cause an exception)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Set the text to be encoded in the barcode
                generator.CodeText = "123456";

                // Attempt to generate and save the barcode image to a file
                generator.Save("code128_invalid.png");
            }

            // If execution reaches here, no exception was thrown, which means the test failed
            Console.WriteLine("No exception thrown, test failed.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be caught when checksum is disabled for Code128
            Console.WriteLine("Expected exception caught: " + ex.Message);
        }
    }
}