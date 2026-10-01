// Title: Barcode recognition with timeout handling
// Description: Demonstrates generating a QR code, reading it using Aspose.BarCode, and aborting the recognition if it exceeds a five‑second limit.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and the BarCodeReader for decoding them, highlighting how to configure the Timeout property to prevent long‑running recognition tasks. Developers working with barcode scanning, image processing, or real‑time applications often need to limit processing time to maintain responsiveness.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: barcode, qr, generation, recognition, timeout, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition with a timeout to abort long‑running scans.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a QR code, reads it with a 5‑second timeout, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTimeoutSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR code and save it to the temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a BarCodeReader with a timeout of 5 seconds (5000 ms)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            reader.Timeout = 5000; // Timeout in milliseconds

            try
            {
                // Attempt to read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    // Output details for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    }
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted after timeout: {ex.ExecutionTime} ms");
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
            // Ignored – cleanup failures should not affect program flow
        }
    }
}