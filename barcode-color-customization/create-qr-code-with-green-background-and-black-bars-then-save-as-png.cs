// Title: Generate QR Code with Green Background
// Description: Demonstrates creating a QR code with a green background and black bars, then saving it as a PNG file.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr, barcode, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with a green background and black bars using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Define the data to encode in the QR code.
            generator.CodeText = "Hello World";

            // Configure visual appearance: black bars on a green background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.Green;

            // Persist the generated QR code image as a PNG file.
            generator.Save("qr_green_background.png");
        }
    }
}