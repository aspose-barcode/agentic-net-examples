// Title: Place Text Above DataMatrix Barcode
// Description: Demonstrates how to position human‑readable text above a DataMatrix symbol using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on text placement options. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control the location of the codetext. Developers often need to adjust text positioning for readability or branding when generating 2‑D barcodes for packaging, labeling, or documentation.
// Prompt: Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = TextLocation.Above.
// Tags: datamatrix, textlocation, above, barcode generation, aspose.barcodes, csharp

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates placing human‑readable text above a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode with the codetext positioned above the symbol and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with the desired codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello World"))
        {
            // Set the codetext location to appear above the barcode symbol.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Optional: adjust image dimensions if required.
            // generator.Parameters.ImageWidth.Point = 200f;
            // generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image to a PNG file.
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("DataMatrix barcode generated with text above the symbol.");
    }
}