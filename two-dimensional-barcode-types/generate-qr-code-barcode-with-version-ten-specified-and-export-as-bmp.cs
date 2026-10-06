// Title: Generate QR Code with Version 10 and Save as BMP
// Description: Demonstrates how to create a QR Code barcode with a specific version (10) using Aspose.BarCode and export it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QR version settings. Developers commonly generate QR codes for product labeling, marketing, or data sharing, needing control over version size and image format. The snippet shows typical steps: configure parameters, set QR version, adjust module size, and save to BMP.
// Prompt: Generate a QR Code barcode with version ten specified and export as BMP.
// Tags: qr, barcode, generation, bmp, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code with version 10 and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_version10.bmp");

        // Initialize the barcode generator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to 10 (controls the data capacity and matrix size).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Optional: define the size of each module (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the BMP file has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}