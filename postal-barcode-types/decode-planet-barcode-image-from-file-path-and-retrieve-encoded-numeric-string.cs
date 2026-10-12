// Title: Decode Planet Barcode from Image File
// Description: Demonstrates decoding a Planet barcode saved as an image and retrieving its numeric value.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a Planet symbology image and BarCodeReader to decode it. Developers working with barcode automation often need to generate barcodes for testing or labeling and then read them back to verify data integrity or extract information in batch processing scenarios.
// Prompt: Decode a Planet barcode image from a file path and retrieve the encoded numeric string.
// Tags: planet, barcode, decode, image, aspose.barcode, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a Planet barcode, saves it to a temporary file,
/// then decodes it to retrieve the original numeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it, and prints the result.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "PlanetDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "planet.png");

        // Generate a Planet barcode image with sample numeric data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, "123456789"))
        {
            // Set the X-dimension (module width) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to decode
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Decode the Planet barcode and output the encoded numeric string
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            bool anyFound = false;
            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Optional cleanup: uncomment to delete the temporary folder after execution
        // Directory.Delete(tempFolder, true);
    }
}