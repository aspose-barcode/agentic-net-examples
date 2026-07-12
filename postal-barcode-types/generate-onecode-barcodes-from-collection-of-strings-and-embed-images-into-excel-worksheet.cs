// Title: Generate OneCode Barcodes and Embed into Excel
// Description: Demonstrates creating OneCode barcodes from numeric strings and inserting them as images into an Excel worksheet using Aspose.BarCode and Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Cells integration category. It shows how to use BarcodeGenerator (EncodeTypes.OneCode) to produce PNG images, and how to embed those images into an Excel file via the Workbook and Pictures API. Developers often need to automate barcode creation and reporting in spreadsheets for inventory, tracking, or labeling scenarios.
// Prompt: Generate OneCode barcodes from a collection of strings and embed the images into an Excel worksheet.
// Tags: onecode, barcode, generation, excel, aspose.barcode, aspose.cells, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates OneCode barcodes from a list of strings and embeds them into an Excel worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates barcodes, inserts them into an Excel file, and saves the workbook.
    /// </summary>
    static void Main()
    {
        // Sample OneCode numeric strings (20, 25, 29, 31 digits)
        var oneCodeValues = new List<string>
        {
            "12345678901234567890",                     // 20 digits
            "1234567890123456789012345",                // 25 digits
            "12345678901234567890123456789",            // 29 digits
            "1234567890123456789012345678901"           // 31 digits
        };

        // Create a new Excel workbook
        using (var workbook = new Workbook())
        {
            var worksheet = workbook.Worksheets[0];
            worksheet.Name = "OneCode Barcodes";

            // Header row
            worksheet.Cells[0, 0].PutValue("Code Text");
            worksheet.Cells[0, 1].PutValue("Barcode Image");

            int rowIndex = 1; // start after header

            // Iterate over each code string and generate its barcode
            foreach (var code in oneCodeValues)
            {
                // Generate OneCode barcode for the current string
                using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, code))
                {
                    // Enable automatic sizing using interpolation mode
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Save the generated barcode to a memory stream in PNG format
                    using (var pngStream = new MemoryStream())
                    {
                        generator.Save(pngStream, BarCodeImageFormat.Png);
                        pngStream.Position = 0; // Reset stream position for reading

                        // Write the raw code text into the first column
                        worksheet.Cells[rowIndex, 0].PutValue(code);

                        // Insert the barcode image into the second column; the picture is anchored to the cell
                        worksheet.Pictures.Add(rowIndex, 1, pngStream);
                    }
                }

                rowIndex++; // Move to the next row for the next barcode
            }

            // Save the Excel file containing all barcodes
            workbook.Save("OneCodeBarcodes.xlsx");
        }

        // Indicate completion
        Console.WriteLine("Excel file with OneCode barcodes has been created.");
    }
}