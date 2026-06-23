using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a rotated Code128 barcode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates a Code128 barcode with a rotation of 90 degrees and writes it to a BMP file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.bmp";

        // Initialize a BarcodeGenerator for Code128 format with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply a 90-degree rotation to the barcode.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode to the specified path in BMP format.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}