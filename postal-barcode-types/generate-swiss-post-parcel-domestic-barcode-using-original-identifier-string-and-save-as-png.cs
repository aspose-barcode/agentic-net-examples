// Title: Generate Swiss Post Parcel barcode and save as PNG
// Description: Demonstrates creating a Swiss Post Parcel domestic barcode from an original identifier string and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include generating shipping labels for Swiss Post parcels. Developers often need to create barcodes from raw identifier data and export them to common image formats like PNG.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier string and save as PNG.
// Tags: swisspostparcel, barcode generation, png output, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode from an identifier string
/// and saves the result as a PNG image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the original identifier string for the Swiss Post Parcel barcode.
        string identifier = "1234567890123456";

        // Initialize the barcode generator with the Swiss Post Parcel symbology and the identifier.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifier))
        {
            // Save the generated barcode image to a PNG file.
            generator.Save("SwissPostParcel.png");
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine("Swiss Post Parcel barcode saved as SwissPostParcel.png");
    }
}