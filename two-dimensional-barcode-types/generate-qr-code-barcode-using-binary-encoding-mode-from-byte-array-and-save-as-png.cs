using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a QR Code in binary mode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code from a byte array and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample byte array to encode in binary mode.
        byte[] data = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0x10, 0x20 };

        // Initialize a QR Code generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Configure the QR Code to use binary encoding.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Assign the byte array as the content of the QR Code.
            generator.SetCodeText(data);

            // Save the generated QR Code image as a PNG file.
            generator.Save("qr_binary.png");
        }

        // Inform the user that the QR Code has been saved.
        Console.WriteLine("QR Code saved as qr_binary.png");
    }
}