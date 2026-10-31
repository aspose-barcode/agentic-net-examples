// Title: Replace QR Code Display Text with Custom URL
// Description: Demonstrates how to change the text displayed beneath a QR code to a custom URL while keeping the encoded data unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize QR code appearance using the BarcodeGenerator class and its CodeTextParameters. Typical use cases include branding QR codes, providing friendly URLs, or adding contextual information without altering the encoded payload. Developers working with 2‑D barcodes often need to modify displayed text while preserving scan data, and this snippet shows the essential API calls.
// Prompt: Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text.
// Tags: qr, barcode, replace-text, png, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code and replaces its displayed text with a custom URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, customizes the displayed text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "QrWithCustomUrl.png");

        // Create a QR code generator with initial data ("OriginalData")
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "OriginalData"))
        {
            // Replace the text shown under the QR code with a custom URL
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://example.com";

            // Save the QR code image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}