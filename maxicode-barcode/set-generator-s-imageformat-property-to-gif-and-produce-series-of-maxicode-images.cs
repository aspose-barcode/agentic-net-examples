// Title: Generate MaxiCode Barcodes as GIF Images
// Description: Demonstrates creating multiple MaxiCode barcodes and saving them as GIF files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.MaxiCode and BarCodeImageFormat to produce barcode images. Typical use cases include batch creation of shipping labels, inventory tags, or any scenario requiring MaxiCode symbols in a lightweight GIF format. Developers often need to automate image format selection and file naming for large sets of barcodes.
/// Prompt: Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images.
/// Tags: maxicode, barcode generation, gif, aspose.barcode, imageformat, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a series of MaxiCode barcodes and saves each as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output directory, iterates over sample messages,
    /// generates a MaxiCode barcode for each, and saves the result as a GIF file.
    /// </summary>
    static void Main()
    {
        // Define the directory where generated images will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeOutputs");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Sample messages to encode in the MaxiCode barcodes
        string[] messages = new string[]
        {
            "Sample Message 1",
            "Sample Message 2",
            "Sample Message 3",
            "Sample Message 4",
            "Sample Message 5"
        };

        // Loop through each message, generate a barcode, and save it as a GIF image
        for (int i = 0; i < messages.Length; i++)
        {
            // Build the full file path for the current image
            string filePath = Path.Combine(outputDir, $"maxicode_{i + 1}.gif");

            // Initialize the barcode generator with MaxiCode symbology and the current message
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, messages[i]))
            {
                // Save the generated barcode image in GIF format
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }
        }

        // Inform the user about the successful generation
        Console.WriteLine($"Generated {messages.Length} MaxiCode GIF images in: {outputDir}");
    }
}