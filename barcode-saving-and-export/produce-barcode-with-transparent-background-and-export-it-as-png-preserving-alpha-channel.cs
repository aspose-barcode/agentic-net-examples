using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for a QR code with the desired text.
        // The generator implements IDisposable, so we use a using block to ensure proper cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Transparent Background"))
        {
            // Set the background color of the barcode to transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the bar (foreground) color to black.
            // This is the default value, but it is explicitly set here for clarity.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file.
            // PNG format preserves the alpha channel, maintaining transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}