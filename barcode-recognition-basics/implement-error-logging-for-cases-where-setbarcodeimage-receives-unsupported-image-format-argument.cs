// Title: Demonstrate handling unsupported image format when setting barcode image
// Description: Shows how BarCodeReader.SetBarCodeImage reacts to a non‑image file and logs the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of the BarCodeReader class to read barcodes from image files. Developers often need to validate input image formats before processing; this snippet demonstrates typical error handling when an unsupported format is supplied, a common requirement in robust barcode scanning applications.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode, error-logging, unsupported-format, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that attempts to load a non‑image file into <see cref="BarCodeReader"/> 
/// and logs any errors that occur during the operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary file with an unsupported format,
    /// tries to set it as the barcode image, and logs any exception thrown.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary working directory for the demo
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // --------------------------------------------------------------------
        // Create a dummy file with an unsupported image format (e.g., .txt)
        // --------------------------------------------------------------------
        string unsupportedFile = Path.Combine(tempDir, "dummy.txt");
        File.WriteAllText(unsupportedFile, "This is not an image.");

        // --------------------------------------------------------------------
        // Attempt to load the unsupported file into BarCodeReader
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            try
            {
                // This call is expected to throw because the file is not a valid image
                reader.SetBarCodeImage(unsupportedFile);

                // If no exception occurs, try to read any barcodes (unlikely to find any)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Log the error details for troubleshooting
                Console.WriteLine($"Error setting barcode image: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            File.Delete(unsupportedFile);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}