// Title: Set DataBar barcode height and compare PNG vs JPEG outputs
// Description: Demonstrates setting the bar height for continuous DataBar symbologies to 70 pixels and saving the barcodes as PNG and JPEG to compare file sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode dimensions using the BarcodeGenerator class. It covers typical use cases such as customizing bar height for DataBar symbologies and exporting images in different formats. Developers often need to adjust visual properties and evaluate output size for web or print scenarios.
// Prompt: Set barcode height for continuous DataBar types to 70 pixels, compare JPEG and PNG outputs.
// Tags: databar, barcode, height, image, png, jpeg, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates continuous DataBar barcodes with a custom height and saves them as PNG and JPEG
/// to illustrate size differences between the two image formats.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes for various DataBar symbologies,
    /// sets a fixed bar height, saves each barcode in PNG and JPEG formats,
    /// and writes the resulting file sizes to the console.
    /// </summary>
    static void Main()
    {
        // Define the continuous DataBar symbologies and their associated code text.
        var dataBarTypes = new (BaseEncodeType type, string codeText)[]
        {
            (EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"),
            (EncodeTypes.DatabarStacked, "(01)12345678901231"),
            (EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"),
            (EncodeTypes.DatabarExpanded, "(01)12345678901231"),
            (EncodeTypes.DatabarExpandedStacked, "(01)12345678901231")
        };

        // Ensure the output directory exists.
        string outputDir = "Output";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Iterate over each DataBar type, generate the barcode, and save in both formats.
        foreach (var (type, codeText) in dataBarTypes)
        {
            // Initialize the barcode generator with the specific DataBar type and code text.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Disable automatic sizing so that the explicit BarHeight is applied.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set the bar height to 70 pixels.
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;

                // Save the barcode as a PNG file.
                string pngPath = Path.Combine(outputDir, $"{type}.png");
                generator.Save(pngPath, BarCodeImageFormat.Png);

                // Save the barcode as a JPEG file.
                string jpgPath = Path.Combine(outputDir, $"{type}.jpg");
                generator.Save(jpgPath, BarCodeImageFormat.Jpeg);

                // Retrieve file sizes for comparison.
                long pngSize = new FileInfo(pngPath).Length;
                long jpgSize = new FileInfo(jpgPath).Length;

                // Output the size comparison to the console.
                Console.WriteLine($"{type}: PNG size = {pngSize} bytes, JPEG size = {jpgSize} bytes");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}