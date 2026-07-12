// Title: Generate Swiss Post Parcel Domestic Barcode and Save as TIFF
// Description: Demonstrates creating an 18‑digit Swiss Post Parcel domestic barcode (starting with 98) and saving it as a TIFF image using Aspose.BarCode.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode, showcasing how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include generating shipping labels for Swiss Post parcels, where an 18‑digit code beginning with "98" is required. Developers often need to create barcodes and export them to various image formats for integration into logistics workflows.
// Prompt: Generate a Swiss Post Parcel domestic barcode using an 18‑digit code starting with 98 and output TIFF.
// Tags: swisspostparcel, barcode generation, tiff, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss Post Parcel domestic barcode and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the 18‑digit Swiss Post Parcel domestic code (must start with "98")
        string code = "981234567890123456";

        // Initialize the barcode generator with the Swiss Post Parcel symbology and the provided code
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
        {
            // Save the generated barcode image in TIFF format to the file system
            generator.Save("SwissPostParcel.tiff");
        }
    }
}