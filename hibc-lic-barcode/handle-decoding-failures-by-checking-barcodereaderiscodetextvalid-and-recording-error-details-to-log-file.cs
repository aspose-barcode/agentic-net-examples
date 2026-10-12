// Title: Decode barcode with validation and log errors
// Description: Demonstrates generating a Code128 barcode, decoding it, checking validity via the decoded text, and logging successes and failures, including handling non‑barcode files.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding and validation category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with a specific DecodeType to read them, and how to verify decoded text. Developers often need to log decoding outcomes, handle missing or corrupt images, and record error details for troubleshooting. The snippet illustrates typical use cases such as batch processing and error reporting in automated workflows.
// Prompt: Handle decoding failures by checking BarCodeReader.IsCodeTextValid and recording error details to a log file.
// Tags: barcode, decoding, validation, logging, code128, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, decoding, validation, and error logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, attempts to decode it, logs results,
    /// and then forces a decoding failure to illustrate error handling.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder for all generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "DecodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define paths for the barcode image and the log file.
        string barcodePath = Path.Combine(workFolder, "sample.png");
        string logPath = Path.Combine(workFolder, "decode_log.txt");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Attempt to read the generated barcode and log the outcome.
        // ------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Determine validity based on presence of decoded text.
                    bool isValid = !string.IsNullOrEmpty(result.CodeText);
                    string logEntry = $"File:{barcodePath} CodeType:{result.CodeTypeName} CodeText:{result.CodeText} Valid:{isValid}{Environment.NewLine}";
                    File.AppendAllText(logPath, logEntry);
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exception that occurs while reading the barcode.
            string errorLog = $"Error reading barcode from {barcodePath}: {ex.Message}{Environment.NewLine}";
            File.AppendAllText(logPath, errorLog);
        }

        // ------------------------------------------------------------
        // Create a dummy non-barcode file to force a decoding failure.
        // ------------------------------------------------------------
        string dummyPath = Path.Combine(workFolder, "dummy.txt");
        File.WriteAllText(dummyPath, "This is not an image.");

        // ------------------------------------------------------------
        // Attempt to read the dummy file as a barcode and log the result.
        // ------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(dummyPath, DecodeType.Code128))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    // No barcodes were found; record this condition.
                    string noResultLog = $"No barcodes found in {dummyPath}{Environment.NewLine}";
                    File.AppendAllText(logPath, noResultLog);
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        bool isValid = !string.IsNullOrEmpty(result.CodeText);
                        string logEntry = $"File:{dummyPath} CodeType:{result.CodeTypeName} CodeText:{result.CodeText} Valid:{isValid}{Environment.NewLine}";
                        File.AppendAllText(logPath, logEntry);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exception that occurs while reading the dummy file.
            string errorLog = $"Error reading barcode from {dummyPath}: {ex.Message}{Environment.NewLine}";
            File.AppendAllText(logPath, errorLog);
        }

        // Inform the user where the log file is located.
        Console.WriteLine($"Decoding log written to: {logPath}");
    }
}