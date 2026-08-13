// Title: Batch barcode generation from Excel rows
// Description: Demonstrates reading an Excel file, extracting each row's first column as barcode text, and generating PNG barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode for .NET batch processing category, illustrating how to combine Aspose.Cells and Aspose.BarCode APIs to automate barcode creation from tabular data. It shows loading a workbook, iterating over used rows, configuring a BarcodeGenerator (e.g., Code128), and saving images. Developers often need to generate large numbers of barcodes from databases or spreadsheets for inventory, shipping, or labeling workflows.
// Prompt: Batch generate barcodes from an Excel spreadsheet, using each row’s value as CodeText and exporting PNG files.
// Tags: barcode, batch, excel, code128, png, aspose.cells, aspose.barcode, generation

using System;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from an Excel file, saving each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads an Excel file, creates output folder, generates barcodes for each non‑empty cell in the first column, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define input Excel path and output folder for barcode images
        string excelPath = "input.xlsx";
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the Excel file does not exist, create a sample workbook with example data
        if (!File.Exists(excelPath))
        {
            CreateSampleExcel(excelPath);
        }

        // Load the workbook from the specified Excel file
        Workbook workbook = new Workbook(excelPath);
        Worksheet sheet = workbook.Worksheets[0];

        // Determine the last row that contains data
        int maxRow = sheet.Cells.MaxDataRow;

        // Iterate through each row up to the last used row
        for (int row = 0; row <= maxRow; row++)
        {
            // Read the first column value of the current row as the barcode text
            string codeText = sheet.Cells[row, 0].StringValue?.Trim();

            // Skip rows where the cell is empty or contains only whitespace
            if (string.IsNullOrEmpty(codeText))
            {
                continue;
            }

            // Create a barcode generator configured for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeText;

                // Build the output file name (e.g., ABC001.png) and full path
                string fileName = $"{codeText}.png";
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the generated barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Helper method to create a sample Excel file with a few rows of data
    private static void CreateSampleExcel(string path)
    {
        var wb = new Workbook();
        var ws = wb.Worksheets[0];

        // Sample barcode values to populate the first column
        string[] sampleCodes = { "ABC001", "ABC002", "ABC003", "ABC004", "ABC005" };

        for (int i = 0; i < sampleCodes.Length; i++)
        {
            ws.Cells[i, 0].PutValue(sampleCodes[i]);
        }

        // Save the workbook as an XLSX file
        wb.Save(path, SaveFormat.Xlsx);
        Console.WriteLine($"Sample Excel file created at '{path}'.");
    }
}