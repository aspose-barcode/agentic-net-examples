using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating OneCode barcodes and embedding them into an Excel workbook.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for a set of numeric strings,
    /// inserts them into an Excel worksheet, and saves the workbook to disk.
    /// </summary>
    static void Main()
    {
        // Define sample OneCode numeric strings of varying lengths (20, 25, 29, 31 digits)
        var oneCodeValues = new List<string>
        {
            "12345678901234567890",               // 20 digits
            "1234567890123456789012345",          // 25 digits
            "12345678901234567890123456789",      // 29 digits
            "1234567890123456789012345678901"     // 31 digits
        };

        // Create a new Excel workbook within a using block to ensure proper disposal
        using (var workbook = new Workbook())
        {
            // Access the first worksheet (index 0)
            var worksheet = workbook.Worksheets[0];

            int rowIndex = 0; // Tracks the current row for inserting data

            // Iterate over each OneCode value to generate and embed its barcode
            foreach (var code in oneCodeValues)
            {
                // Generate a OneCode barcode image and store it in a memory stream
                using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, code))
                {
                    // Set image resolution (dots per inch) if higher quality is required
                    generator.Parameters.Resolution = 300f;

                    using (var ms = new MemoryStream())
                    {
                        // Save the barcode as a PNG image into the memory stream
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position before reading

                        // Insert the barcode image into the worksheet at the current row, column 0
                        worksheet.Pictures.Add(rowIndex, 0, ms);
                    }
                }

                // Add a text label in column 2 (C) with the original code for reference
                worksheet.Cells[rowIndex, 2].PutValue(code);

                rowIndex++; // Move to the next row for the subsequent barcode
            }

            // Persist the workbook to a file named "OneCodeBarcodes.xlsx"
            workbook.Save("OneCodeBarcodes.xlsx");
        }

        // Inform the user that the Excel file has been created successfully
        Console.WriteLine("Excel file with OneCode barcodes has been created.");
    }
}