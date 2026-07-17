// Title: Generate QR Code and Save as PNG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it as a PNG file, suitable for inclusion in CI pipelines.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the BarcodeGenerator class with EncodeTypes.QR, configuring error correction, and exporting to image formats. Developers often use these APIs to automate barcode generation for testing, documentation, or CI/CD workflows.
// Prompt: Generate QR Code barcode and integrate generation into CI pipeline for automated testing.
// Tags: qr code, barcode generation, png, aspose.barcode, ci integration, automation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG image.
/// Designed for use in automated CI pipelines where interactive console input is unavailable.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Returns an exit code indicating success (0) or failure (non‑zero).
    /// </summary>
    /// <returns>Integer exit code.</returns>
    static int Main()
    {
        // Define the output directory relative to the current working directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists; create it if necessary.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Full path for the generated QR Code image.
        string outputFile = Path.Combine(outputFolder, "qr.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure a high error correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Suppress exceptions for minor code‑text issues to keep CI builds robust.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputFile);
        }

        // Log the location of the generated file for CI visibility.
        Console.WriteLine($"QR code generated at: {outputFile}");

        // Return success exit code.
        return 0;
    }
}