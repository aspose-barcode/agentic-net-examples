// Title: Generate Swiss Post Parcel domestic barcode and save as PNG
// Description: Demonstrates creating a Swiss Post Parcel barcode from an identifier string and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.SwissPostParcel, configure visual parameters, save the image, and then read back the barcode using BarCodeReader. Developers working with postal barcode symbologies can use this pattern for generating and validating barcodes in shipping and logistics applications.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier string and save as PNG.
// Tags: barcode generation, swiss post parcel, png, aspose.barcode, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode, saving it as PNG,
/// and reading it back to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and reads it back.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Original identifier string for the Swiss Post Parcel barcode
        string identifier = "98.34.123456.12345678";

        // Build the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomesticMail.png");

        // Initialize the barcode generator with SwissPostParcel symbology and the identifier
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifier))
        {
            // Set visual parameters: X dimension and bar height in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode image to a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Generate a bitmap of the barcode for subsequent reading
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for SwissPostParcel symbology
                using (var reader = new BarCodeReader(bitmap, DecodeType.SwissPostParcel))
                {
                    // Iterate through all detected barcodes and output their type and data
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Barcode type:{result.CodeTypeName}, Barcode Data:{result.CodeText}");
                    }
                }
            }
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}