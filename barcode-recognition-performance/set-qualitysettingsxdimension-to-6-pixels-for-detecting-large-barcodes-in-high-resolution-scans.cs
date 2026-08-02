// Title: Detect Large Barcode Using XDimension Setting
// Description: Demonstrates setting QualitySettings.XDimension to 6 pixels to detect large barcodes in high‑resolution scans.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to configure QualitySettings for accurate detection of high‑resolution barcodes. It uses BarCodeReader, QualitySettings, and XDimensionMode to adjust the minimal X‑dimension, a common requirement when scanning large or high‑density barcodes. Developers often need to tweak these settings to improve read reliability in industrial scanning applications.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode symbology, recognition, xdimension, highresolution, aspnet, aspose.barcode, cod128, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it to a file,
/// and then reads it back using custom QualitySettings to detect large barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, and reads it with
    /// XDimension configured to 6 pixels for high‑resolution scans.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode image and save it to disk.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image could not be created.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using BarCodeReader with custom QualitySettings.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Configure XDimension to detect large barcodes (6 pixels).
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 6f; // pixels

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}