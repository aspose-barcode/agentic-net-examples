// Title: Center Align Top Caption for QR Code Barcode
// Description: Demonstrates how to add a top caption to a QR code and align it to the center using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CaptionParameters to customize barcode appearance. Typical scenarios include adding descriptive text above barcodes for labeling, packaging, or marketing materials. Developers often need to control caption visibility, text, and alignment to meet branding or regulatory requirements.
// Prompt: Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to CaptionAlignment.Center.
// Tags: qr, caption, alignment, center, barcode, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code with a centered top caption and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode,
    /// aligns the top caption to the center, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "QrWithCenteredTopCaption.png");

        // Initialize the barcode generator for a QR code with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Enable and set the text for the top caption
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center-align the top caption horizontally
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}