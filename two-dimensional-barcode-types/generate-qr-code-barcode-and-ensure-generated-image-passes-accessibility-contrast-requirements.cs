using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code and writes it to <c>qr.png</c>.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the QR code image.
        string outputPath = "qr.png";

        // Ensure the directory for the output file exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure barcode appearance: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Set the highest error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}