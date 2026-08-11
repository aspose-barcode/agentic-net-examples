// Title: Generate Swiss Post Parcel Domestic Barcode and Save as TIFF
// Description: Demonstrates how to create a Swiss Post Parcel domestic barcode from an 18‑digit code and save it as a TIFF image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with the EncodeTypes.SwissPostParcel symbology to produce printable barcodes. Typical use cases include generating shipping labels for Swiss Post parcels, where an 18‑digit numeric code starting with '98' is required. Developers often need to configure generator parameters, handle validation, and export the barcode to common image formats such as TIFF.
// Prompt: Generate a Swiss Post Parcel domestic barcode using an 18‑digit code starting with 98 and output TIFF.
// Tags: barcode generation, swiss post parcel, tiff, aspose.barcode, encode types, image export

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Swiss Post Parcel domestic barcode and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define an 18‑digit code for Swiss Post Parcel (must start with "98")
        string codeText = "981234567890123456";

        // Initialize the barcode generator with Swiss Post Parcel symbology and the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Allow the generator to proceed even if the code text is slightly off the strict format
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Specify the output file path and save the barcode as a TIFF image
            string outputPath = "SwissPostParcel.tiff";
            generator.Save(outputPath, BarCodeImageFormat.Tiff);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}