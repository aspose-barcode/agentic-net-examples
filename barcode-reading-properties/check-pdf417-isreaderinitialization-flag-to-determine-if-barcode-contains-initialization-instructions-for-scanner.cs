// Title: PDF417 Reader Initialization Flag Demo
// Description: Demonstrates how to set and read the IsReaderInitialization flag on a PDF417 barcode, which tells a scanner that the barcode contains initialization instructions.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, barcode, initialization, reader, generation, recognition, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a PDF417 barcode with the IsReaderInitialization flag set,
/// saves it as an image, and then reads the flag back from the generated barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output image file path.
        string imagePath = "pdf417.png";

        // Remove any existing file to ensure a fresh generation.
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // Generate a PDF417 barcode and enable the reader‑initialization flag.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Instruct the scanner that this barcode contains initialization instructions.
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode from the saved image and inspect the IsReaderInitialization flag.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Retrieve the flag from the extended PDF417 parameters.
                bool isReaderInit = result.Extended.Pdf417.IsReaderInitialization;

                // Output the detection results.
                Console.WriteLine($"Detected PDF417 barcode:");
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  IsReaderInitialization: {isReaderInit}");
            }
        }
    }
}