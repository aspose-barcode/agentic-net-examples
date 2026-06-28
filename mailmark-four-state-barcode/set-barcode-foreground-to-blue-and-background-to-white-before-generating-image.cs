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
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 format with the sample text "1234567890".
        // The 'using' statement ensures the generator is properly disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the barcode appearance:
            // Set the color of the barcode bars (foreground) to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to the specified file.
            // The default image format is PNG.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}