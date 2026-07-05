// Title: Large XDimension Barcode Generation and Recognition
// Description: Demonstrates creating a Code128 barcode with an XDimension larger than 10 pixels and recognizing it using Large mode.
// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
// Tags: code128, barcode generation, barcode recognition, large xdimension, aspnet barcoderecognition, aspnet barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode with a large XDimension and then reads it using Large mode configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, verifies its creation, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Define the file name for the generated barcode image.
        const string imagePath = "large_barcode.png";

        // -------------------------------------------------
        // Generate a Code128 barcode with a large XDimension (>10 pixels).
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LargeXDimTest"))
        {
            // Disable auto-sizing so that XDimension directly controls the module size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set XDimension to 12 pixels (larger than the 10‑pixel threshold).
            generator.Parameters.Barcode.XDimension.Point = 12f;

            // Optionally increase the image canvas to accommodate the larger modules.
            generator.Parameters.ImageWidth.Point = 800f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image to disk.
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify that the barcode image file was successfully created.
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not created.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode using Large XDimension mode.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to handle large XDimension values.
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Perform recognition and output the results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}