// Title: Generate Swiss Post Parcel Domestic Barcode with Custom Margin
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode using an original identifier and saving it as a PNG image with a custom margin.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. It shows typical tasks such as setting module size, bar height, and padding to customize the appearance of the generated barcode. Developers working on shipping, logistics, or document automation often need to generate Swiss Post barcodes for parcel tracking and labeling.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier and add a custom margin around the image.
// Tags: swisspostparcel, barcode generation, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss Post Parcel domestic barcode,
/// applies custom padding, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a barcode with specified parameters and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomestic.png");

        // Original identifier for Swiss Post Parcel Domestic Mail.
        string codeText = "98.34.123456.12345678";

        // Initialize the barcode generator with the Swiss Post Parcel symbology and the identifier.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure visual appearance: module size (X dimension) and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Apply a custom margin (padding) of 10 pixels on all sides.
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Swiss Post Parcel domestic barcode saved to: {outputPath}");
    }
}