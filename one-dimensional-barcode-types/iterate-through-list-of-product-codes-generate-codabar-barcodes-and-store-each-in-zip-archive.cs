using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample product codes (without start/stop symbols)
        string[] productCodes = new string[]
        {
            "12345",
            "67890",
            "24680",
            "13579",
            "11223"
        };

        // Output zip file path
        string zipPath = "CodabarBarcodes.zip";

        // Create the zip archive
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                foreach (string code in productCodes)
                {
                    // Codabar requires start/stop symbols; using default 'A' symbol
                    string fullCode = $"A{code}A";

                    // Generate Codabar barcode
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar))
                    {
                        generator.CodeText = fullCode;

                        // Generate the barcode image as a bitmap
                        using (Bitmap bitmap = generator.GenerateBarCodeImage())
                        {
                            // Save bitmap to a memory stream in PNG format
                            using (MemoryStream imageStream = new MemoryStream())
                            {
                                bitmap.Save(imageStream, ImageFormat.Png);
                                imageStream.Position = 0;

                                // Create a zip entry for this barcode image
                                ZipArchiveEntry entry = archive.CreateEntry($"{code}.png");

                                // Write the image data into the zip entry
                                using (Stream entryStream = entry.Open())
                                {
                                    imageStream.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Zip archive created at: {Path.GetFullPath(zipPath)}");
    }
}