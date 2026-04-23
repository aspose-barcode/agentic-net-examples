using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchGenerator
{
    class Program
    {
        static void Main()
        {
            // Output directory for generated PNG files
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Sample list of URLs to encode as barcodes
            List<string> urls = new List<string>
            {
                "https://example.com/page1",
                "https://example.com/page2",
                "https://example.com/page3",
                "https://example.com/page4",
                "https://example.com/page5"
            };

            int index = 1;
            foreach (string url in urls)
            {
                // Create a QR code generator with the URL as CodeText
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, url))
                {
                    // Set QR error correction level (optional)
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Build the output file name
                    string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");

                    // Save the barcode image as PNG
                    generator.Save(filePath);
                }

                index++;
            }
        }
    }
}