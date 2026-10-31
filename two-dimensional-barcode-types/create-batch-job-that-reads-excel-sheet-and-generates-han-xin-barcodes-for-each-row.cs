// Title: Generate Han Xin barcodes from Excel rows in batch
// Description: Demonstrates reading an Excel sheet, creating a Han Xin barcode for each cell in column A, and inserting the barcode image into column B.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells batch processing category, showcasing how to combine spreadsheet data handling with barcode generation. It uses BarcodeGenerator (Aspose.BarCode) and Workbook/Pictures (Aspose.Cells) to automate barcode creation for multiple records, a common requirement for inventory, shipping, or document tagging scenarios.
// Prompt: Create a batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
// Tags: hanxin, batch, excel, image, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Han Xin barcodes from an Excel file and embedding them back into the sheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary Excel file (if missing), reads each row, generates a barcode, and saves the result.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for input and output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define full paths for the sample input and the resulting output workbook
        string inputPath = Path.Combine(tempFolder, "input.xlsx");
        string outputPath = Path.Combine(tempFolder, "output.xlsx");

        // If the sample Excel file does not exist, generate one with test data in column A
        if (!File.Exists(inputPath))
        {
            var wb = new Workbook();
            var ws = wb.Worksheets[0];
            ws.Cells[0, 0].PutValue("Data");
            ws.Cells[1, 0].PutValue("Sample123");
            ws.Cells[2, 0].PutValue("HelloWorld");
            ws.Cells[3, 0].PutValue("Aspose2024");
            ws.Cells[4, 0].PutValue("BarcodeTest");
            wb.Save(inputPath);
        }

        // Load the workbook that contains the data to be encoded
        var workbook = new Workbook(inputPath);
        var worksheet = workbook.Worksheets[0];

        // Determine the first and last rows that contain data in column A (index 0)
        int firstRow = worksheet.Cells.GetFirstDataRow(0);
        int lastRow = worksheet.Cells.GetLastDataRow(0);
        if (firstRow < 0) firstRow = 0;
        if (lastRow < 0) lastRow = 0;

        // Iterate over each populated row and generate a Han Xin barcode for the cell value
        for (int row = firstRow; row <= lastRow; row++)
        {
            var cell = worksheet.Cells[row, 0];
            if (cell == null) continue;

            string codeText = cell.StringValue;
            if (string.IsNullOrEmpty(codeText)) continue;

            // Generate the barcode image and store it in a memory stream
            using (var ms = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    // Configure optional Han Xin parameters: auto mode, error correction level L2, and auto version
                    generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
                    generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
                    generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                    // Save the barcode as PNG into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position before inserting the image
                ms.Position = 0;

                // Add the barcode picture to column B (index 1) of the current row
                int pictureIndex = worksheet.Pictures.Add(row, 1, ms);
                var picture = worksheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.FreeFloating;

                // Optionally set a fixed size for the inserted barcode image
                picture.Width = 150;
                picture.Height = 150;
            }
        }

        // Save the modified workbook containing the barcode images
        workbook.Save(outputPath, SaveFormat.Xlsx);

        // Inform the user about the locations of the input and output files
        Console.WriteLine("Barcode batch job completed.");
        Console.WriteLine("Input file : " + inputPath);
        Console.WriteLine("Output file: " + outputPath);
    }
}