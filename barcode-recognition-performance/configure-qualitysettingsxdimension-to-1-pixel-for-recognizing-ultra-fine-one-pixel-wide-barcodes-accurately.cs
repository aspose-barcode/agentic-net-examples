// Title: Ultra‑fine One‑Pixel Barcode Recognition Example
// Description: Demonstrates configuring QualitySettings.XDimension to 1 pixel for accurate detection of ultra‑fine barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to adjust XDimension settings using the BarCodeReader and QualitySettings classes. Developers often need to recognize very narrow barcodes in high‑resolution images; this snippet shows typical usage for Code128 symbology and minimal XDimension configuration.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: barcode, recognition, xdimension, code128, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode with a 1‑pixel XDimension, saves it, and reads it back using minimal XDimension settings.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the sample barcode image.
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a simple Code128 barcode and save it to the file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a small XDimension (1 point) for the generated image to simulate an ultra‑fine barcode.
            generator.Parameters.Barcode.XDimension.Point = 1f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode using ultra‑fine XDimension settings.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure QualitySettings to detect 1‑pixel wide modules.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform recognition and output results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
            }
        }

        // Clean up the temporary image file.
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit.
        }
    }
}