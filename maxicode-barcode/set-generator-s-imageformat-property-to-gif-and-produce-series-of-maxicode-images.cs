using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of MaxiCode barcodes in different modes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output directory,
    /// generates MaxiCode images for several modes, and reports the result.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeImages");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate MaxiCode images for Mode 4, Mode 5, and Mode 6.
        GenerateMaxiCode(outputDir, MaxiCodeMode.Mode4, "Sample message for Mode 4", "maxicode_mode4.gif");
        GenerateMaxiCode(outputDir, MaxiCodeMode.Mode5, "Sample message for Mode 5", "maxicode_mode5.gif");
        GenerateMaxiCode(outputDir, MaxiCodeMode.Mode6, "Sample message for Mode 6", "maxicode_mode6.gif");

        // Inform the user where the images have been saved.
        Console.WriteLine("MaxiCode images have been generated in: " + outputDir);
    }

    /// <summary>
    /// Generates a MaxiCode barcode image using the specified mode, message, and file name.
    /// </summary>
    /// <param name="folder">The directory where the image will be saved.</param>
    /// <param name="mode">The MaxiCode mode to use (e.g., Mode4, Mode5, Mode6).</param>
    /// <param name="message">The text message to encode in the barcode.</param>
    /// <param name="fileName">The name of the output image file.</param>
    private static void GenerateMaxiCode(string folder, MaxiCodeMode mode, string message, string fileName)
    {
        // Prepare the standard MaxiCode codetext with the specified mode and message.
        var codetext = new MaxiCodeStandardCodetext
        {
            Mode = mode,
            Message = message
        };

        // Use a ComplexBarcodeGenerator to create and save the barcode image.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Combine the folder path and file name to get the full output path.
            string outputPath = Path.Combine(folder, fileName);

            // Save the generated barcode as a GIF image.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }
    }
}