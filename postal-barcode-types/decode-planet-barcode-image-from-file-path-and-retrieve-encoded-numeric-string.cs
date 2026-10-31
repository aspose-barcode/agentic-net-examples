// Title: Decode Planet Barcode from Image File
// Description: Demonstrates generating a Planet barcode, saving it as a PNG file, and decoding it to retrieve the numeric string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include automated data capture, inventory management, and verification of encoded information. Developers often need to generate barcodes, store them as images, and later read them back using these core API classes.
// Prompt: Decode a Planet barcode image from a file path and retrieve the encoded numeric string.
// Tags: planet, barcode, decode, generation, recognition, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Planet barcode, saves it to a temporary file,
/// and then decodes it using Aspose.BarCode to retrieve the encoded numeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "PlanetDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "planet.png");

        // Generate a Planet barcode image with the specified numeric data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
        {
            // Set the X-dimension (module width) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image file not found.");
            return;
        }

        // Decode the Planet barcode from the saved image and output the decoded text
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Attempt to clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program outcome
        }
    }
}