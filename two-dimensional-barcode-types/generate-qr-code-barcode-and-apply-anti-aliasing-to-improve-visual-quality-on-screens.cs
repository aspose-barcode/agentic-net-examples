using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the file path where the QR Code image will be saved.
        string outputPath = "qr_code.png";

        // Initialize a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Enable anti‑aliasing to produce smoother edges when rendering on screens.
            generator.Parameters.UseAntiAlias = true;

            // Set a higher resolution (dots per inch) for better image quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR Code image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user that the QR Code has been saved successfully.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}