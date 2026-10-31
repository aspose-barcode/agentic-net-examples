// Title: DataBar Height Adjustment and Image Format Comparison
// Description: Demonstrates setting the barcode height for continuous DataBar symbologies to 70 pixels and compares the resulting JPEG and PNG file sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class and its Parameters. It shows typical use cases such as customizing bar height for DataBar types and evaluating output image formats (JPEG vs PNG) for size and quality considerations. Developers working with barcode rendering often need to adjust visual properties and compare formats for storage or transmission efficiency.
// Prompt: Set barcode height for continuous DataBar types to 70 pixels, compare JPEG and PNG outputs.
// Tags: databar, barcode height, image format comparison, jpeg, png, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting barcode height for continuous DataBar types and comparing JPEG and PNG outputs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates DataBar barcodes with a height of 70 pixels, saves them as JPEG and PNG, and reports file sizes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarHeightDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample GS1-128 code text used for all barcodes
        string codeText = "(01)12345678901231";

        // Define the continuous DataBar symbologies to be processed
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarTruncated,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded
        };

        // Iterate over each DataBar type, generate images, and compare file sizes
        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Retrieve a friendly name for the current symbology
            string typeName = type.TypeName;

            // Build file paths for JPEG and PNG outputs
            string jpegPath = Path.Combine(outputDir, $"{typeName}_70px.jpg");
            string pngPath = Path.Combine(outputDir, $"{typeName}_70px.png");

            // Generate the barcode with the specified height and save in both formats
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Set barcode height to 70 pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;

                // Save as JPEG
                generator.Save(jpegPath, BarCodeImageFormat.Jpeg);

                // Save as PNG
                generator.Save(pngPath, BarCodeImageFormat.Png);
            }

            // Retrieve file sizes for comparison
            long jpegSize = new FileInfo(jpegPath).Length;
            long pngSize = new FileInfo(pngPath).Length;

            // Output the size information to the console
            Console.WriteLine($"{typeName}: JPEG size = {jpegSize} bytes, PNG size = {pngSize} bytes");
        }

        // Inform the user where the images have been saved
        Console.WriteLine($"Generated images are located in: {outputDir}");
    }
}