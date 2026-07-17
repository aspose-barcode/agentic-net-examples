// Title: Generate QR Code with Binary Encoding from Byte Array
// Description: Demonstrates creating a QR Code barcode using binary encoding mode from a byte array and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows setting QR encoding mode to Binary, providing raw byte data, and exporting the result to a PNG file—common tasks for developers needing to embed binary data in QR codes for applications like device provisioning or secure data transfer.
// Prompt: Generate a QR Code barcode using binary encoding mode from a byte array and save as PNG.
// Tags: qr code,binary encoding,barcode generation,png output,aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode using binary encoding mode from a byte array
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a sample byte array to encode in binary mode.
        byte[] data = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };

        // Initialize the QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Configure the QR Code to use binary encoding mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Assign the byte array as the code text for the barcode.
            generator.SetCodeText(data);

            // Save the generated QR Code image as a PNG file.
            generator.Save("qr_binary.png");
        }
    }
}