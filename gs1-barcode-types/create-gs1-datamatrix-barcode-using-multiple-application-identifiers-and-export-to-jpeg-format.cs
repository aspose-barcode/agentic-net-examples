// Title: Generate a GS1 DataMatrix barcode with multiple Application Identifiers and save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode that encodes several GS1 Application Identifiers, then exporting the image to JPEG format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 symbologies and image output. It showcases the use of EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes to produce GS1 DataMatrix codes, a common requirement for supply‑chain labeling and product identification. Developers often need to embed multiple AI values and export the result in standard image formats for printing or digital workflows.
// Prompt: Create a GS1 DataMatrix barcode using multiple Application Identifiers and export to a JPEG format.
// Tags: datamatrix, gs1, barcode, generation, jpeg, aspose.barcode, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode containing multiple Application Identifiers
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.jpg");

        // Resolve the GS1 DataMatrix symbology using reflection because the enum value is not directly exposed.
        const string symbologyName = "GS1DataMatrix";
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            // If the symbology cannot be found, inform the user and exit.
            Console.WriteLine($"Symbology '{symbologyName}' not found.");
            return;
        }
        // Cast the reflected value to BaseEncodeType for use with BarcodeGenerator.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // GS1 code text containing multiple Application Identifiers:
        // (01) – GTIN, (21) – Serial number, (30) – Variable count.
        string codeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Create the barcode generator with the resolved symbology and code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the module (pixel) size of the DataMatrix.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved image.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputFile}");
    }
}