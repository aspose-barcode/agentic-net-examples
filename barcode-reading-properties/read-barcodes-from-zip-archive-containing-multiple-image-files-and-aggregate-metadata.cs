using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Simple container for aggregated barcode metadata
    private class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public BarCodeConfidence Confidence { get; set; }
        public string Bounds { get; set; }
    }

    static void Main(string[] args)
    {
        // Path to the zip archive – use argument if supplied, otherwise a default name
        string zipPath = args.Length > 0 ? args[0] : "sample_barcodes.zip";

        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        var aggregatedResults = new List<BarcodeInfo>();

        // Open the zip archive for reading
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Process only common image files
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                // Open the entry stream and feed it to BarCodeReader
                using (Stream entryStream = entry.Open())
                using (BarCodeReader reader = new BarCodeReader(entryStream, DecodeType.AllSupportedTypes))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        var info = new BarcodeInfo
                        {
                            FileName = entry.Name,
                            CodeType = result.CodeTypeName,
                            CodeText = result.CodeText,
                            Confidence = result.Confidence,
                            Bounds = result.Region.Rectangle.ToString()
                        };
                        aggregatedResults.Add(info);
                    }
                }
            }
        }

        // Output aggregated metadata
        if (aggregatedResults.Count == 0)
        {
            Console.WriteLine("No barcodes were detected in the provided zip archive.");
        }
        else
        {
            Console.WriteLine("Detected barcodes:");
            foreach (var info in aggregatedResults)
            {
                Console.WriteLine($"File: {info.FileName}");
                Console.WriteLine($"  Type      : {info.CodeType}");
                Console.WriteLine($"  Text      : {info.CodeText}");
                Console.WriteLine($"  Confidence: {info.Confidence}");
                Console.WriteLine($"  Bounds    : {info.Bounds}");
                Console.WriteLine();
            }
        }
    }
}