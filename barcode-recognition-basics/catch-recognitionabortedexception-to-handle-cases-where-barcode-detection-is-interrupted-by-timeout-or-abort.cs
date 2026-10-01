// Title: Handle Barcode Recognition Timeout with RecognitionAbortedException
// Description: Demonstrates generating a QR code, attempting to read it with an intentionally short timeout, and catching RecognitionAbortedException when detection is aborted.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Developers often need to handle time‑sensitive scanning scenarios, where a low timeout or external abort can trigger a RecognitionAbortedException. The sample illustrates proper exception handling for robust barcode processing pipelines.
// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: barcode, qr, recognition, timeout, exception, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates catching <see cref="RecognitionAbortedException"/> when barcode detection is aborted due to timeout.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, attempts to read it with a minimal timeout,
    /// and handles possible <see cref="RecognitionAbortedException"/>.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with a short timeout to demonstrate abort handling
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Set a very low timeout (in milliseconds) to force a timeout scenario
            reader.Timeout = 1; // 1 ms timeout

            try
            {
                // Attempt to read barcodes; this may throw RecognitionAbortedException
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeText} ({result.CodeTypeName})");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout or abort request
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}