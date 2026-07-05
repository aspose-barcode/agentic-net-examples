// Title: Rotated Code128 Barcode Generation and BMP Export
// Description: Demonstrates creating a Code128 barcode, rotating it 90 degrees, and saving the image as a BMP while preserving orientation.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: barcode, code128, rotation, bmp, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Example program that generates a rotated Code128 barcode and saves it as a BMP file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates a barcode, applies rotation, and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the rotated barcode image.
            string outputPath = "rotated_barcode.bmp";

            // Initialize a barcode generator for Code128 with the sample text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set the rotation angle to 90 degrees (float value required by the API).
                generator.Parameters.RotationAngle = 90f;

                // Optional: increase the resolution to 300 DPI for higher image quality.
                generator.Parameters.Resolution = 300;

                // Save the barcode as a BMP file; the format is inferred from the file extension.
                generator.Save(outputPath);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}