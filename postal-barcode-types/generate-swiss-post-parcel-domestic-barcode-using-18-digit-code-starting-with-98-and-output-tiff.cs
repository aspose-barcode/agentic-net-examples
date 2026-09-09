// Title: Generate Swiss Post Parcel barcode and save as TIFF
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode with an 18‑digit value beginning with 98, then saving it as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It illustrates typical tasks such as setting barcode dimensions, configuring image format, and exporting to a file—common operations for developers integrating Swiss Post parcel barcodes into shipping or logistics applications.
// Prompt: Generate a Swiss Post Parcel domestic barcode using an 18‑digit code starting with 98 and output TIFF.
// Tags: swisspostparcel, barcode generation, tiff, aspnet, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Swiss Post Parcel barcode and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures dimensions, saves to a temporary TIFF file, and writes the output path to console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.tif");

        // Initialize the barcode generator with Swiss Post Parcel symbology and an 18‑digit value starting with 98
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "983412345612345678"))
        {
            // Set the X‑dimension (module width) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the barcode height to 40 pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a TIFF image in CMYK color space
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}