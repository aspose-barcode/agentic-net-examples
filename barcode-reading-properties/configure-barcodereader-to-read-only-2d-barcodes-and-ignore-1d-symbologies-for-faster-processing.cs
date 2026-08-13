// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring BarCodeReader to scan only 2D symbologies, ignoring 1D types for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to selectively decode barcodes using BarCodeReader. It highlights key API classes such as BarcodeGenerator for creating barcodes and BarCodeReader for recognition, a common requirement when developers need to improve performance by limiting the set of supported symbologies.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode symbology, read, console output, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code and reads it back using BarCodeReader
/// configured to recognize only 2D barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, verifies its creation, and reads it using a
    /// BarCodeReader limited to 2D symbologies.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "qr.png";

        // Generate a QR code (2D barcode) and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Configure BarCodeReader to recognize only 2D barcodes (ignore 1D symbologies)
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            // Iterate through all detected barcodes and output their type and text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text: {result.CodeText}");
            }
        }
    }
}