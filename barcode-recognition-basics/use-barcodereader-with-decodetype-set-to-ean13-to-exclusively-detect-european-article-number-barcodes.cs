using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating an EAN13 barcode, saving it to a file,
/// reading the barcode back, and cleaning up the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, decodes it, and then deletes the image file.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image.
        string imagePath = "ean13.png";

        // ------------------------------------------------------------
        // Generate a sample EAN13 barcode (13 digits, including checksum)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the image file was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image using DecodeType.EAN13,
        // which restricts detection to EAN13 barcodes only.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Iterate through all detected barcodes (should be only one).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type and text of the detected barcode.
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up: delete the generated image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}