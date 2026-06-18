using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with explicit image dimensions
/// using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, configures its size, and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Create a BarcodeGenerator instance with the specified type and text.
        // The generator is wrapped in a using statement to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the auto-size mode to Interpolation, which allows explicit size control.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define explicit image dimensions in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;   // Width: 300 points
            generator.Parameters.ImageHeight.Point = 150f;  // Height: 150 points

            // Specify the output file path and save the generated barcode as PNG.
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}