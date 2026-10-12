// Title: Generate DotCode barcode with Auto encoding and handle unsupported characters
// Description: Demonstrates generating a DotCode barcode in Auto encoding mode using a specific ECI encoding, and catches errors when the input contains characters not representable in that encoding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DotCode symbology, encoding modes, and ECI (Extended Channel Interpretation) handling. It showcases the use of BarcodeGenerator, EncodeTypes, DotCodeEncodeMode, and ECIEncodings to create barcodes, a common task for developers needing to produce machine-readable symbols with specific character set constraints. Typical use cases include inventory labeling, product tracking, and data encoding where character set compatibility must be validated.
// Prompt: Implement error handling for unsupported characters when generating DotCode in Auto encoding mode.
// Tags: dotcode, barcode, auto-encoding, eci, error-handling, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DotCode barcode using Auto encoding mode,
/// applies a specific ECI encoding, and demonstrates error handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DotCodeAuto.png");

        // Input text includes characters that cannot be represented by ISO-8859-1
        string codeText = "犬Right狗";

        // Initialize the barcode generator for DotCode with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Explicitly set Auto encoding mode (default behavior)
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;

            // Restrict encoding to ISO-8859-1, which will cause an exception for unsupported characters
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_1;

            try
            {
                // Attempt to save the barcode image; an exception is thrown if characters are unsupported
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Handle the error by reporting the unsupported character issue
                Console.WriteLine($"Unsupported character encountered: {ex.Message}");
            }
        }
    }
}