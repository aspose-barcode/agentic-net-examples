// Title: Set DataBar barcode height to 70px and compare JPEG vs PNG output sizes
// Description: Demonstrates how to set the bar height for continuous DataBar symbologies to 70 pixels, generate barcodes, and compare the file sizes of JPEG and PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on barcode appearance customization and image format handling. It uses BarcodeGenerator, BarCodeImageFormat, and related parameter classes to adjust dimensions and export images. Developers often need to control bar height, X‑dimension, and compare output formats for storage or printing requirements.
// Prompt: Set barcode height for continuous DataBar types to 70 pixels, compare JPEG and PNG outputs.
// Tags: databar, barcode height, image format comparison, jpeg, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates DataBar barcodes with a fixed height of 70 pixels and saves them as JPEG and PNG
/// to compare resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for each DataBar symbology, configures dimensions,
    /// saves images in two formats, and outputs size information to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // List of continuous DataBar symbologies to process
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked,
            EncodeTypes.DatabarTruncated
        };

        // Iterate over each symbology type
        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Choose appropriate code text based on symbology requirements
            string codeText = type == EncodeTypes.DatabarLimited
                ? "(01)08888888888888"
                : "(01)12345678901231";

            // Initialize the barcode generator with the selected type and text
            using (BarcodeGenerator generator = new BarcodeGenerator(type, codeText))
            {
                // Disable auto‑sizing so that explicit BarHeight takes effect
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set the bar height to 70 pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;

                // Optionally set a modest XDimension for better visual clarity
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the barcode as a JPEG image
                string jpegPath = Path.Combine(outputDir, $"{type.TypeName}_70px.jpeg");
                generator.Save(jpegPath, BarCodeImageFormat.Jpeg);

                // Save the same barcode as a PNG image
                string pngPath = Path.Combine(outputDir, $"{type.TypeName}_70px.png");
                generator.Save(pngPath, BarCodeImageFormat.Png);

                // Retrieve and display file sizes for comparison
                long jpegSize = new FileInfo(jpegPath).Length;
                long pngSize = new FileInfo(pngPath).Length;
                Console.WriteLine($"{type.TypeName}: JPEG size = {jpegSize} bytes, PNG size = {pngSize} bytes");
            }
        }
    }
}