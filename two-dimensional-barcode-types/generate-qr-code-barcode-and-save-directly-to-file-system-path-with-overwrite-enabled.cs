// Title: Generate QR Code and save to file with overwrite
// Description: This example creates a QR Code barcode, sets its content and error correction level, and saves it as a PNG file, overwriting any existing file.
// Category-Description: Demonstrates basic barcode generation using Aspose.BarCode. The example utilizes the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images, a common requirement for embedding URLs, contact data, or product information in mobile-friendly formats. Developers often need to configure error correction, choose output formats, and manage file overwrites when automating barcode creation in batch processes.
// Prompt: Generate a QR Code barcode and save directly to file system path with overwrite enabled.
// Tags: qr code, barcode generation, png, overwrite, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a QR Code barcode and save it to a file,
/// ensuring any existing file at the target path is overwritten.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, configures its properties,
    /// and writes the image to the specified file path.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path for the QR Code image.
        string outputPath = "qr.png";

        // Remove the file if it already exists to allow overwriting.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Initialize the QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR Code.
            generator.CodeText = "Hello, World!";

            // Configure the error correction level (Level M provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}