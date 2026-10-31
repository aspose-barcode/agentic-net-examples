// Title: Generate Swiss Post Parcel barcode and save as PNG
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode from an identifier string and exporting it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include creating shipping labels for Swiss Post parcels, where developers need to encode parcel identifiers into machine‑readable barcodes and save them in common image formats.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier string and save as PNG.
// Tags: barcode, swisspost, parcel, generation, png, aspose.barcode, encode-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode from a Swiss Post identifier and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the parcel identifier string as per Swiss Post format
        string identifier = "98.34.123456.12345678";

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomesticMail.png");

        // Initialize the barcode generator with SwissPostParcel symbology and the identifier
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifier))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the barcode height in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}