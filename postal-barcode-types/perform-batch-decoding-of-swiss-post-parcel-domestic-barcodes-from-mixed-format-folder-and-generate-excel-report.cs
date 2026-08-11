// Title: Batch decode Swiss Post Parcel barcodes and generate Excel report
// Description: Demonstrates how to decode Swiss Post Parcel domestic barcodes from image files in a folder and compile the results into an Excel spreadsheet.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating the use of BarcodeGenerator, BarCodeReader, and Aspose.Cells to generate barcode images, perform bulk decoding, and export results to Excel. Typical use cases include processing large sets of shipping labels, validating barcode data, and creating reports for logistics operations. Developers often need to combine barcode generation, recognition, and spreadsheet output in automated workflows.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: swisspostparcel, decoding, excel, aspose.barcode, aspose.cells

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates sample Swiss Post Parcel barcodes, decodes them in bulk,
/// and writes the decoding results to an Excel report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Define source folder for barcode images and destination path for report
        // --------------------------------------------------------------------
        string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelReport.xlsx");

        // Ensure the barcode folder exists
        if (!Directory.Exists(baseFolder))
        {
            Directory.CreateDirectory(baseFolder);
        }

        // --------------------------------------------------------------
        // Sample data representing Swiss Post Parcel barcode values
        // --------------------------------------------------------------
        var sampleData = new List<string>
        {
            "1234567890123456",
            "9876543210987654",
            "5555555555555555"
        };

        // --------------------------------------------------------------
        // Generate PNG barcode images for each sample value (if not already present)
        // --------------------------------------------------------------
        foreach (var codeText in sampleData)
        {
            string filePath = Path.Combine(baseFolder, $"SwissPost_{codeText}.png");
            if (File.Exists(filePath))
                continue;

            // Resolve EncodeTypes.SwissPostParcel via reflection (API may not expose it directly)
            var encodeField = typeof(EncodeTypes).GetField("SwissPostParcel");
            if (encodeField == null)
            {
                Console.WriteLine("EncodeTypes does not contain SwissPostParcel. Skipping generation.");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)encodeField.GetValue(null);

            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------
        // Prepare a collection to store decoding results
        // --------------------------------------------------------------
        var results = new List<DecodeResult>();

        // --------------------------------------------------------------
        // Scan the folder for PNG files and decode each using SwissPostParcel symbology
        // --------------------------------------------------------------
        var imageFiles = Directory.GetFiles(baseFolder, "*.png");
        foreach (var imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
                continue;

            BaseDecodeType decodeType = DecodeType.SwissPostParcel;
            using (BarCodeReader reader = new BarCodeReader(imageFile, decodeType))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    results.Add(new DecodeResult
                    {
                        FileName = Path.GetFileName(imageFile),
                        CodeText = result.CodeText ?? string.Empty,
                        Symbology = result.CodeTypeName,
                        Confidence = result.Confidence.ToString()
                    });
                }
            }
        }

        // --------------------------------------------------------------
        // Create an Excel workbook and populate it with the decoding data
        // --------------------------------------------------------------
        using (Workbook workbook = new Workbook())
        {
            Worksheet sheet = workbook.Worksheets[0];

            // Header row
            sheet.Cells[0, 0].PutValue("File Name");
            sheet.Cells[0, 1].PutValue("Code Text");
            sheet.Cells[0, 2].PutValue("Symbology");
            sheet.Cells[0, 3].PutValue("Confidence");

            // Data rows
            for (int i = 0; i < results.Count; i++)
            {
                var r = results[i];
                int row = i + 1;
                sheet.Cells[row, 0].PutValue(r.FileName);
                sheet.Cells[row, 1].PutValue(r.CodeText);
                sheet.Cells[row, 2].PutValue(r.Symbology);
                sheet.Cells[row, 3].PutValue(r.Confidence);
            }

            // Save the workbook to the specified path
            workbook.Save(reportPath, SaveFormat.Xlsx);
        }

        Console.WriteLine($"Decoding completed. Report saved to: {reportPath}");
    }

    // Simple DTO to hold decoding information
    private class DecodeResult
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string Symbology { get; set; }
        public string Confidence { get; set; }
    }
}