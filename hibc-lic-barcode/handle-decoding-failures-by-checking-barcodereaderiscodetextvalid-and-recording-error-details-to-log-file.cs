// Title: Decode barcode images and log results, handling invalid reads
// Description: Demonstrates generating a barcode, creating a non‑barcode image, then decoding each file while checking for valid code text and recording successes or failures to a log file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeGenerator for encoding and BarCodeReader for decoding. It shows typical use cases such as batch processing images, validating decoded text via BarCodeReader.IsCodeTextValid (or checking CodeText), and logging outcomes—common tasks for developers integrating barcode scanning into .NET applications.
// Prompt: Handle decoding failures by checking BarCodeReader.IsCodeTextValid and recording error details to a log file.
// Tags: barcode, code128, generation, recognition, validation, logging, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, dummy image creation, decoding, and logging of results,
/// including handling of invalid or missing barcode data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, creates a dummy image, attempts to decode both,
    /// and writes detailed outcomes to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for all demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the generated barcode, dummy image, and log file
        string barcodePath = Path.Combine(tempDir, "sample.png");
        string dummyPath = Path.Combine(tempDir, "dummy.png");
        string logPath = Path.Combine(tempDir, "decode_log.txt");

        // ------------------------------------------------------------
        // Generate a valid Code128 barcode image
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Create a dummy PNG image that contains no barcode
        // ------------------------------------------------------------
        using (Bitmap bmp = new Bitmap(200, 200))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Aspose.Drawing.Color.White);
            }
            bmp.Save(dummyPath, ImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Process each file: attempt to read barcodes and log results
        // ------------------------------------------------------------
        string[] files = new[] { barcodePath, dummyPath };
        foreach (string file in files)
        {
            // Verify the file exists before processing
            if (!File.Exists(file))
            {
                File.AppendAllText(logPath, $"File not found: {file}{Environment.NewLine}");
                continue;
            }

            try
            {
                // Initialize the reader for Code128 barcodes
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // No barcodes detected in the image
                    if (results.Length == 0)
                    {
                        File.AppendAllText(logPath, $"No barcode detected in file: {file}{Environment.NewLine}");
                    }
                    else
                    {
                        // Iterate through all detected barcodes
                        foreach (BarCodeResult result in results)
                        {
                            // Determine if the decoded text is valid (non‑empty)
                            bool isValid = !string.IsNullOrEmpty(result.CodeText);
                            if (isValid)
                            {
                                File.AppendAllText(logPath,
                                    $"Success: File={file}, Type={result.CodeTypeName}, Text={result.CodeText}{Environment.NewLine}");
                            }
                            else
                            {
                                // Barcode detected but text could not be decoded properly
                                File.AppendAllText(logPath,
                                    $"Invalid decode in file: {file}, Type={result.CodeTypeName}{Environment.NewLine}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors during processing
                File.AppendAllText(logPath,
                    $"Error processing file {file}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Inform the user where the log file is located
        Console.WriteLine($"Processing complete. Log file: {logPath}");
    }
}