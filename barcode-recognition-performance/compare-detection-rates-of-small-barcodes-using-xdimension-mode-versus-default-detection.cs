using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Code128 barcodes with different XDimension settings
/// and evaluates detection success using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, reads them back, and reports detection rates.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 10;               // Number of barcode samples to generate per setting
        const string codeText = "1234567890";     // Text to encode in each barcode

        // Containers for generated barcode images (as byte arrays)
        var smallBarcodes = new List<byte[]>();
        var defaultBarcodes = new List<byte[]>();

        // --------------------------------------------------------------------
        // Generate barcodes:
        //   - smallBarcodes: XDimension set to 1 point (very small modules)
        //   - defaultBarcodes: default XDimension (no explicit setting)
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            // ---- Small XDimension barcode ----
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Disable auto-sizing to allow manual XDimension control
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Set module size to 1 point
                generator.Parameters.Barcode.XDimension.Point = 1f;
                // Set bar height to 40 points
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                using (var ms = new MemoryStream())
                {
                    // Save barcode as PNG into memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Store the resulting byte array
                    smallBarcodes.Add(ms.ToArray());
                }
            }

            // ---- Default barcode (no explicit XDimension) ----
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Use default AutoSizeMode (Interpolation) and default XDimension
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    defaultBarcodes.Add(ms.ToArray());
                }
            }
        }

        // --------------------------------------------------------------------
        // Detect barcodes generated with small XDimension using XDimensionMode.Small
        // --------------------------------------------------------------------
        int smallDetected = 0;
        foreach (var data in smallBarcodes)
        {
            using (var ms = new MemoryStream(data))
            using (var bitmap = new Bitmap(ms))
            using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
            {
                // Instruct the reader to treat modules as small
                reader.QualitySettings.XDimension = XDimensionMode.Small;

                // Perform detection
                var results = reader.ReadBarCodes();

                // Verify detection succeeded and matched expected text
                if (results.Length > 0 && results[0].CodeText == codeText)
                    smallDetected++;
            }
        }

        // --------------------------------------------------------------------
        // Detect barcodes generated with default XDimension (auto mode)
        // --------------------------------------------------------------------
        int defaultDetected = 0;
        foreach (var data in defaultBarcodes)
        {
            using (var ms = new MemoryStream(data))
            using (var bitmap = new Bitmap(ms))
            using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
            {
                // No explicit XDimension setting; defaults to XDimensionMode.Auto
                var results = reader.ReadBarCodes();

                if (results.Length > 0 && results[0].CodeText == codeText)
                    defaultDetected++;
            }
        }

        // --------------------------------------------------------------------
        // Output detection rates for both settings
        // --------------------------------------------------------------------
        Console.WriteLine(
            $"Small XDimension detection rate: {smallDetected}/{sampleCount} ({(double)smallDetected / sampleCount:P0})");
        Console.WriteLine(
            $"Default detection rate: {defaultDetected}/{sampleCount} ({(double)defaultDetected / sampleCount:P0})");
    }
}