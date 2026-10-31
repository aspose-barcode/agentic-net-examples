// Title: Center Align Top Caption for QR Code Barcode
// Description: Demonstrates how to add a top caption to a QR code and align it to the center using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CaptionParameters to customize barcode appearance. Developers often need to add descriptive text above barcodes for branding or informational purposes, and aligning captions correctly ensures a professional look. The snippet illustrates typical steps: creating a generator, configuring caption visibility, text, and alignment, then saving the image.
// Prompt: Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to CaptionAlignment.Center.
// Tags: qr code, caption, alignment, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example that generates a QR code with a centered top caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, adds a visible top caption,
    /// centers the caption, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting image file.
        string outputPath = Path.Combine(outputDir, "QrWithCenteredTopCaption.png");

        // Create a QR code generator with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Enable the top caption and set its displayed text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center the top caption horizontally.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}