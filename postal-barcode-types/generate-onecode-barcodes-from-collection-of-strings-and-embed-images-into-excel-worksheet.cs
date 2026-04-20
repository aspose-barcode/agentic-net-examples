using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Cells;

namespace OneCodeBarcodeExcelDemo
{
    class Program
    {
        static void Main()
        {
            // Sample collection of valid OneCode numeric strings (20, 25, 29, or 31 digits)
            List<string> oneCodeValues = new List<string>
            {
                "12345678901234567890",                     // 20 digits
                "1234567890123456789012345",                // 25 digits
                "12345678901234567890123456789",            // 29 digits
                "1234567890123456789012345678901"           // 31 digits
            };

            // Create a new Excel workbook
            using (Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook())
            {
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];

                int row = 0;
                foreach (string code in oneCodeValues)
                {
                    // Validate OneCode length (must be 20, 25, 29, or 31 digits)
                    if (code.Length != 20 && code.Length != 25 && code.Length != 29 && code.Length != 31)
                    {
                        Console.WriteLine($"Skipping invalid OneCode value: {code}");
                        continue;
                    }

                    // Generate OneCode barcode
                    using (Aspose.BarCode.Generation.BarcodeGenerator generator =
                        new Aspose.BarCode.Generation.BarcodeGenerator(EncodeTypes.OneCode, code))
                    {
                        // Optional: set barcode colors if desired
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                        // Generate the barcode image as a Bitmap
                        using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                        {
                            // Save bitmap to a memory stream in PNG format
                            using (MemoryStream imageStream = new MemoryStream())
                            {
                                barcodeImage.Save(imageStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                                imageStream.Position = 0; // Reset stream position for reading

                                // Insert the image into the worksheet at the current row
                                // Column 0 (A) is used; each image occupies its own row
                                sheet.Pictures.Add(row, 0, imageStream);
                            }
                        }
                    }

                    // Write the code text next to the image for reference
                    sheet.Cells[row, 2].PutValue(code); // Column C

                    row += 15; // Leave some rows between images for visibility
                }

                // Save the Excel file
                string outputPath = "OneCodeBarcodes.xlsx";
                workbook.Save(outputPath);
                Console.WriteLine($"Excel file with OneCode barcodes saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}