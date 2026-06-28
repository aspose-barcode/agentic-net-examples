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

        // Initialize a BarcodeGenerator for Code128 symbology with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Increase the image resolution to 300 DPI for higher print quality (optional).
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}