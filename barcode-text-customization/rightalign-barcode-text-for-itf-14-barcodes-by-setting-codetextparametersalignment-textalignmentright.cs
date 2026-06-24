using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an ITF‑14 barcode with right‑aligned human‑readable text
/// and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures text alignment, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the sample ITF‑14 code (14 digits)
        const string codeText = "12345678901231";

        // Initialize a barcode generator for ITF‑14 using the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Configure the human‑readable text to be right‑aligned beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Define the output file name for the generated PNG image
            const string outputPath = "itf14_right_aligned.png";

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}