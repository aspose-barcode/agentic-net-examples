// Title: Batch decode HIBC LIC barcodes from TIFF images to CSV
// Description: Demonstrates how to generate sample HIBC LIC QR barcodes, save them as TIFF files, decode them in bulk, and export the extracted data to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating HIBC LIC barcodes and BarCodeReader for batch decoding multiple images. Developers working with healthcare or logistics barcodes often need to automate extraction of detailed product information and export results for downstream processing.
// Prompt: Batch decode a folder of TIFF images containing HIBC LIC barcodes and export results to a CSV file.
// Tags: hibc, lic, batch, decode, csv, aspose.barcode, complexbarcode, barcoderecognition, barcodegeneration

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides a sample that generates HIBC LIC QR barcodes, saves them as TIFF images,
/// decodes all images in a folder, and writes the extracted data to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Executes the generation, decoding, and CSV export workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchHIBCLIC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated image file paths
        List<string> imageFiles = new List<string>();

        // Generate sample HIBC LIC QR barcodes (primary data) and save as TIFF
        for (int i = 1; i <= 5; i++)
        {
            // Prepare primary data codetext
            HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = "P" + i.ToString("D3"),
                    LabelerIdentificationCode = "L" + i.ToString("D3"),
                    UnitOfMeasureID = i
                }
            };

            // Generate barcode image
            string filePath = Path.Combine(tempFolder, $"HIBCLIC_{i}.tiff");
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            imageFiles.Add(filePath);
        }

        // Prepare CSV output file with header row
        string csvPath = Path.Combine(tempFolder, "Results.csv");
        string header = "FileName,CodeText,ProductOrCatalogNumber,LabelerIdentificationCode,UnitOfMeasureID,ExpiryDate,Quantity,LotNumber,SerialNumber,DateOfManufacture";
        File.WriteAllText(csvPath, header + Environment.NewLine);

        // Decode each image using all supported barcode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        // Attempt to decode HIBC LIC specific data
                        HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        string product = "";
                        string labeler = "";
                        string unit = "";
                        string expiry = "";
                        string quantity = "";
                        string lot = "";
                        string serial = "";
                        string manuf = "";

                        if (complex is HIBCLICPrimaryDataCodetext primary)
                        {
                            product = primary.Data?.ProductOrCatalogNumber ?? "";
                            labeler = primary.Data?.LabelerIdentificationCode ?? "";
                            unit = primary.Data?.UnitOfMeasureID.ToString() ?? "";
                        }
                        else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                        {
                            expiry = secondary.Data?.ExpiryDate.ToString("yyyy-MM-dd") ?? "";
                            quantity = secondary.Data?.Quantity.ToString() ?? "";
                            lot = secondary.Data?.LotNumber ?? "";
                            serial = secondary.Data?.SerialNumber ?? "";
                            manuf = secondary.Data?.DateOfManufacture.ToString("yyyy-MM-dd") ?? "";
                        }
                        else if (complex is HIBCLICCombinedCodetext combined)
                        {
                            product = combined.PrimaryData?.ProductOrCatalogNumber ?? "";
                            labeler = combined.PrimaryData?.LabelerIdentificationCode ?? "";
                            unit = combined.PrimaryData?.UnitOfMeasureID.ToString() ?? "";
                            expiry = combined.SecondaryAndAdditionalData?.ExpiryDate.ToString("yyyy-MM-dd") ?? "";
                            quantity = combined.SecondaryAndAdditionalData?.Quantity.ToString() ?? "";
                            lot = combined.SecondaryAndAdditionalData?.LotNumber ?? "";
                            serial = combined.SecondaryAndAdditionalData?.SerialNumber ?? "";
                            manuf = combined.SecondaryAndAdditionalData?.DateOfManufacture.ToString("yyyy-MM-dd") ?? "";
                        }

                        // Build CSV line with proper escaping
                        string csvLine = $"{Path.GetFileName(imagePath)},{EscapeCsv(result.CodeText)},{EscapeCsv(product)},{EscapeCsv(labeler)},{EscapeCsv(unit)},{EscapeCsv(expiry)},{EscapeCsv(quantity)},{EscapeCsv(lot)},{EscapeCsv(serial)},{EscapeCsv(manuf)}";
                        File.AppendAllText(csvPath, csvLine + Environment.NewLine);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(imagePath)}: {ex.Message}");
            }
        }

        Console.WriteLine($"Batch decoding completed. CSV saved to: {csvPath}");
    }

    // Simple CSV escaping for commas, quotes, and newlines
    static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}