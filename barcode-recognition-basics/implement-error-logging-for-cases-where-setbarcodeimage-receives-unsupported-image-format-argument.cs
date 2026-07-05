// Title: Demonstrate error handling for unsupported image format in BarCodeReader
// Description: Shows how to catch and log errors when SetBarCodeImage receives a file that is not a supported image format, using Aspose.BarCode.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode symbology, error handling, image format, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that attempts to read a barcode from an unsupported image file
/// and logs appropriate error messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a dummy non‑image file, tries to read a barcode,
    /// and demonstrates error handling for unsupported image formats.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a dummy file with an unsupported image format (e.g., a text file)
        // --------------------------------------------------------------------
        string unsupportedFilePath = "unsupported.txt";
        if (!File.Exists(unsupportedFilePath))
        {
            File.WriteAllText(unsupportedFilePath, "This is not an image.");
        }

        // --------------------------------------------------------------------
        // Attempt to use BarCodeReader with the unsupported file.
        // BarCodeReader expects a supported image format (png, jpg, bmp, etc.).
        // The constructor will throw an exception for unsupported formats.
        // --------------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(unsupportedFilePath, DecodeType.Code128))
            {
                // If no exception, attempt to read (unlikely for unsupported format)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Found barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Log the specific Aspose.BarCode exception indicating unsupported format
            Console.WriteLine($"Error: Unsupported image format for SetBarCodeImage. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log any other unexpected exceptions
            Console.WriteLine($"Unexpected error: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // --------------------------------------------------------------------
            // Clean up the dummy file
            // --------------------------------------------------------------------
            if (File.Exists(unsupportedFilePath))
            {
                File.Delete(unsupportedFilePath);
            }
        }
    }
}