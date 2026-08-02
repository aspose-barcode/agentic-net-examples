// Title: Gaussian Blur Removal Before Barcode Detection
// Description: Demonstrates preprocessing of a barcode image with Gaussian blur removal (deconvolution) to evaluate its effect on detection.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to generate a sample barcode, then use BarCodeReader with default settings and with high‑quality deconvolution (Fast mode) to compare detection results. Developers working with barcode scanning often need to improve read rates on blurred images, using classes such as BarcodeGenerator, BarCodeReader, QualitySettings, and DeconvolutionMode.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode symbology, deconvolution, blur removal, image preprocessing, barcode detection, aspose.barcode, code128, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it using default settings
/// and with Gaussian blur removal (deconvolution) enabled to compare detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        string barcodePath = "sample_barcode.png";

        // Ensure the barcode image exists; generate it if missing
        if (!File.Exists(barcodePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure basic visual appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode to a PNG file
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode image: {barcodePath}");
            }
        }
        else
        {
            Console.WriteLine($"Using existing barcode image: {barcodePath}");
        }

        // Local function to read the barcode and output detection results
        void ReadAndReport(string description, Action<BarCodeReader> configureReader)
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                // Apply any custom reader configuration (e.g., deconvolution settings)
                configureReader?.Invoke(reader);

                // Iterate through all detected barcodes and report them
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{description} - Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Read barcode with default settings (no preprocessing)
        ReadAndReport("Default Settings", null);

        // Read barcode with deconvolution (blur removal) enabled for high-quality detection
        ReadAndReport("Deconvolution (Fast) Enabled", r =>
        {
            r.QualitySettings = QualitySettings.HighQuality;
            r.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
        });
    }
}