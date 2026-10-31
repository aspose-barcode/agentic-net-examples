// Title: Generate QR Code with custom human‑readable label font
// Description: Demonstrates creating a QR Code barcode, customizing the font of the human‑readable text, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Two‑Dimensional (2D) symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to modify display text and font settings, a common requirement when branding or labeling QR codes in applications such as marketing, inventory, or ticketing.
// Prompt: Generate QR Code barcode and set custom font for TwoDDisplayText showing human readable label.
// Tags: qr,qrcode,barcode,2d,customfont,displaytext,aspnet,aspose.barcode,png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example that creates a QR Code, applies a custom font to the human‑readable label,
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code with custom TwoDDisplayText and font, then writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_custom.png");

        // Initialize the barcode generator for QR Code with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "1234567890"))
        {
            // -------------------------------------------------
            // Configure the human‑readable text appearance.
            // -------------------------------------------------
            // Use manual font mode to allow custom font settings.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            // Set the font family to Helvetica.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            // Set the font size to 14 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Replace the default displayed text with a custom label.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Custom QR Label";

            // Optional: adjust the size of each QR module (pixel) for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}