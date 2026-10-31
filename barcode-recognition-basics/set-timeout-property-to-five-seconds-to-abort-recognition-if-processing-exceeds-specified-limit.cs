// Title: QR Code generation and recognition with timeout
// Description: Demonstrates generating a QR barcode, saving it to a PNG file, and recognizing it using Aspose.BarCode with a 5‑second timeout to abort long‑running recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and the Timeout property to limit processing time. Typical use cases include batch scanning where long‑running reads must be bounded. Developers often need to handle RecognitionAbortedException when the timeout is exceeded.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: qr code, barcode generation, barcode recognition, timeout, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR barcode generation and recognition with a timeout using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it with a 5‑second timeout, and cleans up temporary files.
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

        // Initialize a barcode reader for QR codes and set a 5‑second timeout (5000 ms)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.Timeout = 5000; // milliseconds

            try
            {
                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the number of barcodes found
                Console.WriteLine($"Found {reader.FoundCount} barcode(s).");

                // Iterate through each result and display its type and decoded text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted after timeout. Execution time: {ex.ExecutionTime} ms");
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception cleanupEx)
        {
            Console.WriteLine($"Cleanup error: {cleanupEx.Message}");
        }
    }
}