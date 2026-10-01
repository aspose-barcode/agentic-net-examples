// Title: Ultra‑Fine One‑Pixel Barcode Generation and Recognition
// Description: Demonstrates generating a Code128 barcode with a 1‑pixel XDimension and recognizing it using minimal XDimension settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for fine‑tuning recognition of ultra‑fine barcodes. Developers often need to handle very small module sizes in high‑density printing or scanning scenarios, and this snippet illustrates the key API classes and typical workflow.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: barcode, code128, generation, recognition, xdimension, qualitysettings, ultrafine, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a 1‑pixel wide Code128 barcode and reads it using minimal XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, reads it back, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodePath = Path.Combine(tempFolder, "ultrafine.png");

        // Generate a barcode with ultra‑fine (1 pixel) XDimension
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set module size to 1 pixel
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Prepare the decode type (Code128)
        BaseDecodeType decodeType = DecodeType.Code128;

        // Read the barcode with QualitySettings configured for minimal XDimension (1 pixel)
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Enable minimal XDimension mode and set the minimal dimension to 1 pixel
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}