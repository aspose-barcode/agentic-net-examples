// Title: Compare Grayscale vs Color Barcode Detection Accuracy
// Description: Demonstrates generating barcodes in both grayscale (black on white) and full‑color (blue on yellow) formats, then reading them back to compare detection success across several common symbologies.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for detecting them, and QualitySettings for handling complex backgrounds. Developers often need to verify that barcodes remain readable after being rendered in different color schemes or image formats, especially when integrating scanning functionality into mobile or web applications.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode symbology, detection, grayscale, color, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates barcodes in grayscale and color, then reads them back to compare detection accuracy across multiple symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, attempts detection, reports results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the set of symbologies to test
        var symbologies = new (BaseEncodeType Encode, string Name)[]
        {
            (EncodeTypes.QR, "QR"),
            (EncodeTypes.Code128, "Code128"),
            (EncodeTypes.DataMatrix, "DataMatrix"),
            (EncodeTypes.Aztec, "Aztec"),
            (EncodeTypes.Pdf417, "Pdf417")
        };

        // Process each symbology
        foreach (var (encode, name) in symbologies)
        {
            string codeText = $"Test_{name}";
            string grayPath = Path.Combine(tempFolder, $"{name}_gray.png");
            string colorPath = Path.Combine(tempFolder, $"{name}_color.png");

            // Generate a grayscale barcode (black bars on white background)
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Save(grayPath, BarCodeImageFormat.Png);
            }

            // Generate a color barcode (blue bars on yellow background)
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.Yellow;
                generator.Save(colorPath, BarCodeImageFormat.Png);
            }

            // Attempt to read the grayscale image
            bool graySuccess = false;
            if (File.Exists(grayPath))
            {
                using (var reader = new BarCodeReader(grayPath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    graySuccess = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                }
            }

            // Attempt to read the color image with complex background handling enabled
            bool colorSuccess = false;
            if (File.Exists(colorPath))
            {
                using (var reader = new BarCodeReader(colorPath, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;
                    var results = reader.ReadBarCodes();
                    colorSuccess = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                }
            }

            // Output detection results for the current symbology
            Console.WriteLine($"{name}: Grayscale detection {(graySuccess ? "Success" : "Failure")}, Color detection {(colorSuccess ? "Success" : "Failure")}");
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the OS will eventually clean up the temporary files
        }
    }
}