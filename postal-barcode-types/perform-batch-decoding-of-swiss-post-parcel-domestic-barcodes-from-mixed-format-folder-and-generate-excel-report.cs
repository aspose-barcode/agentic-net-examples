using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "InputBarcodes");
        string outputReport = Path.Combine(Environment.CurrentDirectory, "SwissPostParcelReport.xlsx");

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed a few sample Swiss Post Parcel barcodes if folder is empty
        SeedSampleBarcodes(inputFolder);

        // Collect decoding results
        var results = new List<DecodeResult>();

        // Get all files in the folder (common image and PDF extensions)
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string filePath in files)
        {
            if (!File.Exists(filePath))
                continue; // Skip missing files

            // Use BarCodeReader to decode Swiss Post Parcel barcodes
            using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
            {
                // Optional: enable checksum validation for postal barcodes
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes in the image/PDF
                foreach (var result in reader.ReadBarCodes())
                {
                    results.Add(new DecodeResult
                    {
                        FileName = Path.GetFileName(filePath),
                        BarcodeType = result.CodeTypeName,
                        CodeText = result.CodeText,
                        Confidence = result.Confidence.ToString()
                    });
                }
            }
        }

        // Generate Excel report using Aspose.Cells
        var workbook = new Workbook();
        var sheet = workbook.Worksheets[0];
        // Header row
        sheet.Cells[0, 0].PutValue("File Name");
        sheet.Cells[0, 1].PutValue("Barcode Type");
        sheet.Cells[0, 2].PutValue("Code Text");
        sheet.Cells[0, 3].PutValue("Confidence");

        // Data rows
        for (int i = 0; i < results.Count; i++)
        {
            var r = results[i];
            int row = i + 1;
            sheet.Cells[row, 0].PutValue(r.FileName);
            sheet.Cells[row, 1].PutValue(r.BarcodeType);
            sheet.Cells[row, 2].PutValue(r.CodeText);
            sheet.Cells[row, 3].PutValue(r.Confidence);
        }

        // Save the Excel file
        workbook.Save(outputReport);
        Console.WriteLine($"Report generated: {outputReport}");
    }

    // Helper method to create a few sample barcode images
    private static void SeedSampleBarcodes(string folder)
    {
        // If there are already files, do not overwrite
        if (Directory.GetFiles(folder).Length > 0)
            return;

        // Sample code texts (these are arbitrary valid examples)
        string[] sampleTexts = { "123456789012", "987654321098", "555555555555", "111111111111", "222222222222" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string fileName = Path.Combine(folder, $"Sample_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleTexts[i]))
            {
                // Ensure a reasonable image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(fileName);
            }
        }
    }

    // Simple DTO to hold decoding information
    private class DecodeResult
    {
        public string FileName { get; set; }
        public string BarcodeType { get; set; }
        public string CodeText { get; set; }
        public string Confidence { get; set; }
    }
}