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

        // Initialize a BarcodeGenerator for Code128 symbology with the sample text "123456".
        // The generator implements IDisposable, so we use a using block to ensure proper resource cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the image resolution to 72 DPI.
            // This low resolution is suitable for quick preview thumbnails.
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved, including the resolution used.
        Console.WriteLine($"Barcode image saved to '{outputPath}' with 72 DPI resolution.");
    }
}