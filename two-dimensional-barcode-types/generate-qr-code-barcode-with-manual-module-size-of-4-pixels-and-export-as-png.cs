using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code using Aspose.BarCode library and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code with the text "Hello World" and saves it to "qr.png".
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator (EncodeTypes.QR) within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the data to be encoded in the QR Code
            generator.CodeText = "Hello World";

            // Define the module size (X-dimension) as 4 points (approximately 4 pixels)
            generator.Parameters.Barcode.XDimension.Point = 4f;

            // Render and save the QR Code image as a PNG file named "qr.png"
            generator.Save("qr.png");
        }

        // Inform the user that the QR Code has been successfully generated and saved
        Console.WriteLine("QR Code generated and saved as qr.png");
    }
}