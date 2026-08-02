// Title: Minimal XDimension Filtering for Code128 Barcode Recognition
// Description: Demonstrates how to generate a Code128 barcode, save it as PNG, and recognize it while filtering sub‑pixel noise using QualitySettings.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings to improve detection accuracy. Developers often need to fine‑tune XDimension settings to handle low‑resolution scans or noisy images, making this pattern common in barcode processing pipelines.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: code128, generation, recognition, png, barcodegenerator, barcodereader, qualitysettings, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode, saves it as an image, and reads it back using minimal XDimension filtering
/// to suppress sub‑pixel noise during recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saves the image, and performs recognition.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "sample_barcode.png";

        // Generate a simple Code128 barcode with the value "123456" and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Additional image configuration (size, colors, etc.) can be set here if needed.
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Initialize a BarCodeReader for the saved image, specifying that we expect a Code128 barcode.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Activate minimal XDimension filtering to ignore sub‑pixel noise.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f; // 1 pixel

            // Perform barcode recognition and output the results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }
    }
}