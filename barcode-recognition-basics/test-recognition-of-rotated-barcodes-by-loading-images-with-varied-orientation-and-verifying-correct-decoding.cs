using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder to store generated barcode images
        string folder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(folder);

        // Angles to test (including non‑standard angle)
        float[] angles = new float[] { 0f, 90f, 180f, 270f, 45f };

        foreach (float angle in angles)
        {
            string filePath = Path.Combine(folder, $"barcode_{angle}.png");

            // Generate barcode with specific rotation angle
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Parameters.RotationAngle = angle;
                generator.Save(filePath);
            }

            // Read the barcode and output detected information
            using (var reader = new BarCodeReader())
            {
                // Detect any supported barcode type
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                reader.SetBarCodeImage(filePath);

                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | " +
                                      $"Set Angle: {angle}° | " +
                                      $"Detected Text: {result.CodeText} | " +
                                      $"Detected Angle: {result.Region.Angle}°");
                }
            }
        }
    }
}