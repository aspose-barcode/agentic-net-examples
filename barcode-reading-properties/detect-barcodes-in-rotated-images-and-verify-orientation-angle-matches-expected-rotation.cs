using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "rotated.png";
        const string expectedText = "123456";
        const float expectedAngle = 90f;

        // Create a barcode, rotate it, and save the image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, expectedText))
        {
            generator.Parameters.RotationAngle = expectedAngle;
            generator.Save(imagePath);
        }

        // Read the barcode from the rotated image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {result.Region.Angle}");

                // Verify that the detected angle matches the expected rotation
                // Allow a small tolerance due to possible rounding differences
                const double tolerance = 0.5;
                if (Math.Abs(result.Region.Angle - expectedAngle) <= tolerance)
                {
                    Console.WriteLine("Angle verification succeeded.");
                }
                else
                {
                    Console.WriteLine($"Angle verification failed. Expected {expectedAngle}, but got {result.Region.Angle}");
                }
            }
        }
    }
}