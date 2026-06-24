using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating GS1 Code 128 barcodes, saving them as PNG files,
/// packaging them into a ZIP archive, and cleaning up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images from sample data, zips them, and outputs the archive path.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 data (AI format)
        var dataList = new List<string>
        {
            "(01)12345678901231",
            "(01)98765432109876",
            "(01)55555555555555",
            "(01)11111111111111",
            "(01)22222222222222"
        };

        // Directory to store temporary PNG files
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Generate PNG images for each data string
        var pngFiles = new List<string>();
        foreach (var data in dataList)
        {
            // Build a safe file name by removing parentheses and spaces
            string fileName = $"barcode_{data.Replace("(", "").Replace(")", "").Replace(" ", "")}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Create a barcode generator for GS1 Code 128
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, data))
            {
                // Optional: show checksum in human‑readable text
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode directly as a PNG file
                generator.Save(filePath);
            }

            // Keep track of the generated file path for later zipping
            pngFiles.Add(filePath);
        }

        // Path for the final ZIP archive containing all PNGs
        string zipPath = Path.Combine(outputDir, "Gs1Barcodes.zip");

        // Ensure any existing ZIP archive is removed before creating a new one
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a ZIP archive and add each PNG file as an entry
        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (var png in pngFiles)
            {
                // Add the PNG file to the archive using its file name only
                zip.CreateEntryFromFile(png, Path.GetFileName(png));
            }
        }

        // Clean up temporary PNG files (optional)
        foreach (var png in pngFiles)
        {
            File.Delete(png);
        }

        // Inform the user where the ZIP archive was created
        Console.WriteLine($"Generated ZIP archive at: {zipPath}");
    }
}