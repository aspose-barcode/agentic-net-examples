// Title: Barcode read retry based on ReadingQuality
// Description: Demonstrates generating a QR code and attempting to read it multiple times, retrying when the reading quality is reported as None.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to implement retry logic when barcode quality varies, using classes like BarcodeGenerator, BarCodeReader, BarCodeResult, and related parameters to ensure reliable scanning in real‑world applications.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: qr, barcode, readingquality, retry, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a retry mechanism for reading a barcode image when the reading quality is reported as None.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, then attempts to read it up to a maximum number of attempts,
    /// retrying when the reading quality is None.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeRetry_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        const int maxAttempts = 3;
        bool success = false;

        // Attempt to read the barcode up to the maximum number of attempts
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} to read barcode...");

            using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
            {
                // Read all barcodes found in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    // Process each detected barcode result
                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                        // ReadingQuality of 0 indicates 'None' – retry if possible
                        if (result.ReadingQuality != 0) // Non-None quality
                        {
                            Console.WriteLine("Barcode read with acceptable quality.");
                            success = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ReadingQuality is None; will retry if attempts remain.");
                        }
                    }
                }

                // Exit the retry loop early if a successful read occurred
                if (success)
                    break;
            }
        }

        // Report final outcome after all attempts
        if (!success)
        {
            Console.WriteLine("Failed to read barcode with acceptable quality after maximum attempts.");
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}