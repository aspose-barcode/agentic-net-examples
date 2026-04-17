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
        const string imagePath = "sample_barcode.png";

        // Generate a barcode image with a known rotation (e.g., 90 degrees)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "AutoRotateDemo"))
        {
            // Rotate the generated image; this simulates a mis‑oriented barcode
            generator.Parameters.RotationAngle = 90f;
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: The barcode image '{imagePath}' was not found.");
            return;
        }

        // Read the barcode (auto‑rotation is enabled by default in recent versions)
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Optionally, use a quality preset (NormalQuality is sufficient here)
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type : {result.CodeTypeName}");
                Console.WriteLine($"Detected Text : {result.CodeText}");
                Console.WriteLine($"Detected Angle: {result.Region.Angle}°");
            }
        }
    }
}