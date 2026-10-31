// Title: Generate QR Code and Save as PNG with Overwrite
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it directly to a file path, overwriting any existing file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include generating QR codes for URLs, contact information, or product data and saving them in common image formats such as PNG. Developers often need to control image dimensions, output format, and handle file overwriting when automating barcode creation in batch processes.
// Prompt: Generate a QR Code barcode and save directly to file system path with overwrite enabled.
// Tags: qr code, barcode generation, png, overwrite, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a PNG file, overwriting any existing file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Remove the file if it already exists to enable overwriting
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Initialize the barcode generator for QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code Text"))
        {
            // Set the size of each QR module in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}