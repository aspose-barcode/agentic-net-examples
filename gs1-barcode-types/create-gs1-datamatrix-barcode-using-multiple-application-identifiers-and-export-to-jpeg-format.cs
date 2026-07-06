// Title: Generate GS1 DataMatrix Barcode with Multiple Application Identifiers and Save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode that encodes several Application Identifiers (GTIN, batch number, expiration date) and exporting the image to JPEG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. Developers often need to embed GS1 data in DataMatrix symbols for supply‑chain labeling, requiring multiple Application Identifiers. The snippet shows typical steps: define GS1‑formatted text, instantiate the generator, and save the result in a common image format.
// Prompt: Create a GS1 DataMatrix barcode using multiple Application Identifiers and export to a JPEG format.
// Tags: gs1, datamatrix, barcode, generation, jpeg, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode containing several Application Identifiers
/// and saves the generated image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define GS1 DataMatrix code text with multiple Application Identifiers (AIs):
        // (01) – GTIN, (10) – Batch/Lot number, (17) – Expiration date (YYMMDD)
        string codeText = "(01)12345678901231(10)ABC123(17)240731";

        // Specify the output file path for the JPEG image
        string outputPath = "gs1_datamatrix.jpg";

        // Create the barcode generator for GS1 DataMatrix using the defined code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Save the generated barcode image as a JPEG file
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}