using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading code texts (from an Excel file or fallback data) and generating
/// Code128 bar‑code images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads code texts, creates an output folder, and generates PNG bar‑code images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Define the path to the Excel file that should contain one column
        //    with the code texts.
        // --------------------------------------------------------------------
        string excelPath = "input.xlsx";

        // --------------------------------------------------------------------
        // 2. Prepare a list to hold the code texts read from the Excel file.
        //    If the file cannot be read, sample data will be used instead.
        // --------------------------------------------------------------------
        List<string> codeTexts = new List<string>();

        if (File.Exists(excelPath))
        {
            // NOTE: In a full implementation Aspose.Cells would be used to read the
            //       Excel file. The required package is not available in this
            //       environment, so the actual reading code is shown as a comment.
            /*
            // using Aspose.Cells;
            // var workbook = new Workbook(excelPath);
            // var sheet = workbook.Worksheets[0];
            // int rowCount = sheet.Cells.MaxDataRow + 1;
            // for (int i = 0; i < rowCount; i++)
            // {
            //     var cell = sheet.Cells[i, 0];
            //     if (cell != null && cell.Value != null)
            //         codeTexts.Add(cell.StringValue);
            // }
            */

            Console.WriteLine("Excel file found, but Aspose.Cells is not available in this environment.");
            Console.WriteLine("Falling back to sample data.");
        }

        // --------------------------------------------------------------------
        // 3. If no data was loaded from Excel, populate the list with fallback
        //    sample values.
        // --------------------------------------------------------------------
        if (codeTexts.Count == 0)
        {
            codeTexts.AddRange(new[]
            {
                "Sample001",
                "Sample002",
                "Sample003",
                "Sample004",
                "Sample005"
            });
        }

        // --------------------------------------------------------------------
        // 4. Ensure the output directory for the generated bar‑code images exists.
        // --------------------------------------------------------------------
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // 5. Iterate over each code text, validate it, and generate a PNG bar‑code.
        // --------------------------------------------------------------------
        int index = 1;
        foreach (string codeText in codeTexts)
        {
            // Skip empty or whitespace-only entries.
            if (string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping empty code text at row {index}.");
                index++;
                continue;
            }

            // Build the full path for the output image file.
            string outputPath = Path.Combine(outputDir, $"barcode_{index}.png");

            // Create a barcode generator using Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set optional parameters, e.g., image resolution (300 DPI).
                generator.Parameters.Resolution = 300f;

                // Save the generated bar‑code as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {index}: {outputPath}");
            index++;
        }

        // --------------------------------------------------------------------
        // 6. Signal completion of the bar‑code generation process.
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode generation completed.");
    }
}