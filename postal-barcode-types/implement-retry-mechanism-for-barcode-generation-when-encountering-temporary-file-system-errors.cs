// Title: Barcode Generation with Retry on File System Errors
// Description: Demonstrates generating a Code128 barcode image and saving it to disk with a retry mechanism that handles temporary I/O errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator to create barcodes, save them to files, and implement robust error handling for common file system issues such as IOExceptions and UnauthorizedAccessExceptions. Developers often need to ensure reliable barcode creation in batch or automated processes where transient file errors may occur.
// Prompt: Implement a retry mechanism for barcode generation when encountering temporary file system errors.
// Tags: barcode, symbology, generation, retry, io, exception handling, aspose.barcode, png, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode image with retry logic for temporary file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up parameters and invokes the barcode generation with retry.
    /// </summary>
    static void Main()
    {
        // Define the output file path (current directory + filename)
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Choose barcode symbology and text to encode
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        // Maximum number of retry attempts for transient file errors
        int maxAttempts = 3;

        try
        {
            // Attempt to generate and save the barcode with retry logic
            GenerateBarcodeWithRetry(outputFile, encodeType, codeText, maxAttempts);
        }
        catch (Exception ex)
        {
            // Log failure after exhausting all retry attempts
            Console.WriteLine($"Failed to generate barcode after {maxAttempts} attempts: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image and saves it to the specified path.
    /// Retries the operation when temporary file system errors occur.
    /// </summary>
    /// <param name="outputPath">Full file path to save the barcode image.</param>
    /// <param name="encodeType">The barcode symbology type.</param>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts.</param>
    static void GenerateBarcodeWithRetry(string outputPath, BaseEncodeType encodeType, string codeText, int maxAttempts)
    {
        // Loop through attempts up to the maximum specified
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Ensure the target directory exists before saving
                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create the barcode generator and save the image to disk
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Save(outputPath);
                }

                // Log success and exit the retry loop
                Console.WriteLine($"Barcode successfully saved to '{outputPath}' on attempt {attempt}.");
                break;
            }
            catch (IOException ioEx)
            {
                // Log I/O errors (e.g., file locked) and retry if attempts remain
                Console.WriteLine($"IO exception on attempt {attempt}: {ioEx.Message}");
                if (attempt == maxAttempts)
                    throw; // Rethrow after final attempt
            }
            catch (UnauthorizedAccessException uaEx)
            {
                // Log permission errors and retry if attempts remain
                Console.WriteLine($"Access exception on attempt {attempt}: {uaEx.Message}");
                if (attempt == maxAttempts)
                    throw;
            }
            catch (BarCodeException bcEx)
            {
                // Barcode-specific errors are not transient; abort without retry
                Console.WriteLine($"Barcode generation error on attempt {attempt}: {bcEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Unexpected errors are not retried
                Console.WriteLine($"Unexpected error on attempt {attempt}: {ex.Message}");
                throw;
            }
        }
    }
}