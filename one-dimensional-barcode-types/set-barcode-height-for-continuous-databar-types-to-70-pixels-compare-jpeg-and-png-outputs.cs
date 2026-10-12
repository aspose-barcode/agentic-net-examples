// Title: Set DataBar barcode height to 70px and compare PNG vs JPEG output sizes
// Description: Demonstrates how to configure the bar height for continuous DataBar symbologies to 70 pixels, generate PNG and JPEG images, and compare their file sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode parameter customization for DataBar symbologies. It shows usage of BarcodeGenerator, setting XDimension and BarHeight, and saving images in different formats (PNG, JPEG). Developers often need to adjust visual dimensions and evaluate output file size for web or print scenarios.
// Prompt: Set barcode height for continuous DataBar types to 70 pixels, compare JPEG and PNG outputs.
// Tags: databar, barcode height, image format, png, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates DataBar barcodes with a fixed height of 70 pixels,
/// saves them as PNG and JPEG files, and reports the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode parameters,
    /// saves images in two formats, and writes size information to the console.
    /// </summary>
    static void Main()
    {
        // Define and create the output folder for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "DataBarOutputs");
        Directory.CreateDirectory(outputDir);

        // List of DataBar symbologies to generate with their respective code texts
        var barcodes = new (string Name, BaseEncodeType Encode, string CodeText)[]
        {
            ("DatabarOmniDirectional", EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"),
            ("DatabarTruncated", EncodeTypes.DatabarTruncated, "(01)12345678901231"),
            ("DatabarLimited", EncodeTypes.DatabarLimited, "(01)08888888888888"),
            ("DatabarExpanded", EncodeTypes.DatabarExpanded, "(01)12345678901231")
        };

        // Iterate over each barcode definition, generate images, and compare sizes
        foreach (var (name, encode, codeText) in barcodes)
        {
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                // Optional: increase X-dimension for better visual clarity
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Set the bar height to 70 pixels as required
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;

                // Build file paths for PNG and JPEG outputs
                string pngPath = Path.Combine(outputDir, $"{name}_70px.png");
                string jpegPath = Path.Combine(outputDir, $"{name}_70px.jpg");

                // Save the barcode in PNG and JPEG formats
                generator.Save(pngPath, BarCodeImageFormat.Png);
                generator.Save(jpegPath, BarCodeImageFormat.Jpeg);

                // Retrieve file sizes for comparison
                long pngSize = new FileInfo(pngPath).Length;
                long jpegSize = new FileInfo(jpegPath).Length;

                // Output the size information to the console
                Console.WriteLine($"{name}: PNG size = {pngSize} bytes, JPEG size = {jpegSize} bytes");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}