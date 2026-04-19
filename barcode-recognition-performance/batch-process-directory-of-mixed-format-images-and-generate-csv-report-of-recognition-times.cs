using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputCsv = Path.Combine(Directory.GetCurrentDirectory(), "RecognitionReport.csv");

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Seed sample barcode images
            GenerateSampleBarcodes(inputFolder);
        }

        // Prepare CSV writer
        using (StreamWriter csvWriter = new StreamWriter(outputCsv, false))
        {
            csvWriter.WriteLine("FileName,RecognitionTimeMs,BarCodeCount");

            // Get image files (common formats)
            string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in imageFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" &&
                    extension != ".bmp" && extension != ".tif" && extension != ".tiff" && extension != ".gif")
                {
                    continue; // skip non‑image files
                }

                long elapsedMs = 0;
                int barcodeCount = 0;

                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    using (BarCodeReader reader = new BarCodeReader(filePath))
                    {
                        // Use high‑performance preset to speed up processing
                        reader.QualitySettings = QualitySettings.HighPerformance;
                        // Optional timeout (e.g., 5 seconds)
                        reader.Timeout = 5000;

                        // Perform recognition
                        var results = reader.ReadBarCodes();
                        barcodeCount = results?.Length ?? 0;
                    }
                    sw.Stop();
                    elapsedMs = sw.ElapsedMilliseconds;
                }
                catch (RecognitionAbortedException ex)
                {
                    // Timeout or abort occurred
                    elapsedMs = ex.ExecutionTime;
                    Console.WriteLine($"Recognition aborted for {Path.GetFileName(filePath)}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // General error handling
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }

                // Write CSV line
                csvWriter.WriteLine($"{Path.GetFileName(filePath)},{elapsedMs},{barcodeCount}");
            }
        }

        Console.WriteLine($"Recognition report generated at: {outputCsv}");
    }

    // Generates a few sample barcode images for demonstration purposes
    private static void GenerateSampleBarcodes(string folder)
    {
        // Code128 sample
        using (BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            string path1 = Path.Combine(folder, "Sample_Code128.png");
            gen1.Save(path1);
        }

        // QR Code sample
        using (BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            string path2 = Path.Combine(folder, "Sample_QR.png");
            gen2.Save(path2);
        }

        // EAN13 sample
        using (BarcodeGenerator gen3 = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            string path3 = Path.Combine(folder, "Sample_EAN13.png");
            gen3.Save(path3);
        }
    }
}