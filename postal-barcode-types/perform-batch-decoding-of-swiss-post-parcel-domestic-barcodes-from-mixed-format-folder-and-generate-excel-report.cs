// Title: Batch decode Swiss Post Parcel barcodes and generate Excel report
// Description: Demonstrates generating Swiss Post Parcel barcode images in various formats, decoding them in bulk, and creating an Excel summary of the results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and Aspose.Cells to handle multiple image files, decode specific symbologies, and produce structured reports. Developers often need to process large sets of barcode images, extract data, and export findings to spreadsheets for analysis or integration with business workflows.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: swisspostparcel, barcode, batch decoding, excel, aspose.barcode, aspose.cells, image generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;

/// <summary>
/// Example program that generates Swiss Post Parcel barcode images, decodes them in batch,
/// and writes the results to an Excel report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs image generation, batch decoding, and report creation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchDecode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the generated barcode image file paths.
        List<string> barcodeFiles = new List<string>();

        // Sample Swiss Post Parcel domestic barcode texts.
        string[] sampleTexts = new string[]
        {
            "98.34.123456.12345678",
            "983412345612345678",
            "99.12.654321.87654321"
        };

        // Define image formats to be used for the generated barcodes.
        string[] formats = new string[] { "png", "jpg", "bmp" };

        // Generate barcode images in the specified formats.
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string text = sampleTexts[i];
            string format = formats[i % formats.Length];
            string filePath = Path.Combine(tempFolder, $"SwissPost_{i + 1}.{format}");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Configure barcode appearance.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Map file extension to Aspose.BarCode image format.
                BarCodeImageFormat imgFormat = format.ToLower() switch
                {
                    "png" => BarCodeImageFormat.Png,
                    "jpg" => BarCodeImageFormat.Jpeg,
                    "bmp" => BarCodeImageFormat.Bmp,
                    _ => BarCodeImageFormat.Png
                };

                // Save the generated barcode image.
                generator.Save(filePath, imgFormat);
            }

            barcodeFiles.Add(filePath);
        }

        // Prepare the Excel report workbook.
        string reportPath = Path.Combine(tempFolder, "BatchDecodeReport.xlsx");
        using (Workbook workbook = new Workbook())
        {
            Worksheet sheet = workbook.Worksheets[0];

            // Write header row.
            sheet.Cells[0, 0].PutValue("File Name");
            sheet.Cells[0, 1].PutValue("Barcode Type");
            sheet.Cells[0, 2].PutValue("Decoded Text");

            int currentRow = 1;

            // Process each generated barcode file.
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    // Initialize reader for Swiss Post Parcel symbology.
                    using (BarCodeReader reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcode detected in the image.
                            sheet.Cells[currentRow, 0].PutValue(Path.GetFileName(file));
                            sheet.Cells[currentRow, 1].PutValue("N/A");
                            sheet.Cells[currentRow, 2].PutValue("No barcode detected");
                            currentRow++;
                        }
                        else
                        {
                            // Write each detected barcode to the report.
                            foreach (BarCodeResult result in results)
                            {
                                sheet.Cells[currentRow, 0].PutValue(Path.GetFileName(file));
                                sheet.Cells[currentRow, 1].PutValue(result.CodeTypeName);
                                sheet.Cells[currentRow, 2].PutValue(result.CodeText);
                                currentRow++;
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handle image loading failures or unsupported formats.
                    Console.WriteLine($"Failed to read '{file}': {ex.Message}");
                    sheet.Cells[currentRow, 0].PutValue(Path.GetFileName(file));
                    sheet.Cells[currentRow, 1].PutValue("Error");
                    sheet.Cells[currentRow, 2].PutValue(ex.Message);
                    currentRow++;
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors during processing.
                    Console.WriteLine($"Unexpected error processing '{file}': {ex.Message}");
                    sheet.Cells[currentRow, 0].PutValue(Path.GetFileName(file));
                    sheet.Cells[currentRow, 1].PutValue("Error");
                    sheet.Cells[currentRow, 2].PutValue(ex.Message);
                    currentRow++;
                }
            }

            // Save the Excel report to the temporary folder.
            workbook.Save(reportPath, SaveFormat.Xlsx);
        }

        Console.WriteLine($"Batch decoding completed. Report saved to: {reportPath}");
    }
}