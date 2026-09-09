// Title: Generate DotCode barcode with auto encoding and handle unsupported characters
// Description: Demonstrates generating a DotCode barcode in Auto encoding mode and catching errors when characters are not supported by the selected ECI encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DotCode symbology. It shows how to configure encoding mode, set an ECI encoding, and handle exceptions for unsupported characters. Developers working with barcode creation often need to adjust encoding settings and implement error handling to ensure valid output across different character sets.
// Prompt: Implement error handling for unsupported characters when generating DotCode in Auto encoding mode.
// Tags: dotcode, barcode, generation, error-handling, auto-encoding, eci, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a DotCode barcode using auto encoding mode
/// and demonstrates error handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DotCode barcode, forces an unsupported ECI encoding,
    /// and catches any resulting exceptions.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "DotCodeAuto.png");

        // Create a BarcodeGenerator for DotCode with a sample text containing Unicode characters
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "犬Right狗"))
        {
            // Set Auto encoding mode explicitly (default is Auto, but shown for clarity)
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;

            // Assign an ECI encoding (ISO-8859-1) that does not support the given characters
            // This will trigger an exception during barcode generation
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_1;

            try
            {
                // Attempt to save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
            catch (Exception ex)
            {
                // Handle errors such as unsupported characters in the selected ECI encoding
                Console.WriteLine($"Unsupported character encountered: {ex.Message}");
            }
        }
    }
}