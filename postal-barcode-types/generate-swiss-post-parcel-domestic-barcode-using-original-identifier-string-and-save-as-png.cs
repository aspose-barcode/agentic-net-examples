// Title: Generate Swiss Post Parcel Domestic Barcode and Save as PNG
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode from an original identifier string and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce postal barcodes. Typical use cases include generating shipping labels for Swiss Post parcels, where developers need to encode the original identifier into a barcode image for printing or electronic distribution. The snippet shows setting basic barcode parameters such as X‑dimension and bar height before saving the result.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier string and save as PNG.
// Tags: swisspost, parcel, domestic, barcode, generation, png, aspnet, aspose.barcode

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
    /// Entry point of the example. Creates output folder, generates barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the directory where the barcode image will be saved.
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Build the full file path for the PNG image.
        string outputPath = Path.Combine(outputDirectory, "SwissPostDomesticMail.png");

        // Original identifier string required by Swiss Post Parcel barcode.
        string originalIdentifier = "98.34.123456.12345678";

        // Initialize the barcode generator with the Swiss Post Parcel symbology and identifier.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalIdentifier))
        {
            // Set the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the bar height in pixels.
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Swiss Post Domestic barcode saved to: {outputPath}");
    }
}