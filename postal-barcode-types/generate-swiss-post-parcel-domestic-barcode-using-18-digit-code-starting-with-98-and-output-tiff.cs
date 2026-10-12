// Title: Generate Swiss Post Parcel domestic barcode and save as TIFF
// Description: Demonstrates creating an 18‑digit Swiss Post Parcel domestic barcode (starting with 98) and saving it as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce postal barcodes. Typical use cases include generating shipping labels for Swiss Post parcels where a specific 18‑digit numeric code is required. Developers often need to set barcode dimensions and export the result to common image formats such as TIFF for printing or archival.
// Prompt: Generate a Swiss Post Parcel domestic barcode using an 18‑digit code starting with 98 and output TIFF.
// Tags: swisspost, parcel, barcode, generation, tiff, aspnet, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an 18‑digit Swiss Post Parcel barcode and writes it to a TIFF image.
    /// </summary>
    static void Main()
    {
        // Define the 18‑digit barcode value (must start with 98 for Swiss Post Parcel)
        string code = "983412345612345678";

        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomestic.tif");

        // Initialize the barcode generator with the Swiss Post Parcel symbology and the code
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
        {
            // Set the X dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the barcode height in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}