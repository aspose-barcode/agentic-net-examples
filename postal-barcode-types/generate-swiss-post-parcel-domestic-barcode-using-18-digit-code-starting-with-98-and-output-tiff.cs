using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generation of a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an 18‑digit Swiss Post Parcel barcode and saves it as a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the 18‑digit Swiss Post Parcel domestic code (must start with "98")
        string codeText = "981234567890123456";

        // Build the full output file path in the current working directory, using TIFF extension
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcel.tiff");

        // Create a BarcodeGenerator for the SwissPostParcel symbology with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Save the generated barcode image to the specified path in TIFF format
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}