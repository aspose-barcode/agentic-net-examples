// Title: Generate Swiss Post Parcel Barcodes and Package into ZIP
// Description: Demonstrates how to create Swiss Post Parcel domestic barcodes from 18‑digit codes and store the PNG images in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.SwissPostParcel to produce barcode images. Typical use cases include batch creation of shipping labels, parcel tracking codes, and integration with logistics workflows. Developers often need to generate multiple barcodes, choose image formats, and archive results for distribution or storage.
// Prompt: Generate Swiss Post Parcel domestic barcodes for a list of 18‑digit codes, writing them to a ZIP archive.
// Tags: swisspostparcel, barcode-generation, png, zip, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes for a set of 18‑digit codes
/// and writes the resulting PNG images into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, packages them into a ZIP file,
    /// and writes the archive to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample list of 18‑digit Swiss Post Parcel codes (domestic)
        List<string> parcelCodes = new List<string>
        {
            "123456789012345678",
            "987654321098765432",
            "111111111111111111",
            "222222222222222222",
            "333333333333333333"
        };

        // Prepare a memory stream that will hold the ZIP archive in memory
        using (MemoryStream zipStream = new MemoryStream())
        {
            // Create the ZIP archive in write mode; leave the stream open after disposing the archive
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                // Iterate over each parcel code and generate a barcode image
                foreach (string code in parcelCodes)
                {
                    // Initialize the barcode generator for Swiss Post Parcel symbology
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                    {
                        // Save the generated barcode to a memory stream in PNG format
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading

                            // Create a new entry in the ZIP archive for this barcode image
                            ZipArchiveEntry entry = zip.CreateEntry($"{code}.png");
                            using (Stream entryStream = entry.Open())
                            {
                                // Copy the PNG image data into the ZIP entry
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }

            // Define the output path for the ZIP archive
            string zipPath = "SwissPostParcelBarcodes.zip";

            // Write the in‑memory ZIP archive to a file on disk
            using (FileStream file = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
            {
                zipStream.Position = 0; // Ensure we start copying from the beginning
                zipStream.CopyTo(file);
            }

            // Inform the user where the ZIP archive was created
            Console.WriteLine($"ZIP archive created: {Path.GetFullPath(zipPath)}");
        }
    }
}