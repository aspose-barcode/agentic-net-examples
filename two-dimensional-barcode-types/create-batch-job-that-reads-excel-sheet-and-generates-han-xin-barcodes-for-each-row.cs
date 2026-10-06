// Title: Batch Han Xin Barcode Generation from Excel
// Description: Demonstrates reading an Excel worksheet, creating a Han Xin barcode for each non‑empty cell in the first column, saving the images as PNG files, and embedding the barcode images back into the worksheet.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells batch processing category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) together with Workbook and Worksheet (Aspose.Cells) to generate barcodes, save them to files, and insert them as pictures. Typical scenarios include bulk barcode creation for inventory, shipping labels, or data export where each row requires a unique barcode. Developers often need to automate reading data sources, generating barcodes, and updating documents in a single workflow.
// Prompt: Create a batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
// Tags: hanxin, barcode, batch, excel, aspose.cells, aspose.barcode, png, image generation, embedding

using System;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads an Excel file, generates Han Xin barcodes for each row,
/// saves the barcode images, and inserts them back into the worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Prepare a sample Excel file if it does not already exist
        string excelPath = Path.Combine(Path.GetTempPath(), "HanXinBatch.xlsx");
        if (!File.Exists(excelPath))
        {
            CreateSampleExcel(excelPath);
        }

        // Create a unique output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Load the workbook and get the first worksheet
        Workbook workbook = new Workbook(excelPath);
        Worksheet sheet = workbook.Worksheets[0];

        // Determine the last row that contains data (assume first column holds the text to encode)
        int maxRow = sheet.Cells.MaxDataRow;
        for (int row = 0; row <= maxRow; row++)
        {
            // Read the text from column A (index 0)
            string codeText = sheet.Cells[row, 0].StringValue;
            if (string.IsNullOrWhiteSpace(codeText))
                continue; // Skip empty rows

            // Generate a Han Xin barcode for the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Use automatic encoding mode and set error correction level to L2
                generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Save the barcode image to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Write the image file to the output folder
                    string imagePath = Path.Combine(outputFolder, $"barcode_row{row}.png");
                    using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fileStream);
                    }

                    // Insert the barcode image into column B of the same row
                    int pictureColumn = 1; // Column B (zero‑based index)
                    int pictureIndex = sheet.Pictures.Add(row, pictureColumn, ms);
                    Picture picture = sheet.Pictures[pictureIndex];
                    picture.Placement = PlacementType.FreeFloating;
                }
            }
        }

        // Save the workbook with the embedded barcode images
        string updatedExcelPath = Path.Combine(Path.GetTempPath(), "HanXinBatch_Updated.xlsx");
        workbook.Save(updatedExcelPath, SaveFormat.Xlsx);

        // Inform the user where the results are stored
        Console.WriteLine("Barcode generation completed.");
        Console.WriteLine("Images saved to: " + outputFolder);
        Console.WriteLine("Updated Excel saved to: " + updatedExcelPath);
    }

    /// <summary>
    /// Creates a simple Excel file with sample data for barcode generation.
    /// </summary>
    /// <param name="path">Full path where the workbook will be saved.</param>
    private static void CreateSampleExcel(string path)
    {
        Workbook wb = new Workbook();
        Worksheet ws = wb.Worksheets[0];
        ws.Cells[0, 0].PutValue("ABC123");
        ws.Cells[1, 0].PutValue("汉字测试");
        ws.Cells[2, 0].PutValue("https://example.com");
        wb.Save(path, SaveFormat.Xlsx);
    }
}