using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated QR code image will be saved.
        string outputPath = "qr_blue_white.png";

        // Initialize a BarcodeGenerator for QR encoding with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the barcode's foreground (bars) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Configure the barcode's background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated QR code as a PNG file to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}