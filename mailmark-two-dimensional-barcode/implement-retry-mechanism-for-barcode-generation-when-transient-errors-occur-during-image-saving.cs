// Title: Barcode generation with retry on transient errors
// Description: Demonstrates generating a Code128 barcode and saving it as PNG with a retry mechanism for transient I/O or generation errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set visual parameters, and handle transient failures during image saving. Developers often need to implement retry logic when working with file systems or network shares to ensure reliable barcode creation in production environments.
// Prompt: Implement a retry mechanism for barcode generation when transient errors occur during image saving.
// Tags: barcode, code128, retry, io, exception handling, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode image with retry logic for transient errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it to a PNG file, and retries on transient failures.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the barcode image
        string outputPath = "barcode.png";

        // Ensure the target directory exists before attempting to save
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Configure retry parameters
        const int maxAttempts = 3; // maximum number of retry attempts
        int attempt = 0;           // current attempt counter
        bool success = false;      // flag indicating successful save

        // Retry loop: continue until success or max attempts reached
        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
                {
                    // Set visual appearance (optional)
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Save the generated barcode image to the specified path
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                // If no exception, mark as successful and inform the user
                success = true;
                Console.WriteLine($"Barcode saved successfully on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Handle transient I/O errors (e.g., file lock, network share issues)
                Console.WriteLine($"I/O error on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
            catch (BarCodeException bcEx)
            {
                // Handle transient barcode generation errors
                Console.WriteLine($"Barcode generation error on attempt {attempt}: {bcEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
            catch (Exception ex)
            {
                // Handle non‑transient, unexpected errors and abort further retries
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }

        // Exit code 0 indicates normal termination (implicit)
    }
}