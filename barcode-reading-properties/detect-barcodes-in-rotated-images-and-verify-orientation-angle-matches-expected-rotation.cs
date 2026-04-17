using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "rotated_barcode.png";
        const string codeText = "1234567890";
        const float expectedAngle = 90f; // degrees

        // Generate a barcode image rotated by the expected angle
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = expectedAngle;
            generator.Save(imagePath);
        }

        // Verify the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' was not created.");
            return;
        }

        // Read the barcode and obtain its detected angle
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            foreach (var result in results)
            {
                double detectedAngle = result.Region.Angle; // 0‑360 degrees
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {detectedAngle}°");

                // Compare with the expected rotation (allow a small tolerance)
                const double tolerance = 0.5;
                if (Math.Abs(detectedAngle - expectedAngle) <= tolerance ||
                    Math.Abs(detectedAngle - (expectedAngle + 360)) <= tolerance)
                {
                    Console.WriteLine("Orientation verification: SUCCESS");
                }
                else
                {
                    Console.WriteLine("Orientation verification: FAILURE");
                }
            }
        }
    }
}