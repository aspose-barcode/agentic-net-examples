using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creation of a MaxiCode barcode with extended codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Builds extended codetext, generates a MaxiCode barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Build extended codetext for MaxiCode
        // ------------------------------------------------------------
        var textBuilder = new MaxiCodeExtCodetextBuilder();

        // Add ECIs (Extended Channel Interpretations) with different encodings
        textBuilder.AddECICodetext(ECIEncodings.Win1251, "Will");          // Windows-1251 encoding
        textBuilder.AddECICodetext(ECIEncodings.UTF8, "犬Right狗");        // UTF-8 encoding with Unicode characters
        textBuilder.AddECICodetext(ECIEncodings.UTF16BE, "犬Power狗");    // UTF-16BE encoding with Unicode characters

        // Add plain (non‑ECI) text segment
        textBuilder.AddPlainCodetext("Plain text");

        // Retrieve the combined extended codetext string
        string extendedCodeText = textBuilder.GetExtendedCodetext();

        // ------------------------------------------------------------
        // Output the extended codetext to the console for verification
        // ------------------------------------------------------------
        Console.WriteLine("Extended CodeText for MaxiCode:");
        Console.WriteLine(extendedCodeText);

        // ------------------------------------------------------------
        // Generate MaxiCode barcode using the extended codetext
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodeText))
        {
            // Optional: set human‑readable text displayed below the barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My Text";

            // Determine output file path (current directory)
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}