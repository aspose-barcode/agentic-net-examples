using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a fixed‑size QR Code image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code with a fixed size and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Path where the generated QR Code image will be saved.
        string outputPath = "qr_fixed.png";

        // Initialize a QR Code generator with the desired text (URL in this case).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Turn off automatic sizing so we can specify exact dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the image width and height in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Define the image resolution (dots per inch). This affects the quality of the saved PNG.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR Code image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}