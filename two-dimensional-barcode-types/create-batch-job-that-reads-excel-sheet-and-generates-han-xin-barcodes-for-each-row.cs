// Title: Batch generate Han Xin barcodes from Excel rows
// Description: This example reads text values from the first column of an Excel worksheet, generates a Han Xin barcode for each entry, and embeds the barcode image into the adjacent column.
// Category-Description: Demonstrates how to combine Aspose.Cells and Aspose.BarCode to automate barcode creation within spreadsheets. It covers loading a workbook, iterating rows, using BarcodeGenerator (EncodeTypes.HanXin), saving images, and inserting pictures via Aspose.Cells.Drawing. Ideal for developers needing bulk barcode generation for inventory, shipping, or tracking applications.
// Prompt: Create a batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
// Tags: hanxin, barcode, batch, excel, aspose.cells, aspose.barcode, image, png, generation

using System;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch processing of an Excel file to generate and embed Han Xin barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads an input Excel file, creates barcode images for each row,
    /// inserts them into the worksheet, and saves the result.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "HanXinBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the input and output Excel files
        string excelPath = Path.Combine(tempDir, "Input.xlsx");
        string outputExcelPath = Path.Combine(tempDir, "Output.xlsx");

        // Create a sample Excel file if it does not already exist
        if (!File.Exists(excelPath))
        {
            using (var wb = new Workbook())
            {
                var ws = wb.Worksheets[0];
                ws.Cells[0, 0].PutValue("Sample Text 1");
                ws.Cells[1, 0].PutValue("Sample Text 2");
                ws.Cells[2, 0].PutValue("Sample Text 3");
                ws.Cells[3, 0].PutValue("Sample Text 4");
                ws.Cells[4, 0].PutValue("Sample Text 5");
                wb.Save(excelPath, SaveFormat.Xlsx);
            }
        }

        // Load the Excel workbook for processing
        using (var workbook = new Workbook(excelPath))
        {
            var worksheet = workbook.Worksheets[0];
            int lastRow = worksheet.Cells.MaxDataRow;

            // Limit processing to the first 5 rows for safety
            int rowsToProcess = Math.Min(lastRow + 1, 5);
            for (int row = 0; row < rowsToProcess; row++)
            {
                var cell = worksheet.Cells[row, 0];
                if (cell == null || cell.Value == null)
                    continue;

                string codeText = cell.StringValue;
                if (string.IsNullOrWhiteSpace(codeText))
                    continue;

                // Generate a Han Xin barcode image for the cell value
                string imageFile = Path.Combine(tempDir, $"barcode_{row}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
                    generator.Save(imageFile, BarCodeImageFormat.Png);
                }

                // Insert the generated barcode image into column B of the same row
                using (FileStream imgStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read))
                {
                    int pictureIndex = worksheet.Pictures.Add(row, 1, imgStream);
                    Picture picture = worksheet.Pictures[pictureIndex];
                    picture.Placement = PlacementType.FreeFloating;
                }
            }

            // Save the modified workbook containing the embedded barcodes
            workbook.Save(outputExcelPath, SaveFormat.Xlsx);
        }

        Console.WriteLine("Batch processing completed.");
        Console.WriteLine($"Input Excel: {excelPath}");
        Console.WriteLine($"Output Excel with barcodes: {outputExcelPath}");
    }
}