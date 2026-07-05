// Title: Barcode Generation and Reading with Minimal XDimension Filtering
// Description: Demonstrates generating a Code128 barcode, saving it to an image, and reading it back while filtering sub‑pixel noise using QualitySettings.UseMinimalXDimension.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: code128, barcode generation, barcode recognition, minimalxdimension, qualitysettings, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode image and reads it back
/// using minimal XDimension filtering to suppress sub‑pixel noise.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and then reads it with quality settings applied.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample barcode image and save it to disk.
        // ------------------------------------------------------------
        string imagePath = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode as a PNG file.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // 2. Verify that the barcode image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // 3. Read the barcode with minimal XDimension filtering enabled.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Activate UseMinimalXDimension mode to ignore sub‑pixel variations.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set the minimal XDimension threshold to 1 pixel.
            reader.QualitySettings.MinimalXDimension = 1f;

            // --------------------------------------------------------
            // 4. Process and output detected barcode information.
            // --------------------------------------------------------
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }
    }
}