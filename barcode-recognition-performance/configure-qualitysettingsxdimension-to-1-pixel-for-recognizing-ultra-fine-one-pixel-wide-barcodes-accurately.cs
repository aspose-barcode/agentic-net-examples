// Title: Ultra‑fine 1‑pixel barcode generation and recognition using XDimension settings
// Description: Demonstrates how to generate a Code128 barcode with a 1‑pixel module size and configure the reader's QualitySettings to accurately detect ultra‑fine barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of XDimension settings for high‑precision barcode scanning. It showcases the BarcodeGenerator, BarCodeReader, and QualitySettings classes, common when developers need to handle very narrow barcode elements, such as 1‑pixel wide modules, in imaging or scanning applications.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: code128, xdimension, barcode generation, barcode recognition, qualitysettings, ultra‑fine, pixel, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a 1‑pixel wide Code128 barcode and recognizing it using minimal XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode image, reads it with configured QualitySettings, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "XDimSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a barcode with ultra‑fine 1‑pixel XDimension
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set module size to 1 pixel (point unit)
            generator.Parameters.Barcode.XDimension.Point = 1f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            // Save the barcode as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using QualitySettings configured for 1‑pixel detection
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Instruct the reader to use minimal XDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Define the minimal element size as 1 pixel
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes detected: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}