// Title: Place Text Above DataMatrix Barcode
// Description: Demonstrates how to position human‑readable text above a DataMatrix symbol using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on customizing CodeTextParameters for various symbologies. It shows how to use the BarcodeGenerator class with EncodeTypes.DataMatrix, adjust text location, and apply auto‑size modes. Developers often need to control human‑readable text placement for better visual integration in reports, labels, and packaging.
// Prompt: Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = TextLocation.Above.
// Tags: datamatrix, text location, above, barcode generation, aspose.barcode, png, codetextparameters, autosizemode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DataMatrix barcode with the human‑readable text positioned above the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode, sets the text location to above the symbol, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        const string outputFile = "datamatrix_above.png";

        // Initialize a DataMatrix barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set the human‑readable text to appear above the DataMatrix symbol
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Enable interpolation auto‑size so the image dimensions adapt to the content
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode image to the specified file
            generator.Save(outputFile);
        }

        // Inform the user that the barcode image has been saved
        Console.WriteLine($"Barcode image saved to {outputFile}");
    }
}