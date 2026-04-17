using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeRegionExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input PNG file – use first argument or default sample name
            string imagePath = args.Length > 0 ? args[0] : "sample.png";

            // Verify that the file exists
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                return;
            }

            // Read barcodes from the image
            using (var reader = new BarCodeReader())
            {
                // Detect all supported barcode types
                reader.SetBarCodeReadType(DecodeType.AllSupportedTypes);
                // Load image file for recognition
                reader.SetBarCodeImage(imagePath);

                // Process each detected barcode
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Extract region rectangle and angle
                    var rect = result.Region.Rectangle;
                    double angle = result.Region.Angle;

                    // Output to console for verification
                    Console.WriteLine($"Detected: {result.CodeText}");
                    Console.WriteLine($"  Region - X:{rect.X} Y:{rect.Y} Width:{rect.Width} Height:{rect.Height} Angle:{angle}");
                }
            }

            Console.WriteLine("Barcode region extraction completed.");
        }
    }
}