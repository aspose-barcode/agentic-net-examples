// Title: Generate QR Code with Medium Error Correction Level
// Description: Demonstrates creating a QR Code barcode, setting its error correction level to medium, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce QR Code symbologies. It covers configuring QR-specific parameters such as error correction level, a common requirement for applications needing balanced robustness against damage while maintaining reasonable data capacity. Developers often reference this pattern when integrating QR Code creation into reporting, labeling, or mobile scanning solutions.
// Prompt: Generate QR Code barcode and set error correction level to medium for balanced robustness.
// Tags: qr code, error correction, barcode generation, png output, aspose.barcode, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating a QR Code barcode with medium error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that prepares the output directory, triggers QR Code generation, and reports the result.
    /// </summary>
    static void Main()
    {
        // Create a dedicated folder in the system's temporary directory
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the resulting PNG file
        string outputFile = Path.Combine(outputFolder, "qr_medium.png");

        // Generate the QR code image using the helper method
        GenerateQrCode("Hello, World!", outputFile);

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to: {outputFile}");
    }

    /// <summary>
    /// Generates a QR Code barcode with the specified text and saves it to the given file path.
    /// </summary>
    /// <param name="text">The data to encode in the QR Code.</param>
    /// <param name="filePath">The full file path where the PNG image will be saved.</param>
    static void GenerateQrCode(string text, string filePath)
    {
        // Initialize the barcode generator for QR code with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Configure the QR code to use medium error correction (LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}