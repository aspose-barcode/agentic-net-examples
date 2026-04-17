using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        // Ensure a barcode image exists with a known DPI (300)
        if (!File.Exists(imagePath))
        {
            // Create a Code128 barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                // Set image resolution to 300 DPI for accurate region detection
                generator.Parameters.Resolution = 300f; // float literal with 'f'
                // Optionally set image size to keep it reasonable
                generator.Parameters.ImageWidth.Point = 400f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(imagePath);
            }
        }

        // Load the image and explicitly set its DPI (in case metadata is missing)
        using (var bitmap = new Bitmap(imagePath))
        {
            // Adjust DPI to match generation settings
            bitmap.SetResolution(300f, 300f);

            // Prepare the reader for Code128 (you can add more types if needed)
            using (var reader = new BarCodeReader())
            {
                reader.SetBarCodeReadType(DecodeType.Code128);
                reader.SetBarCodeImage(bitmap);

                // Read barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                    Console.WriteLine("Code Text: " + result.CodeText);

                    // Get the region rectangle (bounds) of the detected barcode
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                }
            }
        }
    }
}