// Title: Set bottom caption padding for DataMatrix barcode
// Description: Demonstrates how to apply an 8‑pixel bottom padding to the caption of a DataMatrix barcode, creating visual separation between the text and the symbol.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on caption customization for 2‑D symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and the Parameters.CaptionBelow API to adjust padding, text, and other visual aspects. Developers often need to fine‑tune caption layout for branding, readability, or design compliance when embedding barcodes in documents or UI.
// Prompt: Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation from the symbol.
// Tags: datamatrix, caption, padding, barcode, generation, image, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a DataMatrix barcode and sets an 8‑pixel bottom padding for its caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode, configures caption padding, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // Set the bottom padding of the caption (below the barcode) to 8 pixels.
            // The Padding property uses Unit members; here we specify Pixels for the bottom side.
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8f;

            // Optionally assign caption text to visualize the padding effect.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";

            // Save the generated barcode image to a PNG file.
            generator.Save("DataMatrixWithBottomPadding.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated successfully.");
    }
}