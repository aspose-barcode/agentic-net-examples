// Title: Generate Codabar Barcodes and Package into a ZIP Archive
// Description: Demonstrates iterating over a list of product codes, creating Codabar barcode images, and storing each image in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.Codabar, configure barcode parameters, save images in PNG format, and archive them using System.IO.Compression. Developers working with barcode creation, image export, and file packaging will find this pattern useful for batch processing scenarios.
// Prompt: Iterate through a list of product codes, generate Codabar barcodes, and store each in a zip archive.
// Tags: codabar, barcode, generation, zip, aspose.barcode, png

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that creates Codabar barcode images from product codes
/// and packages them into a ZIP archive using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcode images for each product code and writes them to a ZIP file.
    /// </summary>
    static void Main()
    {
        // Define sample product codes that already include Codabar start/stop characters.
        List<string> productCodes = new List<string>
        {
            "A12345A",
            "B67890B",
            "C24680C",
            "D13579D"
        };

        // Determine a temporary path for the resulting ZIP archive.
        string zipPath = Path.Combine(Path.GetTempPath(), "CodabarBarcodes.zip");

        // Remove any existing archive with the same name to avoid conflicts.
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a new ZIP archive for storing the generated barcode images.
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.CreateNew))
        using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            int index = 1; // Counter for naming entries sequentially.

            // Iterate through each product code and generate a barcode image.
            foreach (string code in productCodes)
            {
                // Initialize the barcode generator for Codabar symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                {
                    // Explicitly set start/stop symbols (optional, shown for clarity).
                    generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                    generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                    // Render the barcode to a memory stream in PNG format.
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a new entry in the ZIP archive for this barcode image.
                        string entryName = $"Barcode_{index}.png";
                        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Copy the image data into the ZIP entry.
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }

                index++;
            }
        }

        // Inform the user where the ZIP archive has been created.
        Console.WriteLine($"Zip archive created at: {zipPath}");
    }
}