// Title: Generate MaxiCode barcodes and save as GIF images
// Description: Demonstrates how to create MaxiCode barcodes with different modes and save them as GIF files using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode parameters such as mode, X‑dimension, and colors, then output the result in GIF format. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode images for packaging and scanning solutions.
// Prompt: Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images.
// Tags: maxicode, barcode generation, gif, imageformat, aspose.barcode, encoding, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates several MaxiCode barcodes and saves them as GIF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, defines sample data, generates MaxiCode barcodes,
    /// configures appearance, and saves each barcode as a GIF file.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory for the output files
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeGif_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample data for three MaxiCode images (text and corresponding mode)
        var samples = new (string CodeText, MaxiCodeMode Mode)[]
        {
            ("Åspóse.Barcóde©", MaxiCodeMode.Mode4),
            ("[)>\u001e01\u001dB1050\u001d056\u001d001\u001dADDITIONAL DATA\u0004", MaxiCodeMode.Mode2),
            ("123456789\u001d056\u001d001\u001dADDITIONAL DATA\u0004", MaxiCodeMode.Mode2)
        };

        // Iterate over each sample, generate the barcode, and save it as a GIF
        for (int i = 0; i < samples.Length; i++)
        {
            var (codeText, mode) = samples[i];
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Set the specific MaxiCode mode for this barcode
                generator.Parameters.Barcode.MaxiCode.Mode = mode;

                // Configure image size and appearance
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Define the output file path and save the barcode as a GIF image
                string filePath = Path.Combine(outputDir, $"MaxiCode_{i + 1}.gif");
                generator.Save(filePath, BarCodeImageFormat.Gif);
                Console.WriteLine($"Saved: {filePath}");
            }
        }

        Console.WriteLine("All MaxiCode GIF images generated.");
    }
}