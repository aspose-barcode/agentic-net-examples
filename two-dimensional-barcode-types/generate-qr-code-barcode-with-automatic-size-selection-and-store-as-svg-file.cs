using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code and saving it as an SVG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code for a sample URL and writes it to an SVG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated SVG image.
        string outputPath = "qr_code.svg";

        // Initialize a QR code generator with the desired content (sample URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the generator to automatically size the QR code based on its content.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the error correction level; LevelM provides a good balance of data capacity and resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Attempt to save the generated QR code as an SVG file.
            // Wrapping the save operation in a try-catch block handles potential licensing restrictions.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR code saved to {outputPath}");
            }
            catch (Exception ex)
            {
                // Output an error message if the save operation fails.
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}