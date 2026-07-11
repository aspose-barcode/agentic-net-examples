// Title: Generate Swiss Post Parcel barcode with custom margins
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode using an original identifier and applying a custom margin around the generated image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Swiss Post symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and image formatting options such as padding and colors. Developers often need to generate postal barcodes for shipping labels and customize appearance for integration into documents or printing workflows.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier and add a custom margin around the image.
// Tags: swisspostparcel, barcode, generation, padding, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// applies custom margins, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel barcode identifier (original identifier format)
        const string codeText = "123456789012";

        // Output file path for the generated PNG image
        const string outputPath = "SwissPostParcel.png";

        // Initialize the barcode generator with Swiss Post Parcel symbology and the identifier
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure custom margins (padding) – 10 points on each side
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set background to white and bar color to black (default values)
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}