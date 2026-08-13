// Title: Retrieve and Use Extended CodeText for MaxiCode Barcode
// Description: Demonstrates building an extended CodeText string for a MaxiCode barcode, retrieving it via GetExtendedCodetext, and generating the barcode image with that data.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to work with extended CodeText for 2D symbologies. It highlights the use of MaxiCodeExtCodetextBuilder, BarcodeGenerator, and related parameter settings—common tasks for developers needing to embed multiple character sets or plain text within a single barcode.
// Prompt: Retrieve extended CodeText for MaxiCode using GetExtendedCodetext method and include it in generation.
// Tags: barcode, maxicode, extendedcodetext, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a MaxiCode barcode using an extended CodeText that combines multiple ECI encodings and plain text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds extended CodeText, creates a MaxiCode barcode, and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output image file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_extended.png");

        // --------------------------------------------------------------------
        // Build the extended CodeText for MaxiCode.
        // --------------------------------------------------------------------
        var textBuilder = new MaxiCodeExtCodetextBuilder();

        // Add ECI-encoded segments with different character sets.
        textBuilder.AddECICodetext(ECIEncodings.Win1251, "Will");
        textBuilder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");
        textBuilder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");

        // Add a plain (non-ECI) text segment.
        textBuilder.AddPlainCodetext("Plain text");

        // Retrieve the combined extended CodeText string.
        string extendedCodetext = textBuilder.GetExtendedCodetext();

        // --------------------------------------------------------------------
        // Generate the MaxiCode barcode using the extended CodeText.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodetext))
        {
            // Configure the MaxiCode to use the Extended encode mode.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Extended;

            // Set the human‑readable text displayed below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My Text";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Output the result locations to the console for verification.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        Console.WriteLine($"Extended codetext used: {extendedCodetext}");
    }
}