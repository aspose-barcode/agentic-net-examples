// Title: Generate Postnet 2‑state barcode with custom bar height
// Description: Demonstrates creating a Postnet 2‑state postal barcode and overriding the bar height to 30 points.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Postnet to produce postal barcodes. Typical use cases include printing ZIP codes on mail pieces, customizing barcode dimensions, and exporting to image formats. Developers often need to control size properties such as BarHeight to meet mailing standards.
// Prompt: Generate a Postnet 2‑state postal barcode with overridden BarHeight set to 30 points.
// Tags: postnet, barcode, generation, barheight, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that creates a Postnet barcode with a custom bar height and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Postnet 2‑state barcode for a sample ZIP code, sets the bar height to 30 points, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Postnet symbology with a 5‑digit ZIP code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Disable automatic sizing so the explicit BarHeight value is applied.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the bar height to 30 points (1 point = 1/72 inch).
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode as a PNG image file.
            generator.Save("postnet.png");

            // Inform the user that the file has been created.
            Console.WriteLine("Postnet barcode saved to postnet.png");
        }
    }
}