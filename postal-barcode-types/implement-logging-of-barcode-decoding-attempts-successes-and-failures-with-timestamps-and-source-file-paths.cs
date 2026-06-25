using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image and attempting to decode it using Aspose.BarCode.
/// Includes logging for success, failure, and processing steps.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Prepares a temporary directory, creates a sample barcode, and attempts to decode it
    /// along with a non‑existent file to illustrate error handling.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for sample images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Generate a sample barcode image and save it to the temporary directory
        string sampleImagePath = Path.Combine(tempDir, "sample_code128.png");
        GenerateSampleBarcode(sampleImagePath);

        // Define the list of files to attempt decoding (includes a missing file for failure demo)
        string[] filesToDecode = new string[]
        {
            sampleImagePath,
            Path.Combine(tempDir, "nonexistent.png")
        };

        // Iterate over each file path and try to read barcodes
        foreach (string filePath in filesToDecode)
        {
            LogAttempt(filePath);

            // Verify that the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                LogFailure(filePath, "File does not exist.");
                continue;
            }

            // Use BarCodeReader to decode all supported barcode types from the file
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation to detect incorrect barcodes
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                // Allow reading of barcodes that may be slightly incorrect
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                BarCodeResult[] results = null;
                try
                {
                    // Attempt to read barcodes from the image
                    results = reader.ReadBarCodes();
                }
                catch (Exception ex)
                {
                    // Log any exception that occurs during reading
                    LogFailure(filePath, $"Exception during reading: {ex.Message}");
                    continue;
                }

                // Process the results if any barcodes were detected
                if (results != null && results.Length > 0)
                {
                    foreach (var result in results)
                    {
                        LogSuccess(filePath, result);
                    }
                }
                else
                {
                    // No barcodes were found in the image
                    LogFailure(filePath, "No barcode detected.");
                }
            }
        }
    }

    /// <summary>
    /// Generates a simple Code128 barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    private static void GenerateSampleBarcode(string outputPath)
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a reasonable image size (width and height in points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image to the provided path
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Logs an attempt to decode a barcode from the specified file.
    /// </summary>
    /// <param name="filePath">The path of the file being processed.</param>
    private static void LogAttempt(string filePath)
    {
        Console.WriteLine($"{DateTime.Now:O} - Attempting to decode barcode from \"{filePath}\"");
    }

    /// <summary>
    /// Logs a successful barcode decode with detailed information.
    /// </summary>
    /// <param name="filePath">The path of the file that was decoded.</param>
    /// <param name="result">The result object containing barcode details.</param>
    private static void LogSuccess(string filePath, BarCodeResult result)
    {
        Console.WriteLine($"{DateTime.Now:O} - SUCCESS: File=\"{filePath}\", Type={result.CodeTypeName}, CodeText=\"{result.CodeText}\", Confidence={result.Confidence}, ReadingQuality={result.ReadingQuality}");
    }

    /// <summary>
    /// Logs a failure to decode a barcode, including the reason for failure.
    /// </summary>
    /// <param name="filePath">The path of the file that failed to decode.</param>
    /// <param name="reason">A description of why the decoding failed.</param>
    private static void LogFailure(string filePath, string reason)
    {
        Console.WriteLine($"{DateTime.Now:O} - FAILURE: File=\"{filePath}\", Reason={reason}");
    }
}