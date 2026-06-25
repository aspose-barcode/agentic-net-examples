using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator instance configured for Code128 encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode.
            generator.CodeText = "1234567890";

            // Align the human‑readable text (the code text) to the center of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic scaling to fit a narrow width while preserving image quality.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Specify the desired image dimensions in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 150f;   // Narrow width
            generator.Parameters.ImageHeight.Point = 50f;   // Height

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}