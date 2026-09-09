// Title: Generate QR Code with custom human‑readable label font
// Description: Demonstrates creating a QR Code barcode, setting a custom font for the human‑readable text displayed below the symbol, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Two‑dimensional symbologies (QR Code) and text rendering customization. It showcases the BarcodeGenerator class, EncodeTypes, and CodeTextParameters for adjusting display text, font properties, and positioning—common tasks for developers needing branded or readable barcodes in reports, packaging, or web applications.
// Prompt: Generate QR Code barcode and set custom font for TwoDDisplayText showing human readable label.
// Tags: qr code, two-dimensional, custom font, display text, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code with a custom font for the human‑readable label.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code, customizes the display text font, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "qr_custom_font.png");

        // Initialize the barcode generator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the encoded data (URL) using UTF‑8 encoding.
            generator.SetCodeText("https://example.com", Encoding.UTF8);

            // Set the human‑readable text that appears below the QR Code.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Example QR";

            // Switch to manual font mode to apply custom font settings.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Configure the custom font: Helvetica, bold, 14 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Position the display text below the QR Code symbol.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}