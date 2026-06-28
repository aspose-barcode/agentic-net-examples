using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image
        const string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure visual appearance: white background and black bars for high contrast
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the image resolution to 300 DPI to improve scanning quality
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}