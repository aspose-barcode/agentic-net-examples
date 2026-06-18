using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read all supported barcodes from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans the specified image (or a default image) for barcodes and prints their type and text.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the image path.</param>
    static void Main(string[] args)
    {
        // Determine the image file path: use first command‑line argument or fallback to a default name.
        string imagePath = args.Length > 0 ? args[0] : "sample.jpg";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that scans the image for all supported symbologies.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition and retrieve all detected barcodes.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected in the image.");
                return;
            }

            // Output each detected barcode's type and decoded text.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Symbology : {result.CodeTypeName}");
                Console.WriteLine($"Code Text : {result.CodeText}");
                Console.WriteLine();
            }
        }
    }
}