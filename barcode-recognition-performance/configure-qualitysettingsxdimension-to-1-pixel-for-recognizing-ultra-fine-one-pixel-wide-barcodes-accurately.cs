// Title: Ultra‑fine One‑Pixel Barcode Recognition
// Description: Demonstrates configuring QualitySettings.XDimension to 1 pixel to accurately read barcodes that are only one pixel wide.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: barcode, recognition, xdimension, code128, aspnet, aspose

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode and reads it using
/// a minimal XDimension setting of 1 pixel to handle ultra‑fine barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, and reads it
    /// with QualitySettings configured for a 1‑pixel XDimension.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Generate a simple barcode image to be recognized
        // ------------------------------------------------------------
        string imagePath = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Step 2: Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image could not be created.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Read the barcode using ultra‑fine XDimension configuration
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to use minimal XDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set the minimal XDimension to 1 pixel for ultra‑fine barcode detection
            reader.QualitySettings.MinimalXDimension = 1f;

            // Iterate through all detected barcodes and output their details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}