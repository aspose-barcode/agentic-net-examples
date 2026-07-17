// Title: Generate QR Code and embed in Razor page
// Description: Demonstrates creating a QR Code image with Aspose.BarCode and shows how to embed it in a Razor view using an <img> tag helper.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code images. Typical use cases include generating scannable codes for URLs, product information, or authentication tokens, which developers often embed directly into ASP.NET Razor pages via image tag helpers. The snippet highlights setting QR error correction levels and saving the output in PNG format, a common workflow for web applications.
// Prompt: Generate QR Code barcode and embed it into a Razor page using image tag helper.
// Tags: qr code, barcode generation, image output, aspnet razor, tag helper, aspose.barcode, qr error correction

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code image and provides an HTML snippet
/// for embedding the image in an ASP.NET Razor view using an <img> tag helper.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code PNG file and writes an <img> tag snippet to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated QR Code image.
        string outputPath = "qr.png";

        // Create a BarcodeGenerator for QR encoding with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure a high error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Build an HTML <img> tag that references the generated PNG file.
        string htmlSnippet = $"<img src=\"{outputPath}\" alt=\"QR Code\" />";

        // Output the HTML snippet so it can be copied into a Razor view.
        Console.WriteLine(htmlSnippet);
    }
}