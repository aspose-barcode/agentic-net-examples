using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const float minResolution = 200f; // Minimum DPI required for 100% reading quality
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Create two barcode images: low resolution and high resolution
        CreateBarcode(Path.Combine(outputDir, "low_res.png"), 100f);
        CreateBarcode(Path.Combine(outputDir, "high_res.png"), 300f);

        // Validate each image
        ValidateBarcode(Path.Combine(outputDir, "low_res.png"), minResolution);
        ValidateBarcode(Path.Combine(outputDir, "high_res.png"), minResolution);
    }

    static void CreateBarcode(string filePath, float resolutionDpi)
    {
        // Generate a simple Code128 barcode with the specified resolution
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = resolutionDpi; // set DPI
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(filePath);
        }
    }

    static void ValidateBarcode(string filePath, float minResolution)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Retrieve the resolution used during generation from the file name (convention)
        // In a real scenario, store the resolution alongside the image; here we parse it.
        float usedResolution = filePath.Contains("high_res") ? 300f : 100f;

        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Use default quality settings
            reader.QualitySettings = QualitySettings.NormalQuality;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality; // 0‑100 scale
                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Used Resolution: {usedResolution} DPI");
                Console.WriteLine($"  ReadingQuality: {readingQuality}");

                if (readingQuality == 100 && usedResolution >= minResolution)
                {
                    Console.WriteLine("  ✅ ReadingQuality is 100% and meets the minimum resolution requirement.");
                }
                else if (readingQuality == 100 && usedResolution < minResolution)
                {
                    Console.WriteLine("  ⚠️ ReadingQuality is 100% but resolution is below the required threshold.");
                }
                else if (readingQuality < 100 && usedResolution >= minResolution)
                {
                    Console.WriteLine("  ⚠️ Resolution meets the threshold but ReadingQuality is below 100%.");
                }
                else
                {
                    Console.WriteLine("  ℹ️ ReadingQuality below 100% as expected for low‑resolution image.");
                }
            }
        }
    }
}