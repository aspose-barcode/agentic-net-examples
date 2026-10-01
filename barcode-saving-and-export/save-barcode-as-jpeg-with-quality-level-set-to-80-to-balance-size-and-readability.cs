// Title: Save Code128 barcode as JPEG with controlled quality settings
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG image while adjusting resolution and anti‑aliasing to influence file size and readability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes. Typical use cases include creating printable labels, inventory tags, or embedding barcodes in documents where image size matters. Developers often need to balance image quality with file size, using resolution and rendering options to achieve optimal results.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: code128, generation, jpeg, barcodegenerator, barcodimageformat

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and JPEG export with quality considerations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_sample.jpg");

        // Ensure the target directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution (DPI). Higher DPI improves readability but increases file size.
            generator.Parameters.Resolution = 300f; // 300 DPI for good readability

            // Disable anti‑aliasing to reduce file size further (optional).
            generator.Parameters.UseAntiAlias = false;

            // Note: Aspose.BarCode does not expose a direct JPEG quality setting.
            // Adjusting resolution and anti‑aliasing are the primary ways to influence size and clarity.

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}