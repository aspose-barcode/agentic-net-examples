// Title: Generate QR Code and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with automatic version selection and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to encode data into a QR Code, configure basic parameters such as X‑dimension, and save the result in a common image format. Developers working with barcode creation often need to generate QR codes for URLs or other data and export them for web or print use; this snippet shows the typical workflow using Aspose.BarCode.
// Prompt: Generate a QR Code barcode with automatic version selection and export as JPEG.
// Tags: qr code, barcode generation, jpeg output, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_code.jpg");

        // Initialize the barcode generator with QR type and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the X dimension (pixel size) of the QR modules; automatic version selection is handled internally.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}