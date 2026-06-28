using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code, rotating it, and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code with sample text, rotates it 90 degrees clockwise,
    /// saves it to the current directory, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_rotated.png");

        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose!"))
        {
            // Set the rotation angle to 90 degrees (clockwise) for the generated barcode.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated QR code image to the specified file path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}