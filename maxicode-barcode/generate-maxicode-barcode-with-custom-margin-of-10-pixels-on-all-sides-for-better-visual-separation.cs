// Title: Generate MaxiCode barcode with custom margins
// Description: Creates a MaxiCode barcode (Mode 4) and applies a 10‑pixel margin on all sides before saving as PNG.
// Category-Description: This example demonstrates how to use Aspose.BarCode's ComplexBarcodeGenerator to produce two‑dimensional barcodes with custom layout settings. It focuses on the MaxiCode symbology, showing how to configure padding via the Parameters.Barcode.Padding properties. Developers working with shipping labels, logistics, or any application requiring MaxiCode can reuse this pattern to adjust visual spacing and output format.
// Prompt: Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visual separation.
// Tags: maxicode, barcode, margin, padding, png, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode with a uniform 10‑pixel margin using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode (Mode 4) barcode, sets padding, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the MaxiCode content: Mode 4 with a simple message.
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Initialize the ComplexBarcodeGenerator with the defined codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Apply a 10‑pixel margin on all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the resulting barcode image to a PNG file.
            generator.Save("maxicode.png");
        }
    }
}