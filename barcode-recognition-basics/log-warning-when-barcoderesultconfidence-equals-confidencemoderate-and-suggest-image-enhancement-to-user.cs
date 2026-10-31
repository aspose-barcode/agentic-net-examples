// Title: Demonstrate QR barcode generation, recognition, and confidence warning
// Description: This example creates a QR code image, reads it back, and logs a warning when the detection confidence is moderate, advising image enhancement.
// Category-Description: Shows how to use Aspose.BarCode for barcode generation and recognition, focusing on confidence evaluation. Key classes include BarcodeGenerator, BarCodeReader, and BarCodeResult. Typical use cases involve validating barcode readability and prompting image quality improvements, a common need for developers integrating barcode scanning in applications.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: qr, barcode generation, barcode recognition, png, aspose.barcode, confidence

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR barcode generation, reading, and confidence evaluation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode, reads it, and warns if confidence is moderate.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set up the reader to decode all supported barcode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Iterate through each detected barcode and output its details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}, Confidence={result.Confidence}");
                // Log a warning if the confidence level is moderate
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Moderate confidence detected. Consider enhancing the image (e.g., increase resolution, improve contrast).");
                }
            }
        }

        // Clean up temporary files and folder; ignore any errors during cleanup
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