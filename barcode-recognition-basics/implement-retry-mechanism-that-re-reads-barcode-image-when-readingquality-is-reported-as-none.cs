// Title: Barcode Generation and Retry Reading Example
// Description: Demonstrates generating a Code128 barcode, saving it to a temporary file, and retrying the read operation when the reading quality is insufficient.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical scenarios include automated label creation and verification in inventory or shipping systems, where developers often need to handle transient read failures and implement retry logic.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: code128, barcode generation, barcode recognition, retry, readingquality, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode, saves it to a temporary location, and attempts to read it with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates a barcode image, then reads it up to three times,
    /// stopping early if a successful read occurs.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // 1. Set up a temporary folder to store the generated barcode image.
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ----------------------------------------------------------------------
        // 2. Generate a simple Code128 barcode and save it as PNG.
        // ----------------------------------------------------------------------
        const string codeText = "1234567890";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Default settings are sufficient for this demonstration.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------------------------------
        // 3. Verify that the image file was created successfully.
        // ----------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // ----------------------------------------------------------------------
        // 4. Prepare the decode type for reading the barcode.
        // ----------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;

        // ----------------------------------------------------------------------
        // 5. Retry logic: attempt to read the barcode up to maxAttempts times.
        // ----------------------------------------------------------------------
        const int maxAttempts = 3;
        bool success = false;

        for (int attempt = 1; attempt <= maxAttempts && !success; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} to read the barcode...");

            // Ensure the image still exists before each read attempt.
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Barcode image missing.");
                break;
            }

            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                // Iterate through all detected barcodes (normally just one).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // A successful read marks the operation as complete.
                    Console.WriteLine($"Read succeeded. CodeText: {result.CodeText}");
                    success = true;
                    break;
                }
            }
        }

        // ----------------------------------------------------------------------
        // 6. Report final outcome if all attempts failed.
        // ----------------------------------------------------------------------
        if (!success)
        {
            Console.WriteLine($"Failed to read the barcode after {maxAttempts} attempts.");
        }

        // ----------------------------------------------------------------------
        // 7. Clean up temporary files and directories.
        // ----------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            // Log cleanup issues without terminating the program.
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}