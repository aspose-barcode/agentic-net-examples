// Title: Generate Swiss Post Parcel domestic barcode and save as TIFF
// Description: Demonstrates creating an 18‑digit Swiss Post Parcel domestic barcode (starting with 98) using Aspose.BarCode and saving it as a TIFF image.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce a Swiss Post Parcel barcode, configure visual parameters such as X‑dimension and bar height, and export the result in TIFF format. Developers working on shipping, logistics, or document automation often need to generate printable barcodes for parcel labeling and tracking.
// Prompt: Generate a Swiss Post Parcel domestic barcode using an 18‑digit code starting with 98 and output TIFF.
// Tags: swisspostparcel, barcode generation, tiff, aspose.barcode, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode and saving it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures dimensions, saves to file, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the 18‑digit barcode value (must start with 98 for Swiss Post Parcel)
        string code = "983412345612345678";

        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomestic.tif");

        // Initialize the generator with the Swiss Post Parcel symbology and the barcode value
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
        {
            // Set the X‑dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the bar height in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the barcode image as a TIFF file using CMYK color space
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}