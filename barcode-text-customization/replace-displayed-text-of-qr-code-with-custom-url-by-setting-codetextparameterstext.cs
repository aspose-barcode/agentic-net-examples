// Title: Replace QR Code Display Text with Custom URL
// Description: Demonstrates how to change the displayed text of a QR code to a custom URL using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR code creation and customization. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to modify the visual representation of a barcode. Developers often need to generate QR codes with specific display text for marketing, product labeling, or authentication scenarios, and this snippet illustrates the typical API workflow for such tasks.
// Prompt: Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text.
// Tags: qr, barcode, codetext, displaytext, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR code image with a custom display URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, sets its displayed text to a custom URL, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeQRDemo");
        Directory.CreateDirectory(outputFolder);

        // Build the full path for the resulting QR code image.
        string outputPath = Path.Combine(outputFolder, "qr_custom_url.png");

        // Create a QR code generator with initial data (the actual encoded data can remain unchanged).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "OriginalData"))
        {
            // Set the text that will be displayed beneath the QR code to the desired custom URL.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://example.com";

            // Save the generated QR code image in PNG format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}