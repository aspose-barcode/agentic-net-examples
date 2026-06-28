using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code and saves it as a BMP file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.bmp";

        // Create a BarcodeGenerator instance configured for QR encoding with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose!"))
        {
            // Note: Mask pattern setting is not available in the current API version.
            // Save the generated QR code to the specified path in BMP format.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}