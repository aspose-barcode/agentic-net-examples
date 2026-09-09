// Title: Save Barcode as JPEG with Compression Settings
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG image with compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode image export category, illustrating how to configure barcode generation parameters such as resolution and anti-aliasing before saving to a JPEG format. It uses the BarcodeGenerator class and BarCodeImageFormat enumeration, common tasks for developers needing optimized barcode images for web or mobile applications.
// Prompt: Provide configuration to enable compression when saving barcode images as JPEG to reduce file size.
// Tags: barcode, code128, jpeg, compression, image export, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode and saves it as a compressed JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates the barcode,
    /// configures image parameters, and saves the result as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary output folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the JPEG output file.
        string outputPath = Path.Combine(outputDir, "barcode.jpg");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set image resolution (dots per inch) to reduce file size.
            generator.Parameters.Resolution = 72f;

            // Disable anti-aliasing to further lower the output size.
            generator.Parameters.UseAntiAlias = false;

            // Save the barcode as a JPEG image; default compression is applied.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}