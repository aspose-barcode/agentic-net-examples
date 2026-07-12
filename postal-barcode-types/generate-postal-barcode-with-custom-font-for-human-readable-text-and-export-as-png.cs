// Title: Generate Postal Barcode with Custom Font
// Description: Demonstrates creating a Postnet barcode with a custom human‑readable font and exporting it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as symbology, auto‑size mode, font styling, colors, and image output. It uses the BarcodeGenerator class and related parameter objects, which are commonly employed by developers to produce printable barcodes for mailing, shipping, and inventory applications.
// Prompt: Generate a postal barcode with a custom font for the human‑readable text and export as PNG.
// Tags: postnet, barcode generation, custom font, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet postal barcode with a custom font for the
/// human‑readable text and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, applies visual customizations,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define output file name and the sample ZIP+4 code to encode.
        const string outputPath = "postal.png";
        const string codeText = "12345-6789"; // Example ZIP+4 code

        // Initialize the barcode generator for the Postnet symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Enable auto‑size mode to let the library calculate optimal bar height.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Configure the appearance of the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial"; // Custom font name.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;      // Font size.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Position text below bars.

            // Optional: set barcode and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the file has been created.
        Console.WriteLine($"Postal barcode saved to '{outputPath}'.");
    }
}