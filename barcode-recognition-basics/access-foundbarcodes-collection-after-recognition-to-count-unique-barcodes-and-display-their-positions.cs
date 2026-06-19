using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, verifying its creation,
/// and reading back barcodes from the image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a file, then reads and displays barcode information.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image.
        string imagePath = "sample.png";

        // -------------------------------------------------
        // Generate a sample barcode image (Code128, value "ABC123")
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Save the generated barcode to the specified path.
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify that the barcode image was successfully created.
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read all supported barcodes from the generated image.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Retrieve an array of barcode results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine the set of unique barcode texts.
            var uniqueCodes = new HashSet<string>();
            foreach (var result in results)
            {
                uniqueCodes.Add(result.CodeText);
            }

            // Output summary information.
            Console.WriteLine($"Total barcodes detected: {results.Length}");
            Console.WriteLine($"Unique barcodes count: {uniqueCodes.Count}");

            // -------------------------------------------------
            // Display detailed information for each detected barcode.
            // -------------------------------------------------
            foreach (var result in results)
            {
                // Extract the bounding rectangle of the barcode region.
                var rect = result.Region.Rectangle;
                int x = (int)Math.Round((double)rect.X);
                int y = (int)Math.Round((double)rect.Y);
                int width = (int)Math.Round((double)rect.Width);
                int height = (int)Math.Round((double)rect.Height);
                double angle = result.Region.Angle; // Rotation angle of the barcode.

                // Print barcode text and its position/size.
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Position: X={x}, Y={y}, Width={width}, Height={height}, Angle={angle}");
                Console.WriteLine();
            }
        }
    }
}