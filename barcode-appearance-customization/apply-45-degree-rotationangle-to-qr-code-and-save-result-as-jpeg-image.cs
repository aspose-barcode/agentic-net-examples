using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with a 45-degree rotation and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, rotates it, saves to file, and writes a confirmation to console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated QR code image
        string outputPath = "qr45.jpg";

        // Create a BarcodeGenerator instance for QR encoding with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set rotation angle to 45 degrees (clockwise)
            generator.Parameters.RotationAngle = 45f;

            // Save the generated barcode as a JPEG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the QR code image has been saved
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}