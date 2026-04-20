using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "Rm4sccDemo");
        if (!Directory.Exists(workFolder))
        {
            Directory.CreateDirectory(workFolder);
        }

        // Sample RM4SCC code texts
        string[] sampleCodes = new string[] { "AB12C3", "XYZ789", "LMN456" };
        List<string> imageFiles = new List<string>();

        // Generate barcode images
        foreach (string code in sampleCodes)
        {
            string imagePath = Path.Combine(workFolder, $"{code}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, code))
            {
                // Optional: set image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(imagePath);
            }
            imageFiles.Add(imagePath);
        }

        // Create ZIP archive containing the images
        string zipPath = Path.Combine(workFolder, "barcodes.zip");
        using (FileStream zipToCreate = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Update))
            {
                foreach (string filePath in imageFiles)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(filePath));
                    using (Stream entryStream = entry.Open())
                    {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        // Delete original image files to simulate only having the ZIP
        foreach (string filePath in imageFiles)
        {
            File.Delete(filePath);
        }

        // Prepare list for JSON output
        var results = new List<object>();

        // Open ZIP and decode each image
        using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        {
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (Stream entryStream = entry.Open())
                    {
                        using (Bitmap bitmap = new Bitmap(entryStream))
                        {
                            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.RM4SCC))
                            {
                                BarCodeResult[] barcodes = reader.ReadBarCodes();
                                foreach (BarCodeResult barcode in barcodes)
                                {
                                    results.Add(new
                                    {
                                        FileName = entry.Name,
                                        CodeText = barcode.CodeText,
                                        Confidence = barcode.Confidence
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        // Serialize results to JSON and output
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

        // Clean up temporary folder
        try
        {
            File.Delete(zipPath);
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}