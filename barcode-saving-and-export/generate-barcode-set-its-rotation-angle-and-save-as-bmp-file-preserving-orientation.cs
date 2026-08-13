// Title: Generate Rotated Code128 Barcode and Save as BMP
// Description: Demonstrates creating a Code128 barcode, applying a rotation angle, and saving it as a BMP image while preserving orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as rotation using the BarcodeGenerator class. Typical use cases include producing barcodes for printed labels that require specific orientation. Developers often need to set rotation, size, and format before saving the image with BarCodeImageFormat.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: code128, rotation, bmp, barcode, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a rotated Code128 barcode and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "rotated_barcode.bmp";

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the rotation angle to 90 degrees (float value required by the API).
            generator.Parameters.RotationAngle = 90f;

            // Save the barcode image as BMP, preserving the specified orientation.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}