// Title: Decode Planet Barcode from Image File
// Description: Demonstrates generating a Planet barcode, saving it as a PNG image, and decoding it to retrieve the encoded numeric string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding). Typical scenarios include creating barcode images for inventory, shipping, or labeling and later extracting the data programmatically. Developers often need to generate barcodes, store them as image files, and later read them back in batch processing or validation workflows.
// Prompt: Decode a Planet barcode image from a file path and retrieve the encoded numeric string.
// Tags: planet, barcode, decode, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Planet barcode image, saves it, and then decodes it to extract the encoded numeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Planet barcode, writes it to a temporary PNG file, reads the file back,
    /// decodes the barcode, and outputs the decoded text to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "PlanetDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "planet.png");

        // Generate a Planet barcode image with sample numeric data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
        {
            // Set the X-dimension (module width) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Decode the Planet barcode from the saved image file
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            bool anyFound = false;
            // Iterate through all detected barcodes (should be only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }

            // Inform the user if no barcode was detected
            if (!anyFound)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}