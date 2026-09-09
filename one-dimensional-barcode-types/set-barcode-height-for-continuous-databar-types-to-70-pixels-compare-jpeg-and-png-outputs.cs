// Title: Set DataBar barcode height and compare JPEG vs PNG output sizes
// Description: Demonstrates how to set the bar height for continuous DataBar symbologies to 70 pixels and generate both JPEG and PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format options. It illustrates typical scenarios where developers need to customize barcode dimensions and compare different image outputs for storage or transmission efficiency. Ideal for developers working with barcode rendering, image optimization, and format-specific requirements.
// Prompt: Set barcode height for continuous DataBar types to 70 pixels, compare JPEG and PNG outputs.
// Tags: databar, barcode height, image format, jpeg, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that sets DataBar barcode height and saves images in JPEG and PNG formats for size comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates DataBar barcodes with a 70‑pixel height, saves them as JPEG and PNG, and reports file size differences.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated images.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarHeightComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the continuous DataBar symbologies to be processed.
        List<BaseEncodeType> dataBarTypes = new List<BaseEncodeType>
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarTruncated,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded
        };

        // Sample GS1-128 code text to encode.
        string codeText = "(01)12345678901231";

        // Iterate over each DataBar type, generate JPEG and PNG images, and compare their file sizes.
        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Build file paths for JPEG and PNG outputs.
            string jpegPath = Path.Combine(outputDir, $"{type.TypeName}_70px.jpeg");
            string pngPath = Path.Combine(outputDir, $"{type.TypeName}_70px.png");

            // Configure the barcode generator with desired dimensions and save both formats.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;      // Set module width.
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;    // Set bar height to 70 pixels.
                generator.Save(jpegPath, BarCodeImageFormat.Jpeg);      // Save as JPEG.
                generator.Save(pngPath, BarCodeImageFormat.Png);        // Save as PNG.
            }

            // Retrieve file sizes for comparison.
            long jpegSize = new FileInfo(jpegPath).Length;
            long pngSize = new FileInfo(pngPath).Length;
            long diff = Math.Abs(jpegSize - pngSize);

            // Output the size information to the console.
            Console.WriteLine($"{type.TypeName}: JPEG = {jpegSize} bytes, PNG = {pngSize} bytes, Difference = {diff} bytes");
        }

        // Inform the user where the images have been saved.
        Console.WriteLine($"Images saved to: {outputDir}");
    }
}