// Title: Generate Swiss Post Parcel domestic barcodes and package them into a ZIP file
// Description: Demonstrates creating 18‑digit Swiss Post Parcel barcodes, validating input, and saving each barcode as a PNG image inside a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel, image export via BarCodeImageFormat.Png, and .NET ZipArchive for bundling outputs. Developers often need to batch‑create barcodes for shipping labels and archive them for distribution or storage.
// Prompt: Generate Swiss Post Parcel domestic barcodes for a list of 18‑digit codes, writing them to a ZIP archive.
// Tags: swisspostparcel, barcode generation, png, zip, aspose.barcode, barcodegenerator, ziparchive

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates Swiss Post Parcel domestic barcodes from a list of 18‑digit codes
/// and stores the resulting PNG images in a ZIP archive.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, validates codes, and writes them to a ZIP file.
    /// </summary>
    public static void Main()
    {
        // Sample list of 18‑digit Swiss Post Parcel domestic codes
        var codes = new List<string>
        {
            "123456789012345678",
            "987654321098765432",
            "111111111111111111",
            "222222222222222222",
            "333333333333333333"
        };

        // Destination ZIP file path
        string zipPath = "SwissPostParcelBarcodes.zip";

        // Remove existing archive if present to avoid conflicts
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a new ZIP archive and add generated barcode images
        using (var zipFile = new FileStream(zipPath, FileMode.CreateNew))
        {
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Create, leaveOpen: false))
            {
                int index = 1;
                foreach (var code in codes)
                {
                    // Validate that the code consists of exactly 18 digits
                    if (code == null || code.Length != 18 || !IsAllDigits(code))
                    {
                        Console.WriteLine($"Skipping invalid code: {code}");
                        continue;
                    }

                    // Generate barcode image in memory using Aspose.BarCode
                    using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                    {
                        using (var imageStream = new MemoryStream())
                        {
                            // Save the barcode as PNG to the memory stream
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0;

                            // Define entry name with zero‑padded index
                            string entryName = $"Barcode_{index:D3}.png";

                            // Create a new entry in the ZIP archive
                            var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                            using (var entryStream = entry.Open())
                            {
                                // Copy the PNG data into the ZIP entry
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }

                    index++;
                }
            }
        }

        Console.WriteLine($"Barcodes have been saved to '{zipPath}'.");
    }

    // Helper method to ensure a string contains only digit characters
    private static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}