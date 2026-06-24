using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "transparent_barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology.
        // The 'using' statement ensures the generator is properly disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "123ABC";

            // Configure the barcode appearance:
            // - Make the background fully transparent.
            // - Set the bar (foreground) color to black (optional, default is black).
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}