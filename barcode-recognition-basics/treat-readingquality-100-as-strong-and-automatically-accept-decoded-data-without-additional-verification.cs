// Title: QR Code Generation and Reading with Quality Evaluation
// Description: Demonstrates generating a QR code image, reading it back, and evaluating the reading quality to automatically accept strong results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include validating barcode quality in automated workflows, where developers need to decide whether to trust decoded data based on the ReadingQuality metric.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr code,generation,recognition,readingquality,aspose.barcode,csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR code, reads it, and evaluates the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads it, and decides whether to accept the data automatically based on quality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR code image containing the text "HelloWorld".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and evaluate its reading quality.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double quality = result.ReadingQuality;

                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {quality}");

                // Treat a reading quality of 100 as strong and accept the data automatically.
                if (quality == 100)
                {
                    Console.WriteLine("Strong quality detected – data accepted automatically.");
                }
                else
                {
                    Console.WriteLine("Reading quality below strong – additional verification may be required.");
                }
            }
        }

        // Clean up temporary files and directory.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}