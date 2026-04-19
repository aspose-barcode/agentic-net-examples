using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        // Define input Excel file and output folder
        string inputFile = "Barcodes.xlsx";
        string outputFolder = "BarcodesOutput";

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input Excel file does not exist, create a sample file with a few rows
        if (!File.Exists(inputFile))
        {
            CreateSampleExcel(inputFile);
        }

        // Load the workbook
        using (var workbook = new Workbook(inputFile))
        {
            var worksheet = workbook.Worksheets[0];
            var cells = worksheet.Cells;

            // Assume first row (index 0) contains a header, start from row 1
            int startRow = 1;
            int totalRows = cells.MaxDataRow + 1; // MaxDataRow is zero‑based

            for (int row = startRow; row < totalRows; row++)
            {
                // Read the code text from the first column (A)
                string codeText = cells[row, 0].StringValue?.Trim();

                if (string.IsNullOrEmpty(codeText))
                {
                    continue; // Skip empty rows
                }

                // Generate QR code and save as PNG
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Set high error correction level
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    // Build a safe file name
                    string safeFileName = GetSafeFileName($"{row}_{codeText}.png");
                    string outputPath = Path.Combine(outputFolder, safeFileName);

                    // Save the barcode image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Creates a simple Excel file with sample data
    private static void CreateSampleExcel(string filePath)
    {
        using (var wb = new Workbook())
        {
            var sheet = wb.Worksheets[0];
            sheet.Cells[0, 0].PutValue("CodeText"); // Header

            // Sample rows (5 items)
            sheet.Cells[1, 0].PutValue("https://example.com/1");
            sheet.Cells[2, 0].PutValue("https://example.com/2");
            sheet.Cells[3, 0].PutValue("https://example.com/3");
            sheet.Cells[4, 0].PutValue("https://example.com/4");
            sheet.Cells[5, 0].PutValue("https://example.com/5");

            wb.Save(filePath);
        }
    }

    // Replaces invalid filename characters with underscore
    private static string GetSafeFileName(string fileName)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(c, '_');
        }
        return fileName;
    }
}