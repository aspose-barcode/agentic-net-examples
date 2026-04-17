using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Directory to store generated images and XML logs
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process a small set of sample barcodes
        for (int i = 1; i <= 3; i++)
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Export generator configuration to XML (audit of generation settings)
                string genXmlPath = Path.Combine(outputDir, $"generator_{i}.xml");
                bool genExported = generator.ExportToXml(genXmlPath);
                Console.WriteLine($"Generator export {(genExported ? "succeeded" : "failed")} for barcode {i}");

                // Generate the barcode image in memory
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to a file for reference (optional)
                    string imagePath = Path.Combine(outputDir, $"barcode_{i}.png");
                    bitmap.Save(imagePath, Aspose.Drawing.Imaging.ImageFormat.Png);

                    // Initialize a reader with the generated bitmap and specify the decode type
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Read barcodes from the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Read barcode {i}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }

                        // Export reader state to XML (audit of recognition settings and results)
                        string readerXmlPath = Path.Combine(outputDir, $"reader_{i}.xml");
                        bool readerExported = reader.ExportToXml(readerXmlPath);
                        Console.WriteLine($"Reader export {(readerExported ? "succeeded" : "failed")} for barcode {i}");
                    }
                }
            }
        }

        Console.WriteLine("Processing completed. XML logs are available in the 'output' folder.");
    }
}