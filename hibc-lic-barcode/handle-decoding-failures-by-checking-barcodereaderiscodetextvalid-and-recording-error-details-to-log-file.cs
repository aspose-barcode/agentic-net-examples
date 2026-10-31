// Title: Demonstrate barcode decoding with error logging for invalid reads
// Description: This example generates a valid Code128 barcode, attempts to decode it along with a non‑barcode image, and records success or failure details to a log file.
// Category-Description: Shows how to use Aspose.BarCode's BarCodeGenerator and BarCodeReader for encoding and decoding operations. Typical use cases include batch processing of images, validating scanned data, and capturing decoding errors. Developers often need to log invalid reads using BarCodeReader.IsCodeTextValid or similar checks to troubleshoot scanning issues.
// Prompt: Handle decoding failures by checking BarCodeReader.IsCodeTextValid and recording error details to a log file.
// Tags: barcode symbology, decoding, error handling, logging, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point and helper methods for barcode generation, decoding, and logging.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a sample barcode, creates a dummy image, decodes both, and writes results to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image, dummy image, and log file
        string barcodePath = Path.Combine(workFolder, "sample.png");
        string dummyPath = Path.Combine(workFolder, "dummy.png");
        string logPath = Path.Combine(workFolder, "decode_log.txt");

        // Generate a valid Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a dummy PNG image that does not contain a barcode
        using (var bmp = new Bitmap(100, 100))
        {
            bmp.Save(dummyPath, ImageFormat.Png);
        }

        // Decode the valid barcode image and log the outcome
        DecodeAndLog(barcodePath, logPath);

        // Attempt to decode the dummy image and log the outcome
        DecodeAndLog(dummyPath, logPath);

        // Inform the user where the log file was written
        Console.WriteLine("Decoding log written to: " + logPath);
    }

    /// <summary>
    /// Attempts to read barcodes from the specified image file and appends the results to a log file.
    /// </summary>
    /// <param name="imagePath">Path to the image to be decoded.</param>
    /// <param name="logPath">Path to the log file where results are recorded.</param>
    static void DecodeAndLog(string imagePath, string logPath)
    {
        // Verify that the image file exists before attempting decoding
        if (!File.Exists(imagePath))
        {
            File.AppendAllText(logPath, $"File not found: {imagePath}{Environment.NewLine}");
            return;
        }

        // Use a generic decode type (Code128) for demonstration purposes
        BaseDecodeType decodeType = DecodeType.Code128;

        // Initialize the barcode reader for the given image and decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes are detected, log the information
            if (results.Length == 0)
            {
                File.AppendAllText(logPath, $"No barcode detected in file: {imagePath}{Environment.NewLine}");
                return;
            }

            // Process each detected barcode result
            foreach (BarCodeResult result in results)
            {
                // Determine validity: a non‑empty CodeText generally indicates a successful decode
                // (In real scenarios, BarCodeReader.IsCodeTextValid can be used for stricter validation)
                bool isValid = !string.IsNullOrEmpty(result.CodeText);
                string logEntry = $"File:{imagePath} CodeType:{result.CodeTypeName} CodeText:{result.CodeText ?? "null"} Valid:{isValid}";
                File.AppendAllText(logPath, logEntry + Environment.NewLine);
            }
        }
    }
}