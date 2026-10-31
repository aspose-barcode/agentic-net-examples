// Title: Batch decode Swiss Post Parcel barcodes and export to Excel
// Description: Demonstrates how to generate sample Swiss Post Parcel barcode images, decode them in bulk, and create an Excel report summarizing the results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showing how to use BarcodeGenerator, BarCodeReader, and Aspose.Cells to work with multiple barcode images. Typical use cases include automated scanning of shipping labels, validation of barcode data, and reporting. Developers often need to generate, read, and export barcode information to spreadsheets for analysis or integration with other systems.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: swisspost, parcel, barcode, batch decoding, excel, report, aspose.barcode, aspose.cells, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Drawing;

/// <summary>
/// Example program that creates sample Swiss Post Parcel barcode images,
/// decodes them in a batch, and writes the results to an Excel report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder for the sample barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchSwissPost_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Define sample barcode data (Swiss Post Parcel Domestic) with
        //    desired file names and image formats.
        // --------------------------------------------------------------------
        var samples = new List<(string CodeText, string FileName, BarCodeImageFormat Format)>
        {
            ("98.34.123456.12345678", "DomesticOriginal.png", BarCodeImageFormat.Png),
            ("983412345612345678", "DomesticDigits.png", BarCodeImageFormat.Png),
            ("98.12.000001.00000001", "DomesticOriginal2.jpg", BarCodeImageFormat.Jpeg),
            ("981200000100000001", "DomesticDigits2.jpg", BarCodeImageFormat.Jpeg)
        };

        var createdFiles = new List<string>();

        // --------------------------------------------------------------------
        // 3. Generate barcode images using BarcodeGenerator.
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sample.CodeText))
            {
                // Set visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Save the barcode image to the specified format.
                generator.Save(filePath, sample.Format);
            }
            createdFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 4. Prepare a collection to hold rows for the Excel report.
        // --------------------------------------------------------------------
        var reportRows = new List<(string FileName, string BarcodeType, string BarcodeData)>();

        // --------------------------------------------------------------------
        // 5. Decode each generated barcode image using BarCodeReader.
        // --------------------------------------------------------------------
        foreach (string file in createdFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length > 0)
                    {
                        // Use the first detected barcode result.
                        var result = results[0];
                        reportRows.Add((Path.GetFileName(file), result.CodeTypeName, result.CodeText));
                    }
                    else
                    {
                        // No barcode detected in the image.
                        reportRows.Add((Path.GetFileName(file), "NotDetected", ""));
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle image loading errors (e.g., unsupported format).
                Console.WriteLine($"Failed to load image '{file}': {ex.Message}");
                reportRows.Add((Path.GetFileName(file), "LoadError", ""));
            }
        }

        // --------------------------------------------------------------------
        // 6. Create an Excel workbook and populate it with the report data.
        // --------------------------------------------------------------------
        string reportPath = Path.Combine(tempFolder, "SwissPostBatchReport.xlsx");
        var workbook = new Workbook();
        var sheet = workbook.Worksheets[0];
        sheet.Name = "Report";

        // Write header row.
        sheet.Cells[0, 0].PutValue("File Name");
        sheet.Cells[0, 1].PutValue("Barcode Type");
        sheet.Cells[0, 2].PutValue("Barcode Data");

        // Write each data row.
        for (int i = 0; i < reportRows.Count; i++)
        {
            var row = reportRows[i];
            sheet.Cells[i + 1, 0].PutValue(row.FileName);
            sheet.Cells[i + 1, 1].PutValue(row.BarcodeType);
            sheet.Cells[i + 1, 2].PutValue(row.BarcodeData);
        }

        // Save the workbook as an XLSX file.
        workbook.Save(reportPath, SaveFormat.Xlsx);
        Console.WriteLine($"Excel report generated at: {reportPath}");
    }
}