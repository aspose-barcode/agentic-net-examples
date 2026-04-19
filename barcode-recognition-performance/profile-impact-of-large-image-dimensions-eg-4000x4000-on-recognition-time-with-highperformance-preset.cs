using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the large image (e.g., 4000x4000). Adjust as needed.
        string imagePath = "large_image.png";

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader with the image file.
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Use the HighPerformance quality preset.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Optional: set a timeout to avoid hanging on very large images.
            reader.Timeout = 10000; // milliseconds

            // Measure recognition time.
            Stopwatch sw = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");

            // Output recognized barcodes, if any.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}