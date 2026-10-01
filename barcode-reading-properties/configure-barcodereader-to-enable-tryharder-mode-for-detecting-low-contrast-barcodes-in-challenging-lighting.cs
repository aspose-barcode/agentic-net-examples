using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcode_TryHarder_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodePath = Path.Combine(tempFolder, "low_contrast.png");

        // Generate a low‑contrast barcode (dark gray on light gray)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrast"))
        {
            // Set foreground and background colors that are close to each other
            generator.Parameters.Barcode.BarColor = Color.FromArgb(100, 100, 100); // dark gray
            generator.Parameters.BackColor = Color.FromArgb(150, 150, 150); // light gray

            // Save the barcode image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Create a BarCodeReader to detect all supported types
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optionally, use a high‑performance quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Read barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}