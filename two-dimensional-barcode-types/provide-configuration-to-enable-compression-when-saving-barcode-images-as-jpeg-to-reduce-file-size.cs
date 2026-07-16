// Title: Compress JPEG Barcode Image
// Description: Demonstrates how to enable compression when saving a barcode as a JPEG by adjusting resolution and anti‑aliasing settings.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure the BarcodeGenerator to produce smaller JPEG files. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use when creating barcodes for web or mobile applications where bandwidth and storage are concerns.
// Prompt: Provide configuration to enable compression when saving barcode images as JPEG to reduce file size.
// Tags: code128, generation, jpeg, compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode and saves it as a compressed JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode generation parameters to reduce JPEG file size.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the compressed JPEG barcode image.
        string outputPath = "barcode_compressed.jpg";

        // Ensure the directory for the output file exists; create it if necessary.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Lower the image resolution (e.g., 72 DPI) to reduce JPEG size.
            generator.Parameters.Resolution = 72f;

            // Disable anti‑aliasing to further decrease file size (optional).
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG image using the configured settings.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}