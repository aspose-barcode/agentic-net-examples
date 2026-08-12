// Title: Generate MaxiCode barcode and log generation parameters
// Description: This example creates a MaxiCode (Mode 4) barcode, logs its generation settings (mode, aspect ratio, encode mode), and saves the image as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for MaxiCode symbology. It uses MaxiCodeStandardCodetext, ComplexBarcodeGenerator, and related parameter classes to configure mode, aspect ratio, and encoding. Developers working with shipping, logistics, or inventory systems often need to produce MaxiCode barcodes and inspect or log their configuration for debugging or compliance.
// Prompt: Implement logging of MaxiCode generation parameters, including mode, aspect ratio, and encoding mode.
// Tags: maxicode, barcode, generation, logging, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode barcode, logs its parameters, and saves the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode (Mode 4), writes generation settings to the console, and saves the PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated MaxiCode barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Create a standard codetext object for a MaxiCode (Mode 4) with a simple message.
        var standardCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Use ComplexBarcodeGenerator to generate the barcode based on the codetext.
        using (var generator = new ComplexBarcodeGenerator(standardCodetext))
        {
            // Configure MaxiCode-specific parameters.
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f; // Height/Width ratio.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto;

            // Log the configured parameters to the console for verification.
            Console.WriteLine($"MaxiCode Mode      : {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"Aspect Ratio       : {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"Encode Mode        : {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Generate the barcode image and save it as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}