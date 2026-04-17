using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a temporary folder for XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempFolder);

        // Define a small set of barcode configurations
        var barcodeInfos = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE_TEXT"),
            (EncodeTypes.EAN13, "123456789012")
        };

        // Export to XML benchmark
        var exportStopwatch = Stopwatch.StartNew();
        foreach (var info in barcodeInfos)
        {
            string xmlPath = Path.Combine(tempFolder, $"{info.type}_{Guid.NewGuid()}.xml");
            using (var generator = new BarcodeGenerator(info.type, info.text))
            {
                bool success = generator.ExportToXml(xmlPath);
                if (!success)
                {
                    Console.WriteLine($"Export failed for {info.type}");
                }
            }
        }
        exportStopwatch.Stop();
        Console.WriteLine($"ExportToXml total time for {barcodeInfos.Length} barcodes: {exportStopwatch.ElapsedMilliseconds} ms");

        // Import from XML benchmark
        var importStopwatch = Stopwatch.StartNew();
        foreach (var file in Directory.GetFiles(tempFolder, "*.xml"))
        {
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(file))
            {
                // Simple validation to ensure import succeeded
                if (importedGenerator == null)
                {
                    Console.WriteLine($"Import returned null for file {file}");
                }
            }
        }
        importStopwatch.Stop();
        Console.WriteLine($"ImportFromXml total time for {barcodeInfos.Length} barcodes: {importStopwatch.ElapsedMilliseconds} ms");

        // Clean up temporary files
        foreach (var file in Directory.GetFiles(tempFolder))
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder); } catch { }
    }
}