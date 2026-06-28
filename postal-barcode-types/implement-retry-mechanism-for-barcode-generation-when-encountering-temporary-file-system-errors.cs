using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up output paths and initiates barcode generation.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode images will be saved
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputFolder, "code128.png");

        // Text to encode in the barcode
        string codeText = "123ABC";

        // Specify the barcode symbology (Code128)
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Attempt to generate the barcode with up to 3 retries on temporary I/O errors
        bool success = GenerateBarcodeWithRetry(encodeType, codeText, outputPath, maxAttempts: 3);

        // Inform the user of the result
        Console.WriteLine(success
            ? $"Barcode generated successfully at: {outputPath}"
            : "Failed to generate barcode after multiple attempts.");
    }

    /// <summary>
    /// Generates a barcode image and saves it to the specified path.
    /// Retries the operation when a temporary file system error occurs.
    /// </summary>
    /// <param name="type">The barcode symbology.</param>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="outputPath">File path where the image will be saved.</param>
    /// <param name="maxAttempts">Maximum number of attempts.</param>
    /// <returns>True if the barcode was saved successfully; otherwise false.</returns>
    static bool GenerateBarcodeWithRetry(BaseEncodeType type, string codeText, string outputPath, int maxAttempts)
    {
        // Loop through attempts up to the specified maximum
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Ensure the directory for the output file exists
                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(type, codeText))
                {
                    // Enable checksum calculation (optional example setting)
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the generated barcode image to the output path
                    generator.Save(outputPath);
                }

                // Generation succeeded; exit the method
                return true;
            }
            catch (IOException ioEx)
            {
                // Temporary file system error (e.g., file locked, disk busy)
                Console.WriteLine($"Attempt {attempt} failed due to I/O error: {ioEx.Message}");

                // If this was the last allowed attempt, report failure
                if (attempt == maxAttempts)
                {
                    return false;
                }

                // Otherwise, continue to the next attempt
            }
            catch (BarCodeException bcEx)
            {
                // Barcode generation specific error – do not retry
                Console.WriteLine($"Barcode generation error: {bcEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Unexpected error – do not retry
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

        // Should not reach here, but return false as a safeguard
        return false;
    }
}