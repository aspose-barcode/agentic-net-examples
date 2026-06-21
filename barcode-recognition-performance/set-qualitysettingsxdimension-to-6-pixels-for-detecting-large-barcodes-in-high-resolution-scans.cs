using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, reading it, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it using Aspose.BarCode, and deletes the temporary image.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and output file name.
        const string sampleCode = "1234567890";
        const string imagePath = "sample_barcode.png";

        // Generate a Code128 barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleCode))
        {
            // Save the generated barcode to the specified path.
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create sample barcode image at '{imagePath}'.");
            return;
        }

        // Create a BarCodeReader to decode the generated image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set the minimal X-dimension (in pixels) to improve detection of high‑resolution barcodes.
            // This replaces the older XDimension property which accepts an enum.
            reader.QualitySettings.MinimalXDimension = 6f;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary barcode image file.
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