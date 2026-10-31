// Title: Generate Postnet barcodes and save as PNG images
// Description: This example creates Postnet barcodes for a collection of numeric strings and writes each barcode to a PNG file in a uniquely named temporary directory.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode for the Postnet symbology. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving with BarCodeImageFormat. Typical scenarios include generating postal barcodes from database values for shipping labels or bulk printing.
// Prompt: Generate a batch of Postnet barcodes from a database table column and save images to a specified folder.
// Tags: postnet, barcode, generation, png, aspose.barcode, batch, image, c#, encode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Postnet barcodes from a list of codes and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves them, and writes progress to the console.
    /// </summary>
    static void Main()
    {
        // Sample data that would normally come from a database column
        List<string> postnetCodes = new List<string>
        {
            "123456",
            "1159628792",
            "9876543210",
            "55555",
            "00123"
        };

        // Create a unique output folder in the system temporary directory
        string outputFolder = Path.Combine(Path.GetTempPath(), "PostnetBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Saving barcodes to: " + outputFolder);

        int index = 1;
        // Iterate over each code and generate a barcode image
        foreach (string code in postnetCodes)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(outputFolder, $"Postnet_{index}.png");

            // Initialize the barcode generator with Postnet symbology and the current code
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
            {
                // Configure typical Postnet parameters
                generator.Parameters.Barcode.XDimension.Pixels = 3f;          // Width of a single module
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;        // Overall bar height
                generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f; // Height of short bars

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {index}: {code}");
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }
}