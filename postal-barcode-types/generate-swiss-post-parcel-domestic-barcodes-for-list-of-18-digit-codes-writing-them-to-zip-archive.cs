// Title: Generate Swiss Post Parcel Domestic Barcodes and Save to ZIP
// Description: Demonstrates how to create Swiss Post Parcel domestic barcodes from a list of 18‑digit identifiers and store the PNG images in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It illustrates typical bulk barcode creation, image formatting, and archiving workflows that developers often need when integrating barcode output into document management or shipping systems.
// Prompt: Generate Swiss Post Parcel domestic barcodes for a list of 18‑digit codes, writing them to a ZIP archive.
// Tags: barcode, swisspostparcel, bulk generation, zip, png, aspose.barcode, c#

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that generates Swiss Post Parcel domestic barcodes
/// and writes them to a ZIP archive as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for a predefined set of 18‑digit codes
    /// and packages them as PNG files in a ZIP archive.
    /// </summary>
    static void Main()
    {
        // Define a sample list of 18‑digit Swiss Post Parcel domestic codes.
        List<string> codes = new List<string>
        {
            "983412345612345678",
            "983412345612345679",
            "983412345612345680",
            "983412345612345681",
            "983412345612345682"
        };

        // Determine the full path for the output ZIP file in the current directory.
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostBarcodes.zip");

        // Create a FileStream for the ZIP archive.
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        {
            // Initialize a ZipArchive in create mode.
            using (ZipArchive zip = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                // Iterate over each code and generate a barcode image.
                foreach (string code in codes)
                {
                    // Initialize the barcode generator for Swiss Post Parcel symbology.
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                    {
                        // Configure visual parameters: X-dimension and bar height.
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;
                        generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                        // Render the barcode to a memory stream in PNG format.
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading.

                            // Create a new entry in the ZIP archive for this barcode image.
                            ZipArchiveEntry entry = zip.CreateEntry($"{code}.png");
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

        // Inform the user about the successful generation.
        Console.WriteLine($"Generated {codes.Count} barcodes and saved to '{zipPath}'.");
    }
}