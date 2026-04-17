using System;
using System.IO;
using System.Diagnostics;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the BMP image with embedded metadata
        string imagePath = "sample.bmp";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image
        using (Bitmap bmp = new Bitmap(imagePath))
        {
            // Create the barcode reader
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Use high‑performance mode to focus on speed
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Set the image for recognition
                reader.SetBarCodeImage(bmp);

                // Measure recognition time
                Stopwatch sw = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();

                // Output timing information
                Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");

                // Output recognized barcodes, if any
                if (results.Length > 0)
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
                else
                {
                    Console.WriteLine("No barcodes detected.");
                }
            }
        }
    }
}