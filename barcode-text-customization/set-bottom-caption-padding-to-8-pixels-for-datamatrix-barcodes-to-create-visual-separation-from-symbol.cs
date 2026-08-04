// Title: Set bottom caption padding for DataMatrix barcode
// Description: Demonstrates how to set an 8‑pixel bottom padding for the caption of a DataMatrix barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows configuring caption text and padding, a common requirement when developers need visual separation between the barcode symbol and its descriptive text. Typical use cases include creating printable labels or UI elements where caption spacing improves readability.
// Prompt: Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation from the symbol.
// Tags: datamatrix, caption, padding, barcode generation, aspnet, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting bottom caption padding for a DataMatrix barcode and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode with a caption and custom bottom padding, then saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file name
        string outputPath = "datamatrix.png";

        // Initialize a DataMatrix barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
        {
            // Configure the caption that appears below the barcode symbol
            generator.Parameters.CaptionBelow.Text = "DataMatrix";

            // Apply an 8‑pixel bottom padding to separate the caption from the symbol
            generator.Parameters.CaptionBelow.Padding.Bottom.Point = 8f;

            // Render and save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved image for user reference
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}