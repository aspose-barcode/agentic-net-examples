using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures its appearance,
    /// and writes the result to a TIFF file in the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output TIFF file in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.tiff");

        // Initialize a BarcodeGenerator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Specify the data to encode in the barcode.
            generator.CodeText = "1234567890";

            // Set the bar (foreground) color to lime (RGB 0,255,0).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 255, 0);

            // Define the image resolution (dots per inch) for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}