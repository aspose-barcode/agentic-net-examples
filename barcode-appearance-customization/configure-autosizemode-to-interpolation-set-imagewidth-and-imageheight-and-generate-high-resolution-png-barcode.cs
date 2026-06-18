using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode with specific dimensions and resolution, then writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code128) and the data to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Destination file for the generated barcode image.
        string outputPath = "high_res_barcode.png";

        // Create a BarcodeGenerator instance and configure it for high‑resolution output.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use interpolation mode so the barcode is scaled to exact dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image size in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 600f;   // Width of the image.
            generator.Parameters.ImageHeight.Point = 300f;  // Height of the image.

            // Define the image resolution (dots per inch). 300 DPI yields a high‑quality image.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}