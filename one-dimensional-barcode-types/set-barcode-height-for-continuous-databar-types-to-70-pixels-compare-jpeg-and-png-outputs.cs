using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of various DataBar barcode symbologies
/// and compares PNG vs JPEG file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, saves them in PNG and JPEG formats,
    /// and outputs the file size comparison to the console.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where barcode images will be saved.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the set of DataBar symbologies to be generated.
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked
        };

        // Iterate over each symbology type.
        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Choose the appropriate codetext based on the symbology.
            // DatabarLimited requires a different example value.
            string codeText = type == EncodeTypes.DatabarLimited
                ? "(01)08888888888888"
                : "(01)12345678901231";

            // Create a barcode generator for the current type and codetext.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Disable automatic sizing to enforce a fixed bar height.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = 70f;

                // Save the barcode as a PNG image.
                string pngPath = Path.Combine(outputDir, $"{type}_70px.png");
                generator.Save(pngPath, BarCodeImageFormat.Png);

                // Save the barcode as a JPEG image.
                string jpgPath = Path.Combine(outputDir, $"{type}_70px.jpg");
                generator.Save(jpgPath, BarCodeImageFormat.Jpeg);

                // Retrieve file sizes for comparison.
                long pngSize = new FileInfo(pngPath).Length;
                long jpgSize = new FileInfo(jpgPath).Length;

                // Output the size comparison to the console.
                Console.WriteLine($"{type}: PNG size = {pngSize} bytes, JPEG size = {jpgSize} bytes");
            }
        }

        // Indicate that the process has completed.
        Console.WriteLine("Barcode generation completed. Check the 'output' folder.");
    }
}