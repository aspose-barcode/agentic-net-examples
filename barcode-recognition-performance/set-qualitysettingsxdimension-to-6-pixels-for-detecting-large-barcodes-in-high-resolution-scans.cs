// Title: Detect large barcodes in high‑resolution scans using XDimension setting
// Description: Demonstrates setting QualitySettings.XDimension to 6 pixels for reliable detection of large barcodes in high‑resolution images.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode, detection, high-resolution, xdimension, qualitysettings, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it, and reads it back
/// using custom <see cref="QualitySettings"/> to detect large barcodes in high‑resolution scans.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, verifies its creation, and reads it using
    /// <see cref="QualitySettings"/> with a minimal XDimension of 6 pixels.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        const string codeText = "1234567890";

        // Path where the generated barcode image will be saved
        const string imagePath = "barcode.png";

        // Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Persist the barcode image to disk
            generator.Save(imagePath);
        }

        // Ensure the image file was successfully created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a barcode reader for the saved image, targeting Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Start with a high‑performance preset as a baseline configuration
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Adjust XDimension detection to handle large barcodes:
            // - Use the minimal XDimension mode
            // - Set the minimal XDimension value to 6 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 6f; // 6 pixels

            // Perform barcode recognition and output results
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Detected code text: {result.CodeText}");
            }
        }
    }
}