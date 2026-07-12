// Title: Strip FNC Characters from Barcode Decoding
// Description: Demonstrates how to prevent stripping of FNC symbols when reading a GS1 Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to generate a GS1 Code128 barcode containing FNC1 characters, save it as a PNG image, and read it back while configuring BarCodeReader.StripFNC to retain those symbols. Developers working with GS1 symbologies often need to preserve FNC characters for accurate data extraction, making this pattern common in inventory, logistics, and retail applications.
// Prompt: Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results.
// Tags: gs1, code128, stripfnc, barcode decoding, aspose.barcode, image generation, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a GS1 Code128 barcode containing FNC1 characters, saves it as an image,
/// and reads it back while preserving the FNC symbols in the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, and cleanup.
    /// </summary>
    static void Main()
    {
        // Define the full path for the generated barcode image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

        // Create a GS1 Code128 barcode that includes FNC1 characters (represented by parentheses).
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231(10)ABC123"))
        {
            // Save the barcode image to a PNG file.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Initialize a barcode reader for Code128 and configure it to retain FNC characters.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // According to the task, set StripFNC to false (do not strip FNC characters).
            reader.BarcodeSettings.StripFNC = false;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the generated image file.
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