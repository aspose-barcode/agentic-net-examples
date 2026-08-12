// Title: Generate QR Code and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with automatic version selection and exporting it to a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows typical steps such as setting the encoded text, configuring QR encoding mode, and saving the result in a common image format. Developers working on QR Code creation for web links, product IDs, or marketing materials can use this pattern as a starting point.
// Prompt: Generate a QR Code barcode with automatic version selection and export as JPEG.
// Tags: qr code, barcode generation, jpeg output, aspose.barcode, encode types, qrcode, automatic version

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
    /// Entry point that creates the QR Code and writes it to a file in the current directory.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_code.jpg");

        // Ensure the target directory exists before attempting to save the image
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for QR Code with default automatic version selection
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text (URL) that the QR Code will encode
            generator.CodeText = "https://example.com";

            // Optionally enforce automatic encoding mode (default behavior)
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Save the generated QR Code as a JPEG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}