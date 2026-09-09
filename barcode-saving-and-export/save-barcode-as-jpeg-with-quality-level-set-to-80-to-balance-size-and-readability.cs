// Title: Save Barcode as JPEG with Specified Quality
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG image, highlighting resolution and anti-aliasing settings while noting that JPEG quality cannot be set directly via the Aspose.BarCode API.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes and export them to common image formats. Developers often need to adjust image resolution, anti‑aliasing, and compression settings to balance file size and readability when integrating barcodes into documents, web pages, or mobile apps.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: barcode, code128, generation, jpeg, image format, quality, resolution, anti-aliasing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures image parameters, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputFile = Path.Combine(Path.GetTempPath(), "sample_barcode.jpg");

        // The data to encode in the barcode.
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology and the sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Reduce image resolution to lower file size (optional).
            generator.Parameters.Resolution = 72f;

            // Disable anti‑aliasing to further reduce size (optional).
            generator.Parameters.UseAntiAlias = false;

            // Note: Aspose.BarCode does not expose a direct JPEG quality setting.
            // The generated JPEG will use the default compression quality.

            // Save the generated barcode as a JPEG file.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved and the limitation regarding JPEG quality.
        Console.WriteLine($"Barcode saved to: {outputFile}");
        Console.WriteLine("Note: JPEG quality level cannot be set explicitly with Aspose.BarCode API.");
    }
}