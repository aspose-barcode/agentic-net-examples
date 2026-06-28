using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image and rotating it 180 degrees using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code, rotates it, saves to file, and writes the path to console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_rotated.png";

        // Initialize a BarcodeGenerator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set rotation angle to 180 degrees to display the QR code upside‑down.
            generator.Parameters.RotationAngle = 180f;

            // Save the rotated QR code image to the specified file.
            generator.Save(outputPath);
        }

        // Output the location of the saved QR code image to the console.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}