// Title: Generate Swiss Post Parcel barcode with checksum correction and save as JPEG
// Description: Demonstrates creating a Swiss Post Parcel (international) barcode, letting the library auto‑correct an invalid checksum, and exporting the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.SwissPostParcel. Typical use cases include preparing postal barcodes for shipping labels where checksum validation is required. Developers often need to configure dimensions, bar height, and output formats using the Parameters API and save the image with BarCodeImageFormat.
// Prompt: Generate a Swiss Post Parcel international barcode with automatic checksum correction and save as JPEG.
// Tags: swisspost, parcel, barcode, generation, checksum, jpeg, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode with automatic checksum correction and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output folder path relative to current directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Barcode text with an intentionally wrong checksum; Aspose.BarCode will correct it automatically
        string codeText = "RM999605017CH";
        // Full file path for the resulting JPEG image
        string filePath = Path.Combine(outputDir, "SwissPostInternationalMail.jpg");

        // Initialize the barcode generator for Swiss Post Parcel symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Set bar height in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            // Save the generated barcode as a JPEG image
            generator.Save(filePath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}