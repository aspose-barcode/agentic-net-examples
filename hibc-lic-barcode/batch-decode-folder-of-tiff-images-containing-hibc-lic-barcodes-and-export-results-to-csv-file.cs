// Title: Batch decode HIBC LIC barcodes from TIFF files and export to CSV
// Description: Demonstrates how to read multiple TIFF images containing HIBC LIC barcodes, extract their data, and write the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on complex barcode types such as HIBC LIC. It shows how to use BarCodeReader with DecodeType.HIBCQRLIC, ComplexCodetextReader, and the HIBCLICCombinedCodetext model to parse detailed product information. Developers often need to batch‑process scanned images and export structured data for inventory or regulatory reporting.
// Prompt: Batch decode a folder of TIFF images containing HIBC LIC barcodes and export results to a CSV file.
// Tags: hibc lic, barcode decoding, tiff, csv export, batch processing, aspose.barcode, complex barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates batch decoding of HIBC LIC barcodes from TIFF images and exporting the extracted data to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, decodes them, and writes results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchHIBCLIC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample HIBC LIC combined barcode images (TIFF)
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"HIBCLIC_{i}.tiff");
            GenerateSampleBarcode(filePath, i);
            imageFiles.Add(filePath);
        }

        // Prepare CSV output with header line
        string csvPath = Path.Combine(tempFolder, "Results.csv");
        var sb = new StringBuilder();
        sb.AppendLine("FileName,ProductOrCatalogNumber,LabelerIdentificationCode,UnitOfMeasureID,ExpiryDate,Quantity,LotNumber,SerialNumber,DateOfManufacture");

        // Decode each generated image
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"File not found: {imgPath}");
                continue;
            }

            try
            {
                // Specify the decode type for HIBC QR LIC barcodes
                BaseDecodeType decodeType = DecodeType.HIBCQRLIC;
                using (BarCodeReader reader = new BarCodeReader(imgPath, decodeType))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Attempt to parse the complex HIBC LIC codetext
                        HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        if (complex is HIBCLICCombinedCodetext combined)
                        {
                            // Build a CSV line with the extracted fields
                            string line = $"{Path.GetFileName(imgPath)}," +
                                          $"{combined.PrimaryData.ProductOrCatalogNumber}," +
                                          $"{combined.PrimaryData.LabelerIdentificationCode}," +
                                          $"{combined.PrimaryData.UnitOfMeasureID}," +
                                          $"{combined.SecondaryAndAdditionalData.ExpiryDate:yyyy-MM-dd}," +
                                          $"{combined.SecondaryAndAdditionalData.Quantity}," +
                                          $"{combined.SecondaryAndAdditionalData.LotNumber}," +
                                          $"{combined.SecondaryAndAdditionalData.SerialNumber}," +
                                          $"{combined.SecondaryAndAdditionalData.DateOfManufacture:yyyy-MM-dd}";
                            sb.AppendLine(line);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file due to load error: {imgPath} - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {imgPath}: {ex.Message}");
            }
        }

        // Write the accumulated CSV content to disk
        File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"Decoding completed. CSV saved to: {csvPath}");
    }

    /// <summary>
    /// Generates a sample HIBC LIC combined barcode image and saves it as a TIFF file.
    /// </summary>
    /// <param name="filePath">Full path where the TIFF image will be saved.</param>
    /// <param name="index">Numeric index used to vary the sample data.</param>
    static void GenerateSampleBarcode(string filePath, int index)
    {
        // Create combined HIBC LIC codetext object
        HIBCLICCombinedCodetext combined = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,

            // Primary data fields
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = $"P{index:D4}",
                LabelerIdentificationCode = $"L{index:D3}",
                UnitOfMeasureID = index
            },

            // Secondary and additional data fields
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Today.AddDays(30 * index),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10 * index,
                LotNumber = $"LOT{index:D3}",
                SerialNumber = $"SN{index:D5}",
                DateOfManufacture = DateTime.Today.AddDays(-10 * index)
            }
        };

        // Generate the barcode image using the complex barcode generator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combined))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 5f;
            generator.Save(filePath, BarCodeImageFormat.Tiff);
        }
    }
}