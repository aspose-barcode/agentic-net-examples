// Title: Barcode Generation with AutoSizeMode and Dimension Logging
// Description: Demonstrates how to set different AutoSizeMode values when generating barcodes with Aspose.BarCode and logs the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, AutoSizeMode, and image size parameters. Developers often need to control barcode scaling and retrieve image dimensions for layout or validation purposes. The snippet illustrates typical usage patterns for setting AutoSizeMode, adjusting width/height or XDimension, and accessing bitmap properties.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode, autosizemode, image dimensions, generation, aspose.barcode, bitmap

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with various AutoSizeMode settings and logs image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for different symbologies and AutoSizeMode values, then writes their dimensions to the console.
    /// </summary>
    static void Main()
    {
        // Define sample data: each tuple contains the barcode type, text to encode, and desired AutoSizeMode.
        var samples = new List<(BaseEncodeType encodeType, string text, AutoSizeMode mode)>
        {
            (EncodeTypes.Code128, "12345", AutoSizeMode.None),
            (EncodeTypes.DataMatrix, "ASPOSE", AutoSizeMode.Interpolation),
            (EncodeTypes.QR, "Hello", AutoSizeMode.Nearest)
        };

        // Iterate through each sample, generate the barcode, and log its dimensions.
        foreach (var sample in samples)
        {
            // Create a BarcodeGenerator for the current sample.
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.text))
            {
                // Apply the specified AutoSizeMode.
                generator.Parameters.AutoSizeMode = sample.mode;

                // Adjust image size or XDimension based on the AutoSizeMode.
                if (sample.mode == AutoSizeMode.Interpolation || sample.mode == AutoSizeMode.Nearest)
                {
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;
                }
                else if (sample.mode == AutoSizeMode.None)
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                }

                // Generate the barcode image.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Log the encode type, AutoSizeMode, and resulting image dimensions.
                    Console.WriteLine($"EncodeType: {sample.encodeType}, AutoSizeMode: {sample.mode}, Width: {bitmap.Width}, Height: {bitmap.Height}");
                }
            }
        }
    }
}