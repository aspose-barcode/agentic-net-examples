// Title: Generate Swiss Post Parcel International Barcode and Save as JPEG
// Description: Demonstrates creating a Swiss Post Parcel international barcode with automatic checksum correction and exporting it to a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include creating shipping labels for Swiss Post parcels, where the barcode must conform to international standards and include a valid checksum. Developers often need to adjust visual parameters such as X‑dimension and bar height before saving the barcode in common image formats.
// Prompt: Generate a Swiss Post Parcel international barcode with automatic checksum correction and save as JPEG.
// Tags: barcode, swisspost, parcel, international, checksum, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss Post Parcel international barcode,
/// lets the library correct an invalid checksum automatically,
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "SwissPostInternationalMail.jpg");

        // Barcode text with an intentionally incorrect checksum; the library will correct it.
        string codeText = "RM999605017CH";

        // Create a BarcodeGenerator for the Swiss Post Parcel symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set visual appearance: X‑dimension (module width) and bar height in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Swiss Post International barcode saved to: {outputPath}");
    }
}