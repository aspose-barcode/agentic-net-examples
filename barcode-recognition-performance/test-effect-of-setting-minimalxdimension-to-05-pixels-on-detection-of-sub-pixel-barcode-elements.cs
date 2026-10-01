// Title: Effect of MinimalXDimension on Sub‑Pixel Code128 Barcode Detection
// Description: Demonstrates how setting MinimalXDimension to 0.5 pixels influences the detection of a Code128 barcode with sub‑pixel module size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings to control image rendering and decoding parameters. Developers often need to fine‑tune X‑dimension settings to improve read‑ability of low‑resolution or sub‑pixel barcodes, especially when working with compact layouts or high‑density data.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: code128, minimalxdimension, detection, png, generator, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a Code128 barcode with a very small XDimension,
/// then reads it back with and without the MinimalXDimension quality setting to
/// illustrate its impact on detection of sub‑pixel barcode elements.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates a temporary barcode image, performs
    /// recognition under two different settings, outputs the results, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMinimalXDim_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with a very small XDimension (module size)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a small module size (0.5 point ≈ 0.7 pixel). This creates sub‑pixel elements.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;
            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify the image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Local function to read a barcode with optional MinimalXDimension setting
        // --------------------------------------------------------------------
        static string ReadBarcode(string path, bool useMinimalXDimension, float minimalXDim)
        {
            // Use the appropriate decode type for Code128
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(path, decodeType))
            {
                if (useMinimalXDimension)
                {
                    // Enable minimal X dimension mode and set the threshold (pixels)
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minimalXDim;
                }

                // Perform recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    return results[0].CodeText;
                }
                else
                {
                    return null;
                }
            }
        }

        // --------------------------------------------------------------------
        // Read the barcode without MinimalXDimension (default settings)
        // --------------------------------------------------------------------
        string resultDefault = ReadBarcode(barcodePath, false, 0f);

        // --------------------------------------------------------------------
        // Read the barcode with MinimalXDimension set to 0.5 pixels
        // --------------------------------------------------------------------
        string resultWithMinimal = ReadBarcode(barcodePath, true, 0.5f);

        // --------------------------------------------------------------------
        // Output the comparison of detection results
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode detection results:");
        Console.WriteLine($"Default settings: {(resultDefault ?? "Not detected")}");
        Console.WriteLine($"With MinimalXDimension = 0.5px: {(resultWithMinimal ?? "Not detected")}");

        // --------------------------------------------------------------------
        // Clean up temporary files
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect the demo
        }
    }
}