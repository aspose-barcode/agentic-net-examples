// Title: Generate QR Code PNG with 300 DPI
// Description: Creates a QR code containing 'Hello World' and saves it as a PNG image at 300 DPI resolution.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr, barcode, generation, png, 300dpi, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code and saving it as a PNG file with 300 DPI resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the QR code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Define the data to encode in the QR code.
            generator.CodeText = "Hello World";

            // Configure the output image resolution (dots per inch).
            generator.Parameters.Resolution = 300;

            // Persist the generated QR code as a PNG file.
            generator.Save("qr.png");
        }
    }
}