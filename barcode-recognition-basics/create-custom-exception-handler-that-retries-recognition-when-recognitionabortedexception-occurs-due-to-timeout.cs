using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition with retry logic.
/// </summary>
class Program
{
    // Maximum number of recognition attempts
    const int MaxRetryAttempts = 3;

    /// <summary>
    /// Entry point of the application.
    /// Generates a sample barcode image and attempts to recognize it with retry logic.
    /// </summary>
    static void Main()
    {
        // Define the path for the generated barcode image
        string imagePath = "sample_barcode.png";

        // Generate a simple Code128 barcode and save it to the specified path
        GenerateSampleBarcode(imagePath);

        // Attempt to recognize the barcode, retrying up to the maximum attempts
        RecognizeWithRetry(imagePath, MaxRetryAttempts);
    }

    // Generates a simple Code128 barcode and saves it to the specified path
    static void GenerateSampleBarcode(string outputPath)
    {
        // Create a barcode generator for Code128 with the given data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image using default settings
            generator.Save(outputPath);
        }
    }

    // Tries to recognize barcodes from an image, retrying on timeout-related exceptions
    static void RecognizeWithRetry(string imagePath, int maxAttempts)
    {
        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File '{imagePath}' does not exist.");
            return;
        }

        int attempt = 0;

        // Loop until successful recognition or maximum attempts reached
        while (attempt < maxAttempts)
        {
            attempt++;

            try
            {
                // Initialize the barcode reader for all supported types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Set a timeout (milliseconds) to simulate a timeout scenario
                    reader.Timeout = 5000; // 5 seconds

                    // Iterate through all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Attempt {attempt}: Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"Attempt {attempt}: Code text: {result.CodeText}");
                    }
                }

                // Recognition succeeded; exit the retry loop
                break;
            }
            catch (Exception ex)
            {
                // Determine if the exception is related to a timeout
                if (ex.Message != null && ex.Message.IndexOf("timeout", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"Attempt {attempt}: Recognition timed out. Retrying...");
                    // Continue to the next retry attempt
                }
                else
                {
                    // An unexpected error occurred; report and stop retrying
                    Console.WriteLine($"Attempt {attempt}: Unexpected error: {ex.Message}");
                    break;
                }
            }
        }

        // If the loop exited because the maximum attempts were exhausted, inform the user
        if (attempt == maxAttempts)
        {
            Console.WriteLine("Maximum retry attempts reached. Recognition failed.");
        }
    }
}