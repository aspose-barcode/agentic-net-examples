// Title: Generate QR Code with custom human‑readable label font
// Description: Demonstrates creating a QR Code barcode, setting a custom display text, and applying a specific font to the human‑readable label.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Two‑Dimensional symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize QR Code appearance, a common requirement for developers needing branded or readable barcodes in images, PDFs, or UI components.
// Prompt: Generate QR Code barcode and set custom font for TwoDDisplayText showing human readable label.
// Tags: qr code, two-dimensional, custom font, display text, aspose.barcode, barcode generation, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, sets a custom human‑readable label,
/// and applies a specific font to that label using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, customizes its display text and font,
    /// then saves the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleData"))
        {
            // Set the text that will be displayed instead of the raw codetext in the QR image.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My QR Label";

            // Customize the font for the displayed text (family and size).
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Optionally adjust the location of the human‑readable text (e.g., below the QR code).
            // generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Define the output file path and save the generated QR code image.
            string outputPath = "qr_custom.png";
            generator.Save(outputPath);

            // Inform the user where the image was saved.
            Console.WriteLine($"QR code saved to {outputPath}");
        }
    }
}