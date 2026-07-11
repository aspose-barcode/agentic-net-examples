// Title: Decode Planet Barcode from Image
// Description: Demonstrates how to decode a Planet barcode stored in an image file and retrieve its numeric value.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader with DecodeType.Planet. It shows how to load an image, verify its existence, and extract encoded data—common tasks for developers integrating barcode scanning into document processing, inventory systems, or mobile apps.
// Prompt: Decode a Planet barcode image from a file path and retrieve the encoded numeric string.
// Tags: planet, barcode, decode, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console application that decodes Planet barcodes from image files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional image path argument,
    /// validates the file, and prints decoded Planet barcode values to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image path: use the first command‑line argument if supplied, otherwise default to "planet.png".
        string imagePath = args.Length > 0 ? args[0] : "planet.png";

        // Ensure the specified file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for the Planet symbology using the provided image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            // Optional: configure quality settings if higher accuracy is required.
            // reader.QualitySettings = QualitySettings.HighQuality;

            // Execute the barcode recognition process.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check whether any Planet barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No Planet barcode detected in the image.");
            }
            else
            {
                // Iterate through all detected barcodes and output their decoded text.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Decoded Planet barcode: {result.CodeText}");
                }
            }
        }
    }
}