// Title: Adjust DPI Settings When Loading Barcode Images
// Description: Demonstrates generating a QR barcode image with a specific DPI and reading it to verify accurate barcode region detection.
// Category-Description: This example belongs to the Aspose.BarCode image handling category, showcasing how to set image resolution (DPI) during barcode generation and how that DPI information aids the BarCodeReader in correctly locating barcode regions. It uses BarcodeGenerator for creation and BarCodeReader for recognition, common tasks for developers needing precise barcode scanning from high‑resolution images.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: qr, dpi, image, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR barcode image with a defined DPI, reads it back, and displays detection details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode with 300 DPI,
    /// reads the barcode, outputs detection information, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDPI_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode and embed DPI information (300 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set image resolution to 300 DPI; this value is stored in the PNG metadata
            generator.Parameters.Resolution = 300f;

            // Save the barcode image to the temporary location
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            try
            {
                // Attempt to read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    // Output details for each detected barcode
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Code Text : {result.CodeText}");
                        Console.WriteLine($"Symbology : {result.CodeTypeName}");
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                        Console.WriteLine($"Region Angle: {result.Region.Angle}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        Console.WriteLine();
                    }
                }
            }
            // Handle specific image‑loading failures (e.g., unsupported format)
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
            }
            // Catch any other unexpected exceptions
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        // Clean up temporary files and directory; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they are intentionally ignored
        }
    }
}