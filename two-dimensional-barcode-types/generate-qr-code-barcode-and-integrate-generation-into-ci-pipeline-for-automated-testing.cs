using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code for a predefined URL and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full output file path (current directory + file name)
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_code.png");

        // Extract the directory portion of the path
        string outputDir = Path.GetDirectoryName(outputPath);
        // Ensure the target directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a BarcodeGenerator instance for QR encoding with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level to improve readability under damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure image sizing using interpolation mode for smoother scaling
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points
            generator.Parameters.ImageHeight.Point = 300f;  // Height in points

            // Set the image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated QR code image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR Code generated at: {outputPath}");
    }
}