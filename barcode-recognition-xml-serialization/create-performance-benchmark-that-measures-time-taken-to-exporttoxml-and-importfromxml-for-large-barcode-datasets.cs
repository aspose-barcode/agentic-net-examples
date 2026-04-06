using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        const int barcodeCount = 1000;
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempDir);

        List<string> xmlFiles = new List<string>(barcodeCount);

        // Measure ExportToXml performance
        Stopwatch exportTimer = new Stopwatch();
        exportTimer.Start();
        for (int i = 0; i < barcodeCount; i++)
        {
            string codeText = $"CODE{i:D6}";
            string xmlPath = Path.Combine(tempDir, $"barcode_{i}.xml");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Export properties to XML file
                generator.ExportToXml(xmlPath);
            }
            xmlFiles.Add(xmlPath);
        }
        exportTimer.Stop();
        Console.WriteLine($"Exported {barcodeCount} barcodes to XML in {exportTimer.Elapsed.TotalSeconds:F2} seconds.");

        // Measure ImportFromXml performance
        Stopwatch importTimer = new Stopwatch();
        importTimer.Start();
        foreach (string xmlPath in xmlFiles)
        {
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // No further action needed; just loading the XML.
            }
        }
        importTimer.Stop();
        Console.WriteLine($"Imported {barcodeCount} barcodes from XML in {importTimer.Elapsed.TotalSeconds:F2} seconds.");

        // Cleanup temporary files
        foreach (string file in xmlFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any deletion errors
        }
    }
}