// Title: Generate MaxiCode barcode and log generation parameters
// Description: Demonstrates creating a MaxiCode barcode (Mode2) with custom X dimension and aspect ratio, logging its mode, aspect ratio, and encoding mode, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.MaxiCode. It shows typical use cases such as setting MaxiCode-specific parameters (Mode, AspectRatio, EncodeMode) and retrieving them for logging or debugging. Developers working with shipping labels, logistics, or inventory systems often need to generate MaxiCode symbols and verify their configuration.
// Prompt: Implement logging of MaxiCode generation parameters, including mode, aspect ratio, and encoding mode.
// Tags: maxicode, barcode generation, logging, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, logging its parameters, and saving the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, logs parameters, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Define temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeSample.png");

        // Build the codetext according to MaxiCode Mode2 format
        string gs = "\u001d";
        string rs = "\u001e";
        string eot = "\u0004";
        string codetext = $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}ADDITIONAL DATA{eot}";

        // Create a barcode generator for MaxiCode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Configure barcode appearance and MaxiCode-specific settings
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 0.5f;

            // Log the configured MaxiCode parameters
            Console.WriteLine($"MaxiCode Mode: {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"Aspect Ratio: {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"Encode Mode: {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}