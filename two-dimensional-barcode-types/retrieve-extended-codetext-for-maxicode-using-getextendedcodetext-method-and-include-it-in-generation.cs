// Title: Generate MaxiCode barcode with extended CodeText using GetExtendedCodetext
// Description: Demonstrates how to build an extended CodeText for a MaxiCode barcode and generate the image. Shows usage of MaxiCodeExtCodetextBuilder and setting encode mode to Extended.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology and extended CodeText handling. It illustrates the use of MaxiCodeExtCodetextBuilder, BarcodeGenerator, and related parameters to create a MaxiCode barcode with mixed ECI and plain text. Developers working with 2‑D barcodes often need to embed multilingual data or custom payloads, and this snippet shows the typical steps for such scenarios.
// Prompt: Retrieve extended CodeText for MaxiCode using GetExtendedCodetext method and include it in generation.
// Tags: maxicode, extended codetext, barcode generation, c#, aspose.barcode, eciencoding

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
    /// Builds extended CodeText, configures the barcode generator, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_extended.png");

        // Create a builder for extended CodeText and add various ECI-encoded segments
        MaxiCodeExtCodetextBuilder builder = new MaxiCodeExtCodetextBuilder();
        builder.AddECICodetext(ECIEncodings.Win1251, "Will");
        builder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");
        builder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");
        // Add a plain (non‑ECI) segment
        builder.AddPlainCodetext("Plain text");

        // Retrieve the combined extended CodeText string
        string extendedCodeText = builder.GetExtendedCodetext();

        // Initialize the barcode generator with MaxiCode type and the extended CodeText
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodeText))
        {
            // Set visual parameters: pixel size and encode mode
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Extended;

            // Set display text for the 2‑D barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Extended mode";

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}