// Title: Barcode recognition with timeout abort after five seconds
// Description: Demonstrates generating a QR barcode image and reading it using Aspose.BarCode with a 5‑second timeout to abort recognition if processing exceeds the limit.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include scanning images in applications where long‑running recognition must be bounded, and developers often need to configure the Timeout property to prevent hangs.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: barcode, qr, timeout, recognition, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR barcode, reads it with a timeout, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, attempts to read it with a 5‑second timeout, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode containing the text "Hello Aspose" and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created before attempting recognition
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader for QR codes and set a 5‑second timeout (5000 ms)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.Timeout = 5000; // milliseconds

            try
            {
                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes found: {results.Length}");

                // Output each detected barcode's type and text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms");
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