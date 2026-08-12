// Title: Enable JPEG compression for Code128 barcode generation
// Description: Demonstrates how to configure a BarcodeGenerator to produce a compressed JPEG image of a Code128 barcode, reducing file size by adjusting resolution and anti-aliasing.
// Category-Description: This example belongs to the Aspose.BarCode image output configuration category. It showcases the use of BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and generator.Parameters to control image quality and size. Developers often need to balance readability and file size when exporting barcodes for web or mobile applications, making these settings essential for performance‑critical scenarios.
// Prompt: Provide configuration to enable compression when saving barcode images as JPEG to reduce file size.
// Tags: code128, image compression, jpeg, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a Code128 barcode and saves it as a compressed JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode generation settings to reduce JPEG file size.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting compressed JPEG file
        string outputFile = Path.Combine(outputDir, "code128_compressed.jpg");

        // Initialize the barcode generator for the Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode
            generator.CodeText = "1234567890";

            // Lower the image resolution (e.g., 72 DPI) to decrease file size
            generator.Parameters.Resolution = 72f;

            // Turn off anti‑aliasing to further reduce the output size
            generator.Parameters.UseAntiAlias = false;

            // Save the barcode as a JPEG image using the configured compression settings
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}