// Title: Decode HIBC LIC Barcodes from a Directory and Log Product IDs
// Description: Demonstrates how to generate sample HIBC LIC barcode images, iterate through a folder, decode each barcode, and output the primary product identifier.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on complex barcode types such as HIBC Code 128 LIC. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader to create, read, and parse HIBC LIC barcodes. Developers working with healthcare or logistics labeling often need to extract product or catalog numbers from HIBC barcodes, and this snippet provides a clear pattern for batch processing image files.
// Prompt: Iterate over a directory of barcode images, decode each HIBC LIC, and log primary product IDs.
// Tags: hibc, lic, barcode, decoding, csharp, aspose.barcode, complexbarcode, batch-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that generates HIBC LIC barcodes (if none exist), scans a directory for image files,
/// decodes each barcode, and writes the primary product ID to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample barcodes, then reads and decodes all supported image files in the
    /// "Barcodes" subfolder, outputting the extracted product identifiers.
    /// </summary>
    static void Main()
    {
        // Define the folder that will contain barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Seed sample HIBC LIC barcodes if the folder is empty
        bool folderEmpty = Directory.GetFiles(folderPath, "*.png").Length == 0 &&
                           Directory.GetFiles(folderPath, "*.jpg").Length == 0 &&
                           Directory.GetFiles(folderPath, "*.bmp").Length == 0;

        if (folderEmpty)
        {
            var samples = new[]
            {
                new { Product = "12345", Labeler = "A999", Unit = 1 },
                new { Product = "67890", Labeler = "B123", Unit = 2 },
                new { Product = "54321", Labeler = "C456", Unit = 3 }
            };

            int index = 1;
            foreach (var s in samples)
            {
                // Build primary data codetext for HIBC LIC
                var primaryData = new PrimaryData
                {
                    ProductOrCatalogNumber = s.Product,
                    LabelerIdentificationCode = s.Labeler,
                    UnitOfMeasureID = s.Unit
                };

                var complexCodetext = new HIBCLICPrimaryDataCodetext
                {
                    BarcodeType = EncodeTypes.HIBCCode128LIC,
                    Data = primaryData
                };

                // Save the generated barcode image
                string fileName = Path.Combine(folderPath, $"HIBC_{index}.png");
                using (var generator = new ComplexBarcodeGenerator(complexCodetext))
                {
                    generator.Save(fileName, BarCodeImageFormat.Png);
                }

                index++;
            }
        }

        // Process each image file in the folder
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };
        foreach (string pattern in patterns)
        {
            foreach (string filePath in Directory.GetFiles(folderPath, pattern))
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Read and decode HIBC LIC barcodes from the current image
                using (var reader = new BarCodeReader(filePath, DecodeType.HIBCCode128LIC))
                {
                    bool anyFound = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyFound = true;

                        // Decode the complex HIBC LIC codetext
                        var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        if (complex is HIBCLICPrimaryDataCodetext primary)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(filePath)} - Product ID: {primary.Data.ProductOrCatalogNumber}");
                        }
                        else
                        {
                            Console.WriteLine($"File: {Path.GetFileName(filePath)} - Unable to extract primary product ID.");
                        }
                    }

                    if (!anyFound)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - No barcode detected.");
                    }
                }
            }
        }
    }
}