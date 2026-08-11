// Title: Generate Swiss Post Parcel Barcode and Save as PNG
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode from an identifier string and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce parcel barcodes. Typical use cases include preparing shipping labels for Swiss Post services, where developers need to encode parcel identifiers into machine‑readable barcodes. The snippet shows directory handling, barcode creation, and image export, common tasks for logistics and e‑commerce applications.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier string and save as PNG.
// Tags: barcode, swisspostparcel, generation, png, barcodegenerator, encode-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode using a sample identifier and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Sample identifier for Swiss Post Parcel domestic barcode
        string identifier = "123456789012";

        // Output file path (PNG format)
        string outputPath = "SwissPostParcel.png";

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a barcode generator for Swiss Post Parcel using the identifier
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifier))
        {
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}