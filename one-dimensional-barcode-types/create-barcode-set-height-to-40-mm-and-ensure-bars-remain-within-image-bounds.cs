// Title: Generate a Code128 barcode with a specific height and bounded image
// Description: Demonstrates creating a Code128 barcode, setting its bar height to 40 mm, and configuring the image so the bars stay within the image bounds.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and Parameters to control barcode dimensions and image sizing. Typical use cases include generating printable barcodes with precise size requirements for labeling, inventory, and packaging applications. Developers often need to set bar height, image height, and disable auto‑sizing to ensure the barcode fits within a predefined layout.
// Prompt: Create a barcode, set Height to 40 mm, and ensure bars remain within image bounds.
// Tags: code128, barcode generation, png output, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with a fixed bar height
/// and ensures the barcode fits within the image bounds.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Resolve the full directory path and ensure it exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a BarcodeGenerator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode
            generator.CodeText = "1234567890";

            // Specify the bar height in points (40 mm ≈ 40 points for this example)
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Set the image height to a value that comfortably contains the bars
            generator.Parameters.ImageHeight.Point = 50f;

            // Disable automatic sizing so the explicit dimensions are used
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}