// Title: Generate QR Code with custom font for human‑readable label
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set a custom font for the TwoDDisplayText, and save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on two‑dimensional symbologies. It shows how to configure CodeTextParameters such as FontMode, Font, and TwoDDisplayText for QR Code generation. Developers commonly use these APIs to add readable text beneath or alongside barcodes for branding or user guidance, and to customize appearance for different output formats.
// Prompt: Generate QR Code barcode and set custom font for TwoDDisplayText showing human readable label.
// Tags: qr code, two-dimensional, font customization, codetext, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, applies a custom font to the
/// human‑readable label (TwoDDisplayText), and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the full path where the generated PNG will be saved.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_custom_font.png");

        // Create a BarcodeGenerator for QR Code with the data "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "1234567890"))
        {
            // --------------------------------------------------------------------
            // Configure the appearance of the human‑readable text (TwoDDisplayText)
            // --------------------------------------------------------------------
            // Use manual font settings instead of the default automatic mode.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the desired font family and size for the label.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Define the text that will be displayed alongside the QR Code.
            // This does not affect the encoded data; it only provides a readable label.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My QR Code";

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"QR Code with custom font saved to: {outputPath}");
    }
}