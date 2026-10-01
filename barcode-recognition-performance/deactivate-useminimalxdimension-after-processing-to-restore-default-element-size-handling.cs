// Title: Deactivate UseMinimalXDimension after barcode processing
// Description: Demonstrates enabling UseMinimalXDimension for barcode reading and then restoring default X-dimension handling.
// Category-Description: This example belongs to the Aspose.BarCode reading and quality settings category. It shows how to generate a barcode image, configure the BarCodeReader's QualitySettings to use minimal X-dimension mode for precise element sizing, and then revert to normal X-dimension handling. Developers working with barcode recognition often need to adjust X-dimension settings for specific scanning requirements, using classes like BarcodeGenerator, BarCodeReader, and XDimensionMode.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode, code128, reading, x-dimension, useminimalxdimension, aspose.barcode, png, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with minimal X‑dimension mode,
/// then deactivates the mode to restore default element size handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with
    /// <c>UseMinimalXDimension</c>, and then restores the default X‑dimension setting.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");

        // Generate a simple Code128 barcode and save it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // No special sizing settings – default auto-sizing based on content.
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Create a BarCodeReader configured for Code128 decoding.
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.Code128))
        {
            // Activate UseMinimalXDimension to force minimal X-dimension handling.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            Console.WriteLine("UseMinimalXDimension enabled.");

            // Perform barcode reading and output the results.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
            }

            // Deactivate UseMinimalXDimension to restore default element size handling.
            reader.QualitySettings.XDimension = XDimensionMode.Normal;
            Console.WriteLine("UseMinimalXDimension deactivated; default X-dimension restored.");
        }

        // Clean up the temporary image file.
        try
        {
            File.Delete(tempImagePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}