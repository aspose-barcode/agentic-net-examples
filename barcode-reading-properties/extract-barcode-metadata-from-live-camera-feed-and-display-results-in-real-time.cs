using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to a sample image that would represent a captured camera frame.
        const string imagePath = "sample.png";

        // Verify that the image file exists; otherwise exit gracefully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file '{imagePath}' not found.");
            return;
        }

        // Load the image into a Bitmap (IDisposable) and process it with BarCodeReader.
        using (var bitmap = new Bitmap(imagePath))
        {
            // BarCodeReader without explicit decode types attempts to recognize all supported types.
            using (var reader = new BarCodeReader(bitmap))
            {
                // Use a normal quality preset for balanced performance.
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Perform recognition.
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected in the image.");
                }
                else
                {
                    // Output metadata for each detected barcode.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Barcode Type      : {result.CodeTypeName}");
                        Console.WriteLine($"Code Text         : {result.CodeText}");
                        Console.WriteLine($"Confidence        : {result.Confidence}");
                        Console.WriteLine($"Reading Quality   : {result.ReadingQuality}");
                        Console.WriteLine($"Angle (degrees)   : {result.Region.Angle}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}