using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const int batchSize = 200; // number of barcodes to process
        string baseDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(baseDir);

        // -----------------------------------------------------------------
        // Step 1: Generate barcode images (images are not used for export,
        // but they simulate a realistic batch processing scenario)
        // -----------------------------------------------------------------
        for (int i = 0; i < batchSize; i++)
        {
            string codeText = $"CODE{i:D5}";
            string imagePath = Path.Combine(baseDir, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode image
                generator.Save(imagePath);
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Export properties to XML using the file‑path overload
        // -----------------------------------------------------------------
        Stopwatch swFile = new Stopwatch();
        swFile.Start();

        for (int i = 0; i < batchSize; i++)
        {
            string codeText = $"CODE{i:D5}";
            string xmlPath = Path.Combine(baseDir, $"export_file_{i}.xml");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Export to XML file
                bool success = generator.ExportToXml(xmlPath);
                if (!success)
                {
                    Console.WriteLine($"Export to file failed for index {i}");
                }
            }
        }

        swFile.Stop();
        Console.WriteLine($"ExportToXml(string) total time for {batchSize} items: {swFile.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // Step 3: Export properties to XML using the stream overload
        // -----------------------------------------------------------------
        Stopwatch swStream = new Stopwatch();
        swStream.Start();

        for (int i = 0; i < batchSize; i++)
        {
            string codeText = $"CODE{i:D5}";

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                using (var ms = new MemoryStream())
                {
                    // Export to XML stream
                    bool success = generator.ExportToXml(ms);
                    if (!success)
                    {
                        Console.WriteLine($"Export to stream failed for index {i}");
                    }
                    // Reset stream position if further processing is needed (not required here)
                }
            }
        }

        swStream.Stop();
        Console.WriteLine($"ExportToXml(Stream) total time for {batchSize} items: {swStream.ElapsedMilliseconds} ms");
    }
}