// Title: Large XDimension Barcode Generation and Recognition
// Description: Demonstrates creating a Code128 barcode with an XDimension larger than 10 pixels and recognizing it using Large mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to configure XDimension for high‑resolution barcodes. It uses BarcodeGenerator, BarCodeReader, and related quality settings, which developers often need when handling large‑format barcodes in printing or scanning applications.
// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
// Tags: barcode, code128, xdimension, large mode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode with a large XDimension
/// and then reads it back using the Large XDimension recognition mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it,
    /// outputs detection results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "large_xdimension_barcode.png");

        // Ensure any existing file is removed before generation
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // -------------------- Barcode Generation --------------------
        // Create a Code128 barcode with a sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LargeXDimTest123"))
        {
            // Set a large XDimension (>10 pixels) to simulate a high‑resolution barcode
            generator.Parameters.Barcode.XDimension.Pixels = 12f; // 12 pixels per module

            // Disable auto‑size to keep the XDimension effect unchanged
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode image to the temporary path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // -------------------- Barcode Recognition --------------------
        // Initialize a reader for the generated image, targeting Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to expect large XDimension barcodes
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Perform recognition and output each detected barcode's details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Recognition XDimension Mode: {reader.QualitySettings.XDimension}");
            }
        }

        // Clean up the temporary image file after processing
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored – file may be locked or already removed
        }
    }
}