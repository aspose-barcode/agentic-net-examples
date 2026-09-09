// Title: Generate Swiss Post Parcel barcode with custom margins
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode using the original identifier and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as X‑dimension, bar height, and custom padding. It uses the BarcodeGenerator class to produce a Swiss Post Parcel barcode, a common requirement for logistics and mailing applications. Developers often need to adjust visual appearance and export formats for integration into shipping labels and documents.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier and add a custom margin around the image.
// Tags: swisspostparcel, barcode generation, custom margin, png, aspose.barcode, xdimension, barheight

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode with custom margins and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies visual settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostDomestic.png");

        // Initialize the barcode generator with Swiss Post Parcel symbology and the original identifier
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "98.34.123456.12345678"))
        {
            // Set the X dimension (module width) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Set the bar height to 40 pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Apply a custom margin of 10 pixels on all sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}