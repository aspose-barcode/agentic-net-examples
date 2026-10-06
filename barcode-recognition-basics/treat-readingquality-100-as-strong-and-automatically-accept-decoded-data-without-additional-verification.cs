// Title: Demonstrate QR Code Generation and Strong ReadingQuality Evaluation
// Description: This example generates a QR code image, reads it back, and treats a ReadingQuality of 100 as strong, automatically accepting the decoded data.
// Category-Description: Shows how to use Aspose.BarCode for barcode generation and recognition, focusing on QR code handling. It demonstrates the BarcodeGenerator, BarCodeReader, and BarCodeResult classes, common tasks such as creating temporary files, saving images, and evaluating reading quality. Developers looking for quick QR code creation and confidence‑based validation will find this pattern useful.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr code, generation, recognition, readingquality, aspose.barcode, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, reading it, and automatically accepting data when reading quality is maximal.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary QR code, reads it, and processes reading quality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR code image with the text "StrongQualityTest"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "StrongQualityTest"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and evaluate its ReadingQuality
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    double quality = result.ReadingQuality;
                    if (quality == 100)
                    {
                        // Treat as strong quality and accept automatically
                        Console.WriteLine($"Accepted (Strong Quality): {result.CodeText}");
                    }
                    else
                    {
                        Console.WriteLine($"Quality {quality}: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}