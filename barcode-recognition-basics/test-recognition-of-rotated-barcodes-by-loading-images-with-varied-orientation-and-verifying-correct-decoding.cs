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
        // Define barcode text and rotation angles to test
        string barcodeText = "Test123";
        float[] angles = new float[] { 0f, 90f, 180f, 270f, 45f };

        // Directory to store generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        foreach (float angle in angles)
        {
            string fileName = $"barcode_{angle}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate barcode with specified rotation
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                generator.Parameters.RotationAngle = angle;
                generator.Save(filePath);
            }

            // Verify file was created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image for angle {angle}.");
                continue;
            }

            // Read and decode the barcode
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                bool decoded = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Angle: {angle}°, Detected Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
                    if (result.CodeText == barcodeText)
                    {
                        decoded = true;
                    }
                }

                if (decoded)
                {
                    Console.WriteLine($"Success: Barcode at {angle}° decoded correctly.");
                }
                else
                {
                    Console.WriteLine($"Failure: Barcode at {angle}° did not decode as expected.");
                }
            }

            Console.WriteLine(new string('-', 50));
        }

        Console.WriteLine("Processing completed.");
    }
}