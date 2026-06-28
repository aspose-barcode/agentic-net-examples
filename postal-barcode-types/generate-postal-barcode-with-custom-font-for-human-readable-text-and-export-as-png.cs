using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Postnet postal barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a Postnet barcode with custom font settings
    /// and writes the image to the file system.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "postal.png";

        // The numeric data to encode in the Postnet barcode.
        string codeText = "12345";

        // Specify the barcode symbology (Postnet) for the generator.
        BaseEncodeType encodeType = EncodeTypes.Postnet;

        // Create a BarcodeGenerator instance with the chosen symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the human‑readable text appearance:
            // - Use a monospaced font (Courier New) for better alignment.
            // - Set the font size to 12 points.
            // - Center the text horizontally.
            // - Position the text below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Courier New";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Render the barcode and save it as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine($"Postal barcode saved to: {outputPath}");
    }
}