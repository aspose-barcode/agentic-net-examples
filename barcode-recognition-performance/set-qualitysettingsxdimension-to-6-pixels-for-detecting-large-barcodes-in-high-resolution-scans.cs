// Title: Detect Large Barcodes in High‑Resolution Scans Using XDimension Settings
// Description: Demonstrates how to generate a Code128 barcode, save it as PNG, and configure QualitySettings.XDimension to 6 pixels for reliable detection of large barcodes in high‑resolution images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with custom QualitySettings to handle large‑module barcodes. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and QualitySettings, which developers commonly use when processing high‑resolution scans where default module size detection may fail. Ideal for scenarios like inventory imaging, document scanning, and industrial automation where barcode size varies.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode, code128, qualitysettings, xdimension, recognition, highresolution, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it, and reads it using custom XDimension settings to detect large barcodes in high‑resolution scans.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, configures reader quality settings, performs recognition, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary file path for the sample barcode image
        // --------------------------------------------------------------------
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
        string codeText = "1234567890";

        // --------------------------------------------------------------------
        // Generate a Code128 barcode and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: adjust the module size (XDimension) if needed; default is 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode to the temporary PNG file
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using custom quality settings for large XDimension
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.Code128))
        {
            // Enable minimal XDimension mode and set the minimal dimension to 6 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 6f; // pixels

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // ----------------------------------------------------------------
            // Output recognition results
            // ----------------------------------------------------------------
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up the temporary barcode image file
        // --------------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}