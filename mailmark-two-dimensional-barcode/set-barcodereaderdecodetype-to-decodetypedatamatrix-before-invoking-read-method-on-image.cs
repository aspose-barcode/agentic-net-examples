// Title: Read DataMatrix Barcode from Generated Image
// Description: Generates a DataMatrix barcode, saves it as a PNG file, then reads and prints its content using Aspose.BarCode.
// Category-Description: This example demonstrates basic barcode generation and recognition with Aspose.BarCode. It showcases the use of BarcodeGenerator for creating a DataMatrix symbol and BarCodeReader for decoding it. Developers working on inventory, tracking, or any application that requires encoding and decoding DataMatrix symbology can refer to this pattern for quick implementation.
// Prompt: Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on the image.
// Tags: datamatrix, barcode, generation, recognition, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it as an image, and then reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated DataMatrix PNG image
        string imagePath = Path.Combine(tempFolder, "datamatrix.png");

        // Generate a DataMatrix barcode with the text "123456" and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the reader to decode only DataMatrix barcodes from the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes and output their type and text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // Attempt to delete the temporary image and folder; ignore any errors
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for this demo
        }
    }
}