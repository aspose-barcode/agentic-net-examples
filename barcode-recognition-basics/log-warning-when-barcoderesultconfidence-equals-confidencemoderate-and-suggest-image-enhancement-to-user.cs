// Title: QR Code Generation, Detection, and Confidence Warning Example
// Description: Demonstrates generating a QR barcode, reading it back, and logging a warning when detection confidence is moderate.
// Category-Description: Shows basic Aspose.BarCode operations: barcode generation with BarcodeGenerator, image saving, barcode recognition with BarCodeReader, and evaluating ReadingQuality. Useful for developers needing quick validation of barcode readability and guidance on image quality improvement.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: qr, barcode generation, barcode recognition, readingquality, confidence warning, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates a QR barcode, reads it, and reports confidence levels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it, and logs a warning if the reading quality is moderate.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode and save it to the temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode from the image
        BaseDecodeType decodeType = DecodeType.QR; // DecodeType.QR returns a BaseDecodeType
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Ensure at least one barcode was detected
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Process each detected barcode
            foreach (BarCodeResult result in results)
            {
                double quality = result.ReadingQuality; // 0-100

                // Treat quality between 50 and 80 as "moderate"
                if (quality >= 50 && quality < 80)
                {
                    Console.WriteLine("Warning: Barcode detection confidence is Moderate. Consider enhancing the image (e.g., improve lighting, focus, or contrast).");
                }
                else
                {
                    Console.WriteLine($"Barcode detected. Confidence: {quality}");
                }

                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program flow
        }
    }
}