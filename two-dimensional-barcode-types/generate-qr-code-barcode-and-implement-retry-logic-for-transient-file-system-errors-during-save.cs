using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code for a sample URL and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Initialize the QR code generator with the desired text (URL) and QR encoding type.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure optional QR code parameters, such as error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Define retry policy parameters for handling transient I/O errors.
            const int maxRetries = 3;

            // Attempt to save the generated QR code, retrying on failure up to maxRetries times.
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Save the QR code image to the specified file path.
                    generator.Save(outputPath);

                    // Inform the user of successful save operation.
                    Console.WriteLine($"QR code saved successfully to '{outputPath}'.");
                    break; // Exit the retry loop on success.
                }
                catch (IOException ioEx)
                {
                    // If this was the final attempt, report the failure.
                    if (attempt == maxRetries)
                    {
                        Console.WriteLine($"Failed to save QR code after {maxRetries} attempts: {ioEx.Message}");
                    }
                    else
                    {
                        // Otherwise, log the error and continue to the next retry attempt.
                        Console.WriteLine($"Attempt {attempt} failed: {ioEx.Message}. Retrying...");
                    }
                }
            }
        }
    }
}