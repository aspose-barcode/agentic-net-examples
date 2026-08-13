// Title: Generate Swiss Post Parcel barcode with custom margins
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode using Aspose.BarCode, applying a custom margin around the image, and saving it as PNG.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode. It showcases the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configuring barcode parameters such as padding and exception handling. Developers often need to generate postal barcodes for shipping labels and customize image layout, making this pattern useful for creating printable barcode graphics.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier and add a custom margin around the image.
// Tags: swisspostparcel, barcode, generation, padding, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// applies custom margins, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample identifier for Swiss Post Parcel domestic barcode
        const string codeText = "1234567890";

        // Output file path for the generated barcode image
        string outputPath = "SwissPostParcel.png";

        // Ensure the output directory exists before saving the file
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create the barcode generator with Swiss Post Parcel symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set custom margins (padding) around the barcode image (15 points on each side)
            generator.Parameters.Barcode.Padding.Left.Point = 15f;   // left margin
            generator.Parameters.Barcode.Padding.Top.Point = 15f;    // top margin
            generator.Parameters.Barcode.Padding.Right.Point = 15f;  // right margin
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f; // bottom margin

            // Optional: prevent exception if the code text is slightly incorrect
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the barcode image as PNG to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}