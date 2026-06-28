using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with a predefined message,
    /// configures its parameters, saves it as a PNG file, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_note.png");

        // Initialize a QR code generator with the desired encoded text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Quick note: Meet at 10am"))
        {
            // Configure the QR code to use the highest error correction level (Level H) for robustness.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the image resolution to 300 DPI (float literal required by the API).
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG image to the specified output path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}