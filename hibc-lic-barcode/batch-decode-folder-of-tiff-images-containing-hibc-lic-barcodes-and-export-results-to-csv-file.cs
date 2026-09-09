// Title: Batch decode HIBC LIC barcodes from TIFF images and export results to CSV
// Description: Demonstrates how to generate sample HIBC LIC barcode TIFF files, read them in bulk, decode the complex codetext, and write the extracted data to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating HIBC LIC barcodes, BarCodeReader with DecodeType.HIBCQRLIC for batch decoding, and handling of ComplexCodetextReader to extract primary data fields. Developers working with healthcare barcodes, bulk image processing, or data export to CSV will find these APIs useful.
// Prompt: Batch decode a folder of TIFF images containing HIBC LIC barcodes and export results to a CSV file.
// Tags: hibc, lic, tiff, csv, batch, decode, complexbarcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of HIBC LIC barcodes from TIFF images and exporting the results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, decodes them, and writes the extracted data to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample HIBC LIC barcode images (TIFF) and collect their file paths
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"HIBCLIC_{i}.tif");

            // Set up primary data codetext for the HIBC LIC barcode
            HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = $"P{i + 1000}",
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Generate the barcode image and save it as TIFF
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 10;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            imageFiles.Add(filePath);
        }

        // Prepare the CSV output file
        string csvPath = Path.Combine(tempFolder, "Results.csv");
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeText,ProductOrCatalogNumber,LabelerIdentificationCode,UnitOfMeasureID");

            // Process each generated image file
            foreach (string imageFile in imageFiles)
            {
                if (!File.Exists(imageFile))
                {
                    Console.WriteLine($"File not found: {imageFile}");
                    continue;
                }

                try
                {
                    // Initialize barcode reader for HIBC LIC QR codes
                    using (BarCodeReader reader = new BarCodeReader(imageFile, DecodeType.HIBCQRLIC))
                    {
                        // Iterate through all detected barcodes in the image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Decode the complex HIBC LIC codetext
                            HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                            string product = "";
                            string labeler = "";
                            string uom = "";

                            // Extract primary data fields if present
                            if (complex is HIBCLICPrimaryDataCodetext primary)
                            {
                                product = primary.Data.ProductOrCatalogNumber;
                                labeler = primary.Data.LabelerIdentificationCode;
                                uom = primary.Data.UnitOfMeasureID.ToString();
                            }

                            // Write a CSV line for the current barcode (escape commas if needed)
                            string line = $"{Path.GetFileName(imageFile)},{result.CodeText},{product},{labeler},{uom}";
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Skipping unreadable file: {imageFile}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imageFile}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Decoding completed. CSV file saved at: {csvPath}");
    }
}