// Title: Generate Swiss Post Parcel Barcodes and Save to ZIP
// Description: Demonstrates generating Swiss Post Parcel domestic barcodes from 18‑digit codes and storing the PNG images in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator with EncodeTypes.SwissPostParcel. Typical use cases include batch creation of postal barcodes for shipping labels and archiving them for downstream processing. Developers often need to configure barcode dimensions, render images, and package results, which this snippet illustrates.
// Prompt: Generate Swiss Post Parcel domestic barcodes for a list of 18‑digit codes, writing them to a ZIP archive.
// Tags: swisspost, parcel, barcode, generation, zip, png, aspose.barcode, encode-types

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates Swiss Post Parcel barcodes from a set of 18‑digit codes
/// and writes the resulting PNG images into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, saves them as PNGs,
    /// and packages them into a ZIP file in the current directory.
    /// </summary>
    static void Main()
    {
        // Define a sample list of 18‑digit Swiss Post Parcel domestic codes (must start with 98 or 99)
        List<string> codes = new List<string>
        {
            "983412345612345678",
            "983412345612345679",
            "983412345612345680",
            "983412345612345681",
            "983412345612345682"
        };

        // Determine the full path for the output ZIP archive
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostBarcodes.zip");

        // Remove any existing ZIP file to avoid conflicts
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a new ZIP archive and add each generated barcode image as an entry
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
        {
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i];

                // Initialize the barcode generator for Swiss Post Parcel symbology
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                {
                    // Configure visual parameters: X-dimension and bar height
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                    // Render the barcode to a memory stream in PNG format
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Create a new entry in the ZIP archive for this barcode image
                        string entryName = $"barcode_{i + 1}.png";
                        ZipArchiveEntry entry = archive.CreateEntry(entryName);

                        // Copy the PNG data into the ZIP entry
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        // Inform the user about the successful generation
        Console.WriteLine($"Generated {codes.Count} Swiss Post Parcel barcodes into '{zipPath}'.");
    }
}