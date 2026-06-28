using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Swiss Post Parcel barcodes for a list of codes and packages them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images and writes them to a ZIP file.
    /// </summary>
    static void Main()
    {
        // Define a sample list of 18‑digit Swiss Post Parcel codes (domestic)
        List<string> codes = new List<string>
        {
            "123456789012345678",
            "987654321098765432",
            "111111111111111111",
            "222222222222222222",
            "333333333333333333"
        };

        // Specify the output ZIP file path
        string zipPath = "SwissPostParcelBarcodes.zip";

        // Create the ZIP archive and add each generated barcode image
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
        {
            // Iterate over each parcel code
            foreach (string code in codes)
            {
                // Generate the barcode image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                {
                    // Enforce strict validation of the codetext (throws if invalid)
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Save the barcode as a PNG image into the memory stream
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Create a ZIP entry named after the parcel code with a .png extension
                    ZipArchiveEntry entry = archive.CreateEntry($"{code}.png");

                    // Write the image bytes from the memory stream into the ZIP entry
                    using (Stream entryStream = entry.Open())
                    {
                        imageStream.CopyTo(entryStream);
                    }
                }
            }
        }

        // Output the full path of the generated ZIP archive
        Console.WriteLine($"Generated ZIP archive: {Path.GetFullPath(zipPath)}");
    }
}