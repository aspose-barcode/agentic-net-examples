// Title: Generate Swiss Post Parcel barcodes and zip them
// Description: Demonstrates creating Swiss Post Parcel domestic barcodes from 18‑digit identifiers and storing the PNG images in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce barcode images, configure dimensions, and package multiple outputs using System.IO.Compression. Typical use cases include batch creation of shipping labels or parcel tracking codes where developers need to automate barcode image creation and archive them for distribution.
// Prompt: Generate Swiss Post Parcel domestic barcodes for a list of 18‑digit codes, writing them to a ZIP archive.
// Tags: swisspostparcel, barcode, generation, png, zip, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Swiss Post Parcel domestic barcodes for a set of
/// 18‑digit codes and saves each barcode image as a PNG file inside a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcode images and writes them to a temporary ZIP file.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of 18‑digit Swiss Post Parcel domestic codes.
        List<string> codes = new List<string>
        {
            "983412345612345678",
            "983412345612345679",
            "983412345612345680",
            "983412345612345681",
            "983412345612345682"
        };

        // Determine a temporary file path for the resulting ZIP archive.
        string zipPath = Path.Combine(Path.GetTempPath(), "SwissPostBarcodes.zip");

        // Create the ZIP file and add each generated barcode image as a separate entry.
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Iterate over each code, generate its barcode, and store it in the archive.
                foreach (string code in codes)
                {
                    // Initialize the barcode generator for the Swiss Post Parcel symbology.
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                    {
                        // Configure visual parameters: X‑dimension and bar height in pixels.
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;
                        generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                        // Render the barcode to a memory stream in PNG format.
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading.

                            // Create a new entry in the ZIP archive named after the code.
                            ZipArchiveEntry entry = archive.CreateEntry($"{code}.png");
                            using (Stream entryStream = entry.Open())
                            {
                                // Copy the PNG data into the ZIP entry.
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        // Inform the user where the ZIP archive has been saved.
        Console.WriteLine($"Barcodes saved to ZIP archive: {zipPath}");
    }
}