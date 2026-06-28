using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image with retry logic for temporary I/O errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Configures barcode settings, ensures the output directory exists,
    /// and invokes the barcode generation with retry handling.
    /// </summary>
    static void Main()
    {
        // Define barcode encoding type and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample12345";

        // Determine the full path for the output image file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Maximum number of attempts to save the barcode in case of temporary failures.
        int maxRetryAttempts = 3;

        // Ensure the directory for the output file exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate the barcode and save it, applying retry logic on I/O exceptions.
        bool success = GenerateBarcodeWithRetry(encodeType, codeText, outputPath, maxRetryAttempts);

        // Inform the user of the result.
        Console.WriteLine(success
            ? $"Barcode saved successfully to '{outputPath}'."
            : $"Failed to save barcode after {maxRetryAttempts} attempts.");
    }

    /// <summary>
    /// Generates a barcode image and saves it to the specified path.
    /// Retries the save operation when a temporary <see cref="IOException"/> occurs.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="path">The file path where the image will be saved.</param>
    /// <param name="maxAttempts">Maximum number of save attempts before giving up.</param>
    /// <returns>True if the image was saved successfully; otherwise false.</returns>
    static bool GenerateBarcodeWithRetry(BaseEncodeType type, string text, string path, int maxAttempts)
    {
        int attempt = 0;

        // Continue attempting until the maximum number of attempts is reached.
        while (attempt < maxAttempts)
        {
            try
            {
                // Create a new barcode generator with the specified type and text.
                using (var generator = new BarcodeGenerator(type, text))
                {
                    // Configure generator parameters (e.g., resolution and auto-size mode).
                    generator.Parameters.Resolution = 300f;
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Save the generated barcode image to the target path.
                    generator.Save(path);
                }

                // If we reach this point, the save succeeded.
                return true;
            }
            catch (IOException ex)
            {
                // Handle temporary I/O errors by incrementing the attempt counter and logging.
                attempt++;
                Console.WriteLine($"Attempt {attempt} failed with IOException: {ex.Message}");

                // If we've exhausted all attempts, report failure.
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("No more retry attempts remaining.");
                    return false;
                }

                // Attempt to delete any partially written file before retrying.
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                    // Ignored – if deletion fails, the next attempt will overwrite or fail again.
                }
            }
            catch (Exception ex)
            {
                // For non-recoverable exceptions, log the error and abort further retries.
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

        // Should not reach here, but return false as a safeguard.
        return false;
    }
}