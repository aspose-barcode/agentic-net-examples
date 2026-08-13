// Title: Diagnostic mode output of raw pixel matrix for barcode detection
// Description: Demonstrates generating a Code128 barcode, printing its grayscale pixel matrix, and recognizing the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related parameter classes. Typical use cases include debugging barcode rendering, validating image quality, and extracting raw pixel data for custom analysis. Developers often need to inspect the pixel matrix when troubleshooting detection issues.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode symbology, generation, recognition, diagnostic, raw pixel matrix, grayscale, aspose.barcode, code128

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, prints its raw grayscale pixel matrix,
/// and then performs barcode recognition on the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, outputs diagnostic pixel data,
    /// and reads back the barcode information.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: configure generation parameters for size and scaling
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Create the barcode image as a Bitmap
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Output raw pixel matrix (grayscale intensity) to console for diagnostic purposes
                Console.WriteLine("Raw pixel matrix (grayscale intensity):");
                for (int y = 0; y < barcodeImage.Height; y++)
                {
                    for (int x = 0; x < barcodeImage.Width; x++)
                    {
                        // Retrieve pixel color and compute average intensity
                        Color pixel = barcodeImage.GetPixel(x, y);
                        int intensity = (pixel.R + pixel.G + pixel.B) / 3;

                        // Print intensity value, padded for alignment
                        Console.Write(intensity.ToString().PadLeft(3));
                        if (x < barcodeImage.Width - 1) Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                // Perform barcode recognition on the generated image
                using (var reader = new BarCodeReader())
                {
                    // Assign the generated image to the reader
                    reader.SetBarCodeImage(barcodeImage);

                    // Use all supported barcode types for detection
                    reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                    // Iterate through detected barcodes and display results
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Detected Barcode:");
                        Console.WriteLine("  Type: " + result.CodeTypeName);
                        Console.WriteLine("  CodeText: " + result.CodeText);

                        // Output the detected region bounds
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"  Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                    }
                }
            }
        }
    }
}