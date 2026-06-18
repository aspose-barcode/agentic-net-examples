using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to read barcodes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads an image, detects barcodes, and prints their details.
    /// </summary>
    static void Main()
    {
        // Path to the image that contains one or more barcodes.
        string imagePath = "barcode.png";

        // Verify that the specified file exists before attempting to load it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image into a Bitmap object (wrapped in a using statement for proper disposal).
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize a barcode reader that can decode all supported barcode types.
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Attempt to read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, inform the user and exit.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                    return;
                }

                // Iterate through each detected barcode and output its details.
                foreach (var result in results)
                {
                    // Retrieve the region rectangle (floating‑point coordinates) of the barcode.
                    var regionRect = result.Region.Rectangle;

                    // Convert the floating‑point rectangle to integer pixel values (rounded).
                    int x = (int)Math.Round((double)regionRect.X);
                    int y = (int)Math.Round((double)regionRect.Y);
                    int width = (int)Math.Round((double)regionRect.Width);
                    int height = (int)Math.Round((double)regionRect.Height);

                    // Display barcode type, decoded text, and its location within the image.
                    Console.WriteLine($"Barcode Type: {result.CodeType}");
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                    Console.WriteLine($"Region (pixels) - X:{x}, Y:{y}, Width:{width}, Height:{height}");
                    Console.WriteLine();
                }
            }
        }
    }
}