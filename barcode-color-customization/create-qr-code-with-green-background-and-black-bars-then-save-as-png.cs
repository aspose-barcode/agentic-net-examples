using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with a custom background color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with a green background and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the visual appearance:
            // - Set the background color of the barcode image to green.
            // - Set the color of the barcode bars (modules) to black.
            generator.Parameters.BackColor = Color.Green;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Persist the generated barcode to a PNG file.
            generator.Save("qr_green_background.png");
        }

        // Inform the user that the QR code has been successfully created.
        Console.WriteLine("QR code generated and saved as qr_green_background.png");
    }
}