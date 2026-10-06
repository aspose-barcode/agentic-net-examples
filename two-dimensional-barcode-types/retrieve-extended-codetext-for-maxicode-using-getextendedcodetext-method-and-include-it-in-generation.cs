// Title: Generate MaxiCode barcode with extended CodeText using Aspose.BarCode
// Description: Demonstrates building an extended CodeText for MaxiCode, including multiple ECI encodings and plain text, then generating a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and extended CodeText handling. It showcases the use of MaxiCodeExtCodetextBuilder, BarcodeGenerator, and related parameter settings to create barcodes with multilingual data. Developers often need to encode diverse character sets in MaxiCode for logistics and tracking applications.
// Prompt: Retrieve extended CodeText for MaxiCode using GetExtendedCodetext method and include it in generation.
// Tags: maxicode, extended codetext, eci encoding, barcode generation, c#, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a MaxiCode barcode using extended CodeText with multiple ECI encodings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds extended CodeText, generates a MaxiCode barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build extended CodeText for MaxiCode with various ECI encodings and plain text
        var builder = new MaxiCodeExtCodetextBuilder();
        builder.AddECICodetext(ECIEncodings.Win1251, "Will");
        builder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");
        builder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");
        builder.AddPlainCodetext("Plain text");
        string extendedCodeText = builder.GetExtendedCodetext();

        // Define the output file path for the generated barcode image
        string outputPath = Path.Combine(Environment.CurrentDirectory, "MaxiCodeExtended.png");

        // Generate the MaxiCode barcode using the extended CodeText
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodeText))
        {
            // Set visual parameters: module size and encode mode
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Extended;

            // Set display text for the 2D barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Extended mode";

            // Save the barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}