// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring BarCodeReader to decode only 2D symbologies, skipping 1D types for faster processing.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode, 2d, decode, aspose, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code (if needed) and reads only 2D barcodes from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR code image (if missing) and uses BarCodeReader configured
    /// to decode only 2D symbologies, ignoring all 1D types for improved performance.
    /// </summary>
    static void Main()
    {
        // Path for the sample QR code image
        string imagePath = "sample_qr.png";

        // Generate a QR code image if it does not already exist
        if (!File.Exists(imagePath))
        {
            // Create a QR code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
            {
                // Save the generated QR code as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Create a BarCodeReader and configure it to process only 2D barcodes
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set the decode type to all 2D symbologies (ignores 1D types)
            reader.BarCodeReadType = DecodeType.Types2D;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type and decoded text of each 2D barcode
                Console.WriteLine($"Detected 2D Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}