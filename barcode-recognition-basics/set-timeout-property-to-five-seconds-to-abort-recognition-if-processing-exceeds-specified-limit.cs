// Title: Barcode recognition with timeout handling
// Description: Demonstrates generating a QR barcode, saving it to a file, and reading it with a 5‑second timeout to abort long‑running recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include scanning images for QR codes or other symbologies where developers need to enforce a processing time limit to prevent hangs. The example highlights setting the Timeout property and handling RecognitionAbortedException, common tasks when building robust barcode scanning solutions.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: barcode, qr, timeout, recognition, aspose.barcode, generation, reading, exception handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a QR barcode, saving it, and reading it with a timeout to abort long‑running recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it with a 5‑second timeout, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTimeoutDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode with the text "Hello Aspose" and save it as a PNG file
        BaseEncodeType encodeType = EncodeTypes.QR;
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "Hello Aspose"))
        {
            using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.Write))
            {
                generator.Save(fs, BarCodeImageFormat.Png);
            }
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for QR codes and set a 5‑second timeout (5000 ms)
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.Timeout = 5000; // Timeout in milliseconds

            try
            {
                // Attempt to read barcodes from the image
                reader.ReadBarCodes();

                // Output the number of barcodes found and their details
                Console.WriteLine($"Barcodes found: {reader.FoundCount}");
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted after timeout. Execution time: {ex.ExecutionTime} ms");
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
            // Cleanup failures are non‑critical for this demo
        }
    }
}