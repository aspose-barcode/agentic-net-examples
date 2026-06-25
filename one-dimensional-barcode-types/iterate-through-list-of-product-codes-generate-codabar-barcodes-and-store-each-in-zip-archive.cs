using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Codabar barcodes for a list of product codes and packages them into a zip file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product codes to be encoded as barcodes.
        List<string> productCodes = new List<string>
        {
            "A12345",
            "B67890",
            "C23456",
            "D78901",
            "E34567"
        };

        // Create a memory stream that will hold the zip archive in memory.
        using (var zipStream = new MemoryStream())
        {
            // Initialize a new zip archive that writes into the memory stream.
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                // Iterate over each product code and generate a barcode image.
                foreach (string code in productCodes)
                {
                    // Create a barcode generator for the Codabar symbology using the current code.
                    using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                    {
                        // Store the generated barcode image in a temporary memory stream (PNG format).
                        using (var imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            // Reset the stream position to the beginning before reading.
                            imageStream.Position = 0;

                            // Create a new entry in the zip archive for this barcode image.
                            ZipArchiveEntry entry = archive.CreateEntry($"{code}.png");
                            // Open the entry's stream and copy the image data into it.
                            using (var entryStream = entry.Open())
                            {
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }

            // Persist the in‑memory zip archive to a physical file on disk.
            File.WriteAllBytes("CodabarBarcodes.zip", zipStream.ToArray());
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine("Codabar barcodes have been generated and saved to CodabarBarcodes.zip");
    }
}