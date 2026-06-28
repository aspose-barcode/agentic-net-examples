using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure visual appearance: white background and black bars for high contrast.
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the output file path.
            string outputPath = "barcode.png";

            // Save the generated barcode image to the specified file in PNG format.
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}