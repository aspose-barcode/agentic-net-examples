// Title: Generate GS1 DataMatrix barcode with multiple Application Identifiers and save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode that encodes several Application Identifiers and exporting the image to JPEG format.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It illustrates typical use cases such as encoding GS1 Application Identifiers, customizing barcode appearance, and saving the result in common image formats. Developers working with product identification, inventory tracking, or logistics often need to generate GS1 DataMatrix symbols programmatically.
// Prompt: Create a GS1 DataMatrix barcode using multiple Application Identifiers and export to a JPEG format.
// Tags: gs1 datamatrix, barcode generation, jpeg, aspose.barcodes, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with multiple Application Identifiers and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures appearance, and writes the JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated JPEG image.
        string outputPath = "gs1_datamatrix.jpg";

        // GS1 DataMatrix code text containing multiple Application Identifiers:
        // (01) – GTIN-14, (10) – Batch/Lot, (21) – Serial Number.
        string codeText = "(01)01234567890128(10)BATCH123(21)SN001";

        // Initialize the barcode generator for GS1 DataMatrix with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Adjust the module (X) dimension to control barcode size (optional).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the barcode foreground (bars) and background colors (optional).
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to the specified path in JPEG format.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to {outputPath}");
    }
}