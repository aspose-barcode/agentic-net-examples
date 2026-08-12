// Title: Generate Han Xin barcodes from Excel rows
// Description: Demonstrates how to read data from an Excel worksheet and create a Han Xin barcode image for each entry, embedding the images into a new Excel file.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells integration category, showing how to combine barcode generation (BarcodeGenerator, EncodeTypes.HanXin) with spreadsheet manipulation (Workbook, Worksheet, Pictures). Typical use cases include batch processing of product codes, inventory lists, or any tabular data that requires QR‑like barcodes. Developers often need to automate barcode creation, embed images into cells, and export the result as an Excel document.
// Prompt: Create a batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
// Tags: hanxin, barcode, excel, batch, image, aspose.barcode, aspose.cells, png, generation

using System;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that reads an Excel file, generates Han Xin barcodes for each row,
/// and writes the barcodes into a new Excel workbook.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes up to 10 rows from 'input.xlsx', creates barcode images,
    /// and saves the result to 'output.xlsx'.
    /// </summary>
    static void Main()
    {
        // Paths for input and output Excel files
        string inputPath = "input.xlsx";
        string outputPath = "output.xlsx";

        // Create a temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Ensure there is an input Excel file; if missing, create a sample one
        if (!File.Exists(inputPath))
        {
            CreateSampleExcel(inputPath);
            Console.WriteLine($"Sample input file created at '{inputPath}'.");
        }

        // Load the input workbook
        using (var workbook = new Workbook(inputPath))
        {
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;

            // Create a new workbook for output (could also modify the same workbook)
            using (var outWorkbook = new Workbook())
            {
                Worksheet outSheet = outWorkbook.Worksheets[0];
                Cells outCells = outSheet.Cells;

                // Process up to 10 rows to keep the example safe
                int maxRows = Math.Min(10, cells.MaxDataRow + 1);
                for (int row = 0; row < maxRows; row++)
                {
                    // Assume the data to encode is in column A (index 0)
                    string codeText = cells[row, 0]?.StringValue;
                    if (string.IsNullOrEmpty(codeText))
                        continue;

                    // Generate Han Xin barcode image and save to a memory stream
                    using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                    {
                        // Example: set error correction level to L2
                        generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                        using (Bitmap bitmap = generator.GenerateBarCodeImage())
                        {
                            using (var ms = new MemoryStream())
                            {
                                // Save bitmap as PNG into the memory stream
                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Position = 0;

                                // Save image file (optional, for inspection)
                                string imagePath = Path.Combine(tempFolder, $"barcode_{row}.png");
                                File.WriteAllBytes(imagePath, ms.ToArray());

                                // Add the barcode picture to the output sheet at column B (index 1)
                                int pictureIndex = outSheet.Pictures.Add(row, 1, ms);
                                Picture picture = outSheet.Pictures[pictureIndex];
                                picture.Placement = PlacementType.FreeFloating;
                            }
                        }
                    }

                    // Copy the original code text to column A of the output sheet
                    outCells[row, 0].PutValue(codeText);
                }

                // Save the output workbook
                outWorkbook.Save(outputPath, SaveFormat.Xlsx);
                Console.WriteLine($"Barcodes generated and saved to '{outputPath}'.");
            }
        }

        // Clean up temporary folder (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the folder is in the temp area.
        }
    }

    // Helper to create a simple Excel file with sample data
    private static void CreateSampleExcel(string path)
    {
        using (var wb = new Workbook())
        {
            Worksheet ws = wb.Worksheets[0];
            Cells cells = ws.Cells;

            string[] samples = new string[]
            {
                "1234567890",
                "ABCDEF",
                "HanXinDemo",
                "https://example.com",
                "测试中文"
            };

            for (int i = 0; i < samples.Length; i++)
            {
                cells[i, 0].PutValue(samples[i]);
            }

            wb.Save(path, SaveFormat.Xlsx);
        }
    }
}