using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Number of samples per rotation angle
        const int samplesPerAngle = 5;
        // Rotation angles to test
        float[] angles = { 0f, 45f, 90f };
        // Store success counts
        var successCounts = new Dictionary<float, int>();
        var totalCounts = new Dictionary<float, int>();

        foreach (float angle in angles)
        {
            successCounts[angle] = 0;
            totalCounts[angle] = samplesPerAngle;

            for (int i = 0; i < samplesPerAngle; i++)
            {
                // Generate random alphanumeric text
                string codeText = GenerateRandomText(10);

                // Create temporary file name
                string tempFile = Path.Combine(Path.GetTempPath(), $"dm_{angle}_{i}.png");

                // Generate DataMatrix barcode with specified rotation
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    generator.Parameters.RotationAngle = angle;
                    generator.Save(tempFile);
                }

                // Recognize the barcode
                using (var reader = new BarCodeReader(tempFile, DecodeType.DataMatrix))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    bool recognized = false;

                    foreach (BarCodeResult result in results)
                    {
                        if (result.CodeText == codeText)
                        {
                            recognized = true;
                            break;
                        }
                    }

                    if (recognized)
                        successCounts[angle]++;
                }

                // Clean up temporary file
                try { File.Delete(tempFile); } catch { }
            }
        }

        // Output success rates
        Console.WriteLine("Recognition success rates for rotated DataMatrix codes:");
        foreach (float angle in angles)
        {
            int success = successCounts[angle];
            int total = totalCounts[angle];
            double rate = (double)success / total * 100.0;
            Console.WriteLine($"Angle {angle}°: {success}/{total} ({rate:F1}%)");
        }
    }

    // Helper method to generate a random alphanumeric string of given length
    private static string GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }
}