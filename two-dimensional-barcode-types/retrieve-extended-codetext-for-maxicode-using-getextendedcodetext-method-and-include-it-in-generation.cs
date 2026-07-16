// Title: Retrieve extended CodeText for MaxiCode and generate barcode
// Description: Demonstrates building an extended CodeText for a MaxiCode barcode using MaxiCodeExtCodetextBuilder and generating the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and extended CodeText handling. It showcases the use of BarcodeGenerator, MaxiCodeExtCodetextBuilder, and related parameter classes to create barcodes with multiple ECI encodings and plain text. Developers often need to generate MaxiCode barcodes for logistics and shipping applications where extended character sets are required.
// Prompt: Retrieve extended CodeText for MaxiCode using GetExtendedCodetext method and include it in generation.
// Tags: maximicode, extendedcodetext, barcode generation, png, aspose.barcode, eciencoding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that builds an extended CodeText for a MaxiCode barcode,
/// configures the generator for extended encoding, and saves the barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Constructs extended CodeText, generates a MaxiCode barcode,
    /// and writes the output image to disk.
    /// </summary>
    static void Main()
    {
        // Build extended CodeText for MaxiCode using the builder.
        var extBuilder = new MaxiCodeExtCodetextBuilder();

        // Add ECI-encoded segments with different character sets.
        extBuilder.AddECICodetext(ECIEncodings.Win1251, "Will");
        extBuilder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");
        extBuilder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");

        // Add a plain (non‑ECI) segment.
        extBuilder.AddPlainCodetext("Plain text");

        // Retrieve the generated extended CodeText string.
        string extendedCodeText = extBuilder.GetExtendedCodetext();
        Console.WriteLine("Extended CodeText: " + extendedCodeText);

        // Create a MaxiCode barcode generator with the extended CodeText.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodeText))
        {
            // Enable extended encoding mode to correctly process the extended CodeText.
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Extended;

            // Set human‑readable text that will be displayed without control characters.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample MaxiCode";

            // Define output file path and save the barcode as a PNG image.
            string outputPath = "maxicode_extended.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine("Barcode saved to: " + outputPath);
        }
    }
}