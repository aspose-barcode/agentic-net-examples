using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Input Excel file path (fallback to a sample file if not provided)
        string inputFile = "Barcodes.xlsx";
        // Output folder for generated PNG files
        string outputFolder = "GeneratedBarcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input Excel file does not exist, create a small sample workbook
        if (!File.Exists(inputFile))
        {
            using (var wb = new Workbook())
            {
                var sheet = wb.Worksheets[0];
                sheet.Cells[0, 0].PutValue("ABC123");
                sheet.Cells[1, 0].PutValue("DEF456");
                sheet.Cells[2, 0].PutValue("GHI789");
                sheet.Cells[3, 0].PutValue("JKL012");
                sheet.Cells[4, 0].PutValue("MNO345");
                wb.Save(inputFile);
            }
        }

        // Load the Excel workbook
        using (var workbook = new Workbook(inputFile))
        {
            var sheet = workbook.Worksheets[0];
            int maxRows = Math.Min(5, sheet.Cells.MaxDataRow + 1); // Process up to 5 rows for safety

            for (int row = 0; row < maxRows; row++)
            {
                var cell = sheet.Cells[row, 0];
                if (cell == null || cell.Value == null)
                {
                    continue; // Skip empty rows
                }

                string codeText = cell.StringValue.Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    continue; // Skip rows with empty text
                }

                // Generate barcode using Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.CodeText = codeText;
                    // Optional: set barcode color (default is black)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    // Save as PNG file
                    string outputPath = Path.Combine(outputFolder, $"barcode_{row + 1}.png");
                    generator.Save(outputPath);
                }
            }
        }
    }
}