// Title: Generate Postnet barcodes from a list and save as PNG files
// Description: Demonstrates how to create Postnet barcodes using Aspose.BarCode and store each image in a designated folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Postnet. It shows typical parameter settings, file output handling, and batch processing of multiple codes—common tasks for developers needing to produce postal barcodes for mailing applications.
// Prompt: Generate a batch of Postnet barcodes from a database table column and save images to a specified folder.
// Tags: postnet, barcode generation, batch processing, png, aspose.barcode, encode types, file output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a batch of Postnet barcodes and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates Postnet barcodes for a set of codes and writes them to an output folder.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the output folder path.</param>
    static void Main(string[] args)
    {
        // Determine the output folder: use the first argument if provided; otherwise create a temporary folder.
        string outputFolder;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            outputFolder = args[0];
        }
        else
        {
            outputFolder = Path.Combine(Path.GetTempPath(), "PostnetBarcodes_" + Guid.NewGuid().ToString("N"));
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Simulated database column values (Postnet code texts).
        List<string> postnetCodes = new List<string>
        {
            "123456",
            "1159628792",
            "9876543210",
            "0012345678",
            "5555555555"
        };

        // Iterate over each code, generate a barcode, and save it as a PNG file.
        foreach (string code in postnetCodes)
        {
            string filePath = Path.Combine(outputFolder, $"Postnet_{code}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
            {
                // Configure typical Postnet barcode parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;          // Width of the smallest bar.
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;        // Overall bar height.
                generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f; // Height of short bars.

                // Save the generated barcode image to the specified file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Log the location of the generated barcode.
            Console.WriteLine($"Generated Postnet barcode for '{code}' at: {filePath}");
        }

        // Indicate that the batch process has finished.
        Console.WriteLine("Batch generation completed.");
    }
}