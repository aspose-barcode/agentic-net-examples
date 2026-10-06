// Title: Generate QR Code with Multilingual Display Text
// Description: Creates a QR Code encoding a phrase containing English, Chinese, and Arabic characters and sets the TwoDDisplayText property so the same multilingual text is shown beneath the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to produce 2‑D barcodes (QR Code) with custom display text. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters classes to encode Unicode data, adjust font settings, and save the result as an image. Developers working with multilingual data, visual barcode labels, or custom barcode rendering will find this pattern useful.
// Prompt: Generate QR Code barcode and set TwoDDisplayText to multilingual phrase for display.
// Tags: qr code, multilingual, display text, barcode generation, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code that encodes a multilingual phrase
/// and sets the TwoDDisplayText property so the same text appears beneath the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, builds the QR Code,
    /// configures Unicode handling and display text, then saves the image.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Multilingual phrase to encode and display (English, Chinese, Arabic)
        string phrase = "Hello 世界 مرحبا";

        // Initialize QR Code generator with the phrase as the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, phrase))
        {
            // Explicitly set the code text using UTF‑8 to guarantee proper Unicode encoding
            generator.SetCodeText(phrase, Encoding.UTF8);

            // Assign the same phrase to the display text shown under the QR Code
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = phrase;

            // Optional: increase font size of the display text for better readability
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Build the full path for the output PNG file
            string outputPath = Path.Combine(outputDir, "qr_multilingual.png");

            // Save the generated QR Code image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
    }
}