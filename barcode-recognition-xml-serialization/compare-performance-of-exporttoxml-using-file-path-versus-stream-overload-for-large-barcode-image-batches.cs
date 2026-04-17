using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const int barcodeCount = 5;
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "ExportXmlDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        long filePathTicks = 0;
        long streamTicks = 0;

        for (int i = 1; i <= barcodeCount; i++)
        {
            string codeText = $"Sample{i:D3}";
            string xmlFilePath = Path.Combine(outputFolder, $"Barcode_{i}_File.xml");
            string xmlStreamFilePath = Path.Combine(outputFolder, $"Barcode_{i}_Stream.xml");

            // Create barcode generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Export using file path overload
                Stopwatch swFile = new Stopwatch();
                swFile.Start();
                bool fileResult = generator.ExportToXml(xmlFilePath);
                swFile.Stop();
                filePathTicks += swFile.ElapsedTicks;

                // Export using stream overload
                using (MemoryStream ms = new MemoryStream())
                {
                    Stopwatch swStream = new Stopwatch();
                    swStream.Start();
                    bool streamResult = generator.ExportToXml(ms);
                    swStream.Stop();
                    streamTicks += swStream.ElapsedTicks;

                    // Save the stream content to a file for verification
                    using (FileStream fileStream = new FileStream(xmlStreamFilePath, FileMode.Create, FileAccess.Write))
                    {
                        ms.Position = 0;
                        ms.CopyTo(fileStream);
                    }
                }
            }
        }

        double filePathMs = (double)filePathTicks / Stopwatch.Frequency * 1000;
        double streamMs = (double)streamTicks / Stopwatch.Frequency * 1000;

        Console.WriteLine($"ExportToXml using file path total time: {filePathMs:F2} ms");
        Console.WriteLine($"ExportToXml using stream total time: {streamMs:F2} ms");
    }
}