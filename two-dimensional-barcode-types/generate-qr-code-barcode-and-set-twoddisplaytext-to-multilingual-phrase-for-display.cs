// Title: Generate QR Code with Multilingual Display Text
// Description: Demonstrates creating a QR Code barcode containing a multilingual phrase and setting the TwoDDisplayText property so the same text is shown when the barcode is rendered as an image.
// Category-Description: Shows how to use Aspose.BarCode to generate 2‑D barcodes (QR Code) with custom display text. The example covers BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for developers needing to embed internationalized data in QR codes and control the human‑readable text shown alongside the barcode. Useful for web, mobile, or desktop apps that generate printable QR codes with multilingual labels.
// Prompt: Generate QR Code barcode and set TwoDDisplayText to multilingual phrase for display.
// Tags: qr code, two-dimensional, display text, multilingual, aspose.barcode, generation, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code containing multilingual text
/// and sets the TwoDDisplayText property for human‑readable display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG image.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_multilingual.png");

        // Multilingual phrase to encode (English, Chinese, Arabic).
        string multilingualText = "Hello 世界 مرحبا";

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the code text using UTF‑8 encoding to support all characters.
            generator.SetCodeText(multilingualText, Encoding.UTF8);

            // Set the text that will be displayed alongside the QR Code image.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = multilingualText;

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}