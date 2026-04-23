using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace SwissPostParcelBarcodeGenerator
{
    class Program
    {
        static void Main()
        {
            // Sample list of 18‑digit Swiss Post Parcel domestic codes.
            var parcelCodes = new List<string>
            {
                "123456789012345678",
                "987654321098765432",
                "111111111111111111",
                "222222222222222222",
                "333333333333333333"
            };

            // Output ZIP file path.
            const string zipPath = "SwissPostParcelBarcodes.zip";

            // Create the ZIP archive and add each barcode image.
            using (var zipFile = new FileStream(zipPath, FileMode.Create))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
            {
                foreach (var code in parcelCodes)
                {
                    // Generate the Swiss Post Parcel barcode for the current code.
                    using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                    {
                        // Generate the barcode image.
                        using (Bitmap bitmap = generator.GenerateBarCodeImage())
                        {
                            // Save the image to a memory stream in PNG format.
                            using (var imageStream = new MemoryStream())
                            {
                                bitmap.Save(imageStream, ImageFormat.Png);
                                imageStream.Seek(0, SeekOrigin.Begin);

                                // Create a ZIP entry named after the code.
                                var entry = archive.CreateEntry($"{code}.png");

                                // Write the image data into the ZIP entry.
                                using (var entryStream = entry.Open())
                                {
                                    imageStream.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Barcodes have been generated and saved to '{zipPath}'.");
        }
    }
}