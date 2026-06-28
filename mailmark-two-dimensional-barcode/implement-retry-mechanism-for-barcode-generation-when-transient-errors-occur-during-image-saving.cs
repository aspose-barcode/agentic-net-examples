using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image with retry logic using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode and saves it to a file, handling transient errors with retries.
    /// </summary>
    static void Main()
    {
        // Define barcode settings: symbology type and text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        // Determine the full output file path (current directory + filename).
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate the barcode image with up to 3 attempts on transient failures.
        bool success = GenerateBarcodeWithRetry(encodeType, codeText, outputPath, maxAttempts: 3);

        // Inform the user of the result.
        Console.WriteLine(success
            ? $"Barcode saved successfully to '{outputPath}'."
            : $"Failed to save barcode after multiple attempts.");
    }

    /// <summary>
    /// Generates a barcode image and saves it to the specified path.
    /// Retries the save operation when a transient exception occurs.
    /// </summary>
    /// <param name="type">The barcode symbology.</param>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="outputPath">File path for the saved image.</param>
    /// <param name="maxAttempts">Maximum number of attempts.</param>
    /// <returns>True if the image was saved successfully; otherwise false.</returns>
    private static bool GenerateBarcodeWithRetry(BaseEncodeType type, string codeText, string outputPath, int maxAttempts)
    {
        // Loop through the allowed number of attempts.
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a barcode generator with the specified type and text.
                using (var generator = new BarcodeGenerator(type, codeText))
                {
                    // Optional: set image resolution (dots per inch).
                    generator.Parameters.Resolution = 300f;

                    // Save the generated barcode image to the target path.
                    generator.Save(outputPath);
                }

                // If we reach this point, the save succeeded; exit with success.
                return true;
            }
            catch (IOException ioEx)
            {
                // Transient I/O error (e.g., file locked). Log and retry.
                Console.WriteLine($"Attempt {attempt}: I/O error while saving barcode - {ioEx.Message}");
            }
            catch (BarCodeException bcEx)
            {
                // Transient barcode generation error. Log and retry.
                Console.WriteLine($"Attempt {attempt}: Barcode generation error - {bcEx.Message}");
            }
            catch (Exception ex)
            {
                // Non-transient error; log and abort further retries.
                Console.WriteLine($"Attempt {attempt}: Unexpected error - {ex.Message}");
                break;
            }
        }

        // All attempts exhausted without success.
        return false;
    }
}