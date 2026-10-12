// Title: Align top caption to center for QR code barcode
// Description: Demonstrates how to generate a QR code with a centered top caption using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers often need to customize barcode appearance, such as adding and aligning captions, for branding or informational purposes. The snippet illustrates typical steps: setting up output paths, configuring barcode properties, and saving the image.
// Prompt: Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to CaptionAlignment.Center.
// Tags: qr code, caption alignment, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code with a centered caption above it and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode settings, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "qr_with_top_caption.png");

        // Initialize the barcode generator for a QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Enable the caption above the barcode and set its text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center-align the caption horizontally.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Adjust the module size (pixel dimension) of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}