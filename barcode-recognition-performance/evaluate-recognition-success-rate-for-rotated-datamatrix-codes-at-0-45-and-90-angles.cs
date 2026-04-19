using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Parameters
        string codeText = "TestDataMatrix123";
        float[] angles = { 0f, 45f, 90f };
        int samplesPerAngle = 5;

        foreach (float angle in angles)
        {
            int successCount = 0;

            for (int i = 0; i < samplesPerAngle; i++)
            {
                string fileName = $"DataMatrix_{angle}_{i}.png";

                // Generate rotated DataMatrix barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    generator.Parameters.RotationAngle = angle;
                    generator.Save(fileName);
                }

                // Verify file exists before reading
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"Failed to create image: {fileName}");
                    continue;
                }

                // Recognize the barcode
                using (var reader = new BarCodeReader(fileName, DecodeType.DataMatrix))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        if (result != null && result.CodeText == codeText)
                        {
                            successCount++;
                        }
                    }
                }
            }

            float successRate = (float)successCount / samplesPerAngle * 100f;
            Console.WriteLine($"Angle {angle}° - Success Rate: {successRate}% ({successCount}/{samplesPerAngle})");
        }
    }
}