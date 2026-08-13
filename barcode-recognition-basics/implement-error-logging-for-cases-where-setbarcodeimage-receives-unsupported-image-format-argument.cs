// Title: Demonstrate barcode generation, reading, and error logging for unsupported image formats
// Description: The example generates a Code128 barcode, reads it from a PNG image, then attempts to load a non‑image file and logs the resulting error.
// Category-Description: This sample belongs to the Aspose.BarCode image handling category, illustrating how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to handle invalid image inputs with SetBarCodeImage. Developers working with barcode generation and recognition often need to validate image sources and log errors when unsupported formats are encountered. The example showcases key classes such as BarcodeGenerator, BarCodeReader, and Image handling from Aspose.Drawing.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode symbology, generation, recognition, error handling, image format, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it, and demonstrates error logging
/// when attempting to set a barcode image from an unsupported file format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a valid barcode image (PNG) using BarcodeGenerator
        // ------------------------------------------------------------
        const string validImagePath = "valid.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(validImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Create a file with an unsupported image format (plain text)
        // ------------------------------------------------------------
        const string unsupportedPath = "unsupported.txt";
        File.WriteAllText(unsupportedPath, "This is not an image.");

        // ------------------------------------------------------------
        // Read barcode from the valid image (normal operation)
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(validImagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read from valid image: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Attempt to set a barcode image using an unsupported format
        // ------------------------------------------------------------
        try
        {
            // Load the file as an Image; this will succeed for any file but will fail later when casting
            using (Image image = Image.FromFile(unsupportedPath))
            {
                // BarCodeReader supports setting the image via SetBarCodeImage
                using (var reader = new BarCodeReader())
                {
                    // Cast to Bitmap as required by SetBarCodeImage; this throws for non‑image files
                    reader.SetBarCodeImage((Bitmap)image);

                    // Attempt to read (won't be reached for unsupported format)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Unexpected read: {result.CodeText}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the error to console
            Console.WriteLine($"Error loading unsupported image format: {ex.Message}");

            // Append detailed error information to a simple log file
            File.AppendAllText(
                "error.log",
                $"[{DateTime.Now}] Unsupported image load error: {ex}{Environment.NewLine}");
        }
    }
}