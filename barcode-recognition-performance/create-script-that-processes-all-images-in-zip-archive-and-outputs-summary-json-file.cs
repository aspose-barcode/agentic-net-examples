using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string zipPath = "input.zip";
        string outputPath = "summary.json";

        // Verify zip file exists
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Prepare collection for results
        var summary = new List<ImageBarcodeInfo>();

        // Open zip archive
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Process only image files based on extension
                string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                var imageInfo = new ImageBarcodeInfo
                {
                    FileName = entry.FullName,
                    Barcodes = new List<BarcodeInfo>()
                };

                // Open entry stream
                using (Stream entryStream = entry.Open())
                using (Bitmap bitmap = new Bitmap(entryStream))
                using (BarCodeReader reader = new BarCodeReader(bitmap))
                {
                    // Read all barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        var barcode = new BarcodeInfo
                        {
                            Type = result.CodeTypeName,
                            CodeText = result.CodeText
                        };
                        imageInfo.Barcodes.Add(barcode);
                    }
                }

                summary.Add(imageInfo);
            }
        }

        // Serialize summary to JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(summary, jsonOptions);
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Summary written to {outputPath}");
    }
}

// Helper classes for JSON structure
class ImageBarcodeInfo
{
    public string FileName { get; set; }
    public List<BarcodeInfo> Barcodes { get; set; }
}

class BarcodeInfo
{
    public string Type { get; set; }
    public string CodeText { get; set; }
}