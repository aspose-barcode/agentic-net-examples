// Title: Decode HIBC LIC Primary Data Barcodes from a Directory
// Description: Demonstrates generating sample HIBC QR LIC barcodes, iterating over a folder of images, decoding each barcode, and logging the primary product identifier.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on complex barcode types such as HIBC QR LIC. It showcases the use of ComplexBarcodeGenerator for encoding, BarCodeReader for decoding, and the ComplexCodetextReader utility to extract structured data. Developers working with healthcare or logistics barcodes often need to batch‑process images to retrieve product or catalog numbers, making this pattern a common requirement.
// Prompt: Iterate over a directory of barcode images, decode each HIBC LIC, and log primary product IDs.
// Tags: hibc, lic, barcode, decoding, batch, aspose.barcode, complexbarcode, primarydata

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating HIBC QR LIC barcodes, decoding them from a temporary directory,
/// and outputting the primary product or catalog numbers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample barcodes, reads them back, and logs product identifiers.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a dedicated temporary folder for the sample images
        string folder = Path.Combine(Path.GetTempPath(), "HIBCLICBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        // Generate sample HIBC LIC primary data barcodes and store their file paths
        List<string> files = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            var complexCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = $"P{i:D3}",
                    LabelerIdentificationCode = $"L{i:D3}",
                    UnitOfMeasureID = i
                }
            };

            string filePath = Path.Combine(folder, $"HIBCLICPrimary_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Adjust visual density of the barcode
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Save(filePath);
            }
            files.Add(filePath);
        }

        // Decode each barcode file and log the primary product or catalog number
        foreach (var file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.HIBCQRLIC))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        var codetext = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        var primary = codetext as HIBCLICPrimaryDataCodetext;
                        if (primary?.Data != null)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - Product or catalog number: {primary.Data.ProductOrCatalogNumber}");
                        }
                        else
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - No primary data decoded.");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Optional cleanup: uncomment to delete the temporary folder after execution
        // Directory.Delete(folder, true);
    }
}