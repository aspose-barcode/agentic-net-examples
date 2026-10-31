// Title: Generate MaxiCode barcode with extended CodeText using Aspose.BarCode
// Description: Demonstrates how to build an extended CodeText for MaxiCode, set the encode mode to Extended, and save the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and extended CodeText handling. It showcases the use of MaxiCodeExtCodetextBuilder, BarcodeGenerator, and related parameter settings, which are common tasks for developers needing to encode multilingual or mixed data in MaxiCode barcodes.
// Prompt: Retrieve extended CodeText for MaxiCode using GetExtendedCodetext method and include it in generation.
// Tags: maxicode, extended codetext, barcode generation, png, aspnet.barcode, eciencoding, multilang

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with extended CodeText using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds extended CodeText, configures the generator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create a builder for extended MaxiCode CodeText
        var textBuilder = new MaxiCodeExtCodetextBuilder();

        // Add ECI-encoded segments with different character sets
        textBuilder.AddECICodetext(ECIEncodings.Win1251, "Will");
        textBuilder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");
        textBuilder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");

        // Add a plain (non-ECI) segment
        textBuilder.AddPlainCodetext("Plain text");

        // Retrieve the combined extended CodeText string
        string extendedCodeText = textBuilder.GetExtendedCodetext();

        // Define the output file path for the generated barcode image
        string outputPath = Path.Combine(Environment.CurrentDirectory, "MaxiCodeExtended.png");

        // Initialize the barcode generator with MaxiCode type and the extended CodeText
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodeText))
        {
            // Set visual parameters: module size and encode mode
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Extended;

            // Set the human‑readable text displayed below the barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Extended mode";

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}