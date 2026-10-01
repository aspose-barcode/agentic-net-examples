// Title: Error handling for unsupported barcode image formats
// Description: Demonstrates how to catch and log errors when SetBarCodeImage is called with a file that is not a supported image format.
// Category-Description: This example belongs to the Aspose.BarCode image handling category, illustrating the use of BarCodeReader to load images, handling unsupported formats, and logging exceptions. Developers working with barcode scanning often need to validate input files and gracefully handle format errors using the BarCodeReader API.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode, error handling, unsupported format, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates error logging when attempting to set a barcode image with an unsupported file format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary file with an unsupported format,
    /// attempts to load it with BarCodeReader, logs any errors, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Create a dummy text file (unsupported image format for barcode reading)
        string unsupportedFile = Path.Combine(tempFolder, "sample.txt");
        File.WriteAllText(unsupportedFile, "This is not an image.");

        // Attempt to set the barcode image and log any errors
        TrySetBarCodeImage(unsupportedFile);

        // Clean up temporary files and folder
        try
        {
            File.Delete(unsupportedFile);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect the demo
        }
    }

    /// <summary>
    /// Tries to set the barcode image on a BarCodeReader instance and logs any exceptions.
    /// </summary>
    /// <param name="imagePath">Path to the image file to be used for barcode reading.</param>
    static void TrySetBarCodeImage(string imagePath)
    {
        // Verify that the file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use a BarCodeReader without an initial image; SetBarCodeImage will be called explicitly
        using (var reader = new BarCodeReader())
        {
            try
            {
                // This will throw if the file format is not supported as a barcode image
                reader.SetBarCodeImage(imagePath);
                Console.WriteLine($"Successfully set barcode image: {imagePath}");
            }
            catch (Exception ex)
            {
                // Log the error – unsupported image format or other issues
                Console.WriteLine($"Error setting barcode image for '{imagePath}': {ex.Message}");
            }
        }
    }
}