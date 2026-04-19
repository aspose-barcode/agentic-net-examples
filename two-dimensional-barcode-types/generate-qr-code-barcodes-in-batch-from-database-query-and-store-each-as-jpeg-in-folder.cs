using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchExample
{
    class Program
    {
        static void Main()
        {
            // Define output folder
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Simulated database query result - replace with actual DB call
            List<string> records = GetSampleData();

            // Process each record and generate QR code
            foreach (var text in records)
            {
                // Build safe file name
                string safeFileName = GetSafeFileName(text);
                string filePath = Path.Combine(outputFolder, safeFileName + ".jpeg");

                // Create barcode generator for QR code with the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
                {
                    // Set QR error correction level (optional)
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Optionally set colors
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Save as JPEG
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                }
            }

            Console.WriteLine($"Generated {records.Count} QR codes in folder: {outputFolder}");
        }

        // Returns a list of sample strings; replace with real DB query logic
        static List<string> GetSampleData()
        {
            return new List<string>
            {
                "https://example.com/item/1",
                "https://example.com/item/2",
                "https://example.com/item/3",
                "https://example.com/item/4",
                "https://example.com/item/5"
            };
        }

        // Generates a file‑system safe name from the text
        static string GetSafeFileName(string text)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                text = text.Replace(c, '_');
            }
            // Limit length to avoid overly long paths
            return text.Length > 50 ? text.Substring(0, 50) : text;
        }
    }
}