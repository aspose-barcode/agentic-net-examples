// Title: Batch decode Swiss Post Parcel barcodes and generate Excel report
// Description: This example creates Swiss Post Parcel barcode images, decodes them in a batch, and writes the results to an Excel spreadsheet.
// Category-Description: The sample belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator, BarCodeReader, and related classes to produce barcodes, perform batch decoding, and export data with Aspose.Cells. Typical use cases include bulk barcode processing, quality assessment, and reporting for logistics or postal applications.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: barcode, swisspost, batch decoding, excel, aspose.barcode, aspose.cells, generation, recognition, report

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;

/// <summary>
/// Demonstrates batch decoding of Swiss Post Parcel barcodes and creating an Excel report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them, and writes results to an Excel file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and the report
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchSwissPost_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Swiss Post Parcel domestic identifiers
        var codes = new List<string>
        {
            "98.34.123456.12345678",
            "983412345612345678",
            "98.12.000001.00000001"
        };

        var barcodeFiles = new List<string>();

        // Generate barcode images for each sample code
        for (int i = 0; i < codes.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codes[i]))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare list to hold decoding results
        var results = new List<(string File, string CodeText, string CodeType, double Quality)>();

        // Batch decode each generated barcode image
        foreach (string file in barcodeFiles)
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
                    BarCodeResult[] barCodes = reader.ReadBarCodes();
                    if (barCodes.Length > 0)
                    {
                        var result = barCodes[0];
                        results.Add((Path.GetFileName(file), result.CodeText, result.CodeTypeName, result.ReadingQuality));
                    }
                    else
                    {
                        // No barcode detected in the image
                        results.Add((Path.GetFileName(file), string.Empty, "NotDetected", 0));
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode
                Console.WriteLine($"Failed to read {file}: {ex.Message}");
                results.Add((Path.GetFileName(file), string.Empty, "Error", 0));
            }
        }

        // Create an Excel workbook to hold the report
        string reportPath = Path.Combine(tempFolder, "SwissPostReport.xlsx");
        var workbook = new Workbook();
        var sheet = workbook.Worksheets[0];
        sheet.Name = "Report";

        // Write header row
        sheet.Cells[0, 0].PutValue("File");
        sheet.Cells[0, 1].PutValue("Code Text");
        sheet.Cells[0, 2].PutValue("Code Type");
        sheet.Cells[0, 3].PutValue("Reading Quality");

        // Write data rows
        for (int i = 0; i < results.Count; i++)
        {
            var r = results[i];
            int row = i + 1;
            sheet.Cells[row, 0].PutValue(r.File);
            sheet.Cells[row, 1].PutValue(r.CodeText);
            sheet.Cells[row, 2].PutValue(r.CodeType);
            sheet.Cells[row, 3].PutValue(r.Quality);
        }

        // Save the Excel report
        workbook.Save(reportPath, SaveFormat.Xlsx);
        Console.WriteLine($"Excel report generated at: {reportPath}");
    }
}