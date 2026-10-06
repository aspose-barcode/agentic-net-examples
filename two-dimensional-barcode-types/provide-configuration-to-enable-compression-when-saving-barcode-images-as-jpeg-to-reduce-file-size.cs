// Title: Compress JPEG Barcode Image Using Aspose.BarCode
// Description: Demonstrates how to configure Aspose.BarCode to generate a JPEG barcode image with reduced file size by adjusting resolution and disabling anti-aliasing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control image quality settings such as resolution, anti-aliasing, and compression when saving barcodes. It uses the BarcodeGenerator class and its Parameters property to modify rendering options, a common requirement for developers who need optimized barcode images for web or mobile applications.
// Prompt: Provide configuration to enable compression when saving barcode images as JPEG to reduce file size.
// Tags: barcode, jpeg, compression, resolution, anti-aliasing, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with JPEG compression settings using Aspose.BarCode.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, applies compression settings,
    /// and saves the result as a JPEG file.
    /// </summary>
    public static void Main()
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeCompressionDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the JPEG file to be saved
        string outputPath = Path.Combine(outputDir, "barcode.jpg");

        // Create a BarcodeGenerator for Code128 symbology with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Lower the image resolution to 72 DPI to reduce JPEG file size
            generator.Parameters.Resolution = 72f;

            // Disable anti-aliasing to further decrease the output size
            generator.Parameters.UseAntiAlias = false;

            // Save the barcode as a JPEG image using the configured compression settings
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}