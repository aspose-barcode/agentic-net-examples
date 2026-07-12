// Title: Batch decode Swiss Post Parcel barcodes and export to Excel
// Description: Demonstrates how to read Swiss Post Parcel domestic barcodes from various image and PDF files in a folder and generate an Excel report summarizing the results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases the use of BarCodeReader for decoding SwissPostParcel symbology, BarcodeGenerator for creating sample barcodes, and Aspose.Cells for building an Excel report. Typical scenarios include bulk barcode validation, inventory auditing, and automated data extraction where developers need to process mixed‑format files and produce structured outputs.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: barcode, swisspostparcel, batch-decoding, excel, aspose.barcode, aspose.cells, report

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of Swiss Post Parcel barcodes from a mixed‑format folder
/// and generates an Excel report containing the decoded information.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO to hold barcode information for the report.
    /// </summary>
    private class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public int Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public string Region { get; set; }
    }

    /// <summary>
    /// Entry point of the example. Scans the input folder, decodes Swiss Post Parcel barcodes,
    /// and writes the results to an Excel workbook.
    /// </summary>
    static void Main()
    {
        // Define input folder containing mixed‑format images/PDFs
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "InputBarcodes");
        // Define output Excel report path
        string reportPath = Path.Combine(Environment.CurrentDirectory, "BarcodeReport.xlsx");

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed a sample Swiss Post Parcel barcode image if the folder is empty
        SeedSampleIfEmpty(inputFolder);

        // Supported file extensions for barcode scanning
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".tif", ".tiff", ".pdf" };
        var files = new List<string>();
        foreach (var ext in extensions)
        {
            // Collect files with the current extension (non‑recursive)
            files.AddRange(Directory.GetFiles(inputFolder, "*" + ext, SearchOption.TopDirectoryOnly));
        }

        // Limit processing to a safe number of files (max 10) to avoid long runtimes
        int maxFiles = Math.Min(10, files.Count);
        var results = new List<BarcodeInfo>();

        // Iterate over each file and decode barcodes
        for (int i = 0; i < maxFiles; i++)
        {
            string filePath = files[i];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a reader configured for Swiss Post Parcel barcodes
            using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
            {
                // Read all barcodes present in the file
                foreach (var result in reader.ReadBarCodes())
                {
                    var info = new BarcodeInfo
                    {
                        FileName = Path.GetFileName(filePath),
                        CodeType = result.CodeTypeName,
                        CodeText = result.CodeText ?? string.Empty,
                        Confidence = (int)result.Confidence,
                        ReadingQuality = result.ReadingQuality,
                        Region = $"X={result.Region.Rectangle.X},Y={result.Region.Rectangle.Y},W={result.Region.Rectangle.Width},H={result.Region.Rectangle.Height}"
                    };
                    results.Add(info);
                }
            }
        }

        // Generate Excel report using Aspose.Cells
        using (var workbook = new Workbook())
        {
            var sheet = workbook.Worksheets[0];

            // Write header row
            sheet.Cells[0, 0].PutValue("File Name");
            sheet.Cells[0, 1].PutValue("Barcode Type");
            sheet.Cells[0, 2].PutValue("Code Text");
            sheet.Cells[0, 3].PutValue("Confidence");
            sheet.Cells[0, 4].PutValue("Reading Quality");
            sheet.Cells[0, 5].PutValue("Region (X,Y,W,H)");

            // Write data rows
            for (int row = 0; row < results.Count; row++)
            {
                var info = results[row];
                int excelRow = row + 1;
                sheet.Cells[excelRow, 0].PutValue(info.FileName);
                sheet.Cells[excelRow, 1].PutValue(info.CodeType);
                sheet.Cells[excelRow, 2].PutValue(info.CodeText);
                sheet.Cells[excelRow, 3].PutValue(info.Confidence);
                sheet.Cells[excelRow, 4].PutValue(info.ReadingQuality);
                sheet.Cells[excelRow, 5].PutValue(info.Region);
            }

            // Auto‑fit columns for better readability
            sheet.AutoFitColumns();

            // Save the report to the specified path
            workbook.Save(reportPath);
        }

        Console.WriteLine($"Barcode decoding completed. Report saved to: {reportPath}");
    }

    /// <summary>
    /// Generates a sample Swiss Post Parcel barcode image if the input folder contains no files.
    /// </summary>
    /// <param name="folderPath">Path to the folder where the sample image will be created.</param>
    private static void SeedSampleIfEmpty(string folderPath)
    {
        var existingFiles = Directory.GetFiles(folderPath);
        if (existingFiles.Length > 0)
            return;

        // Sample codetext for a domestic Swiss Post Parcel barcode (example format)
        string sampleCodeText = "1234567890123456789012345";

        string sampleImagePath = Path.Combine(folderPath, "SampleSwissPostParcel.png");

        // Generate the sample barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleCodeText))
        {
            // Use auto‑size mode for simplicity
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            // Save as PNG
            generator.Save(sampleImagePath);
        }

        Console.WriteLine($"Sample barcode image created at: {sampleImagePath}");
    }
}