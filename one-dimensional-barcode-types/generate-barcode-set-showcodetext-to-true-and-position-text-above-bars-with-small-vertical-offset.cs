// Title: Generate Code128 barcode with text above bars
// Description: Demonstrates creating a Code128 barcode, showing the human‑readable text above the bars with a small vertical offset.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator, EncodeTypes, and CodeTextParameters. Typical use cases include adding readable text to barcodes for labeling products or documents, where developers need to control text location, spacing, and font. The snippet serves as a reference for developers searching for barcode text positioning techniques.
// Prompt: Generate a barcode, set ShowCodeText to true, and position text above bars with a small vertical offset.
// Tags: code128, barcode, generation, showcodetext, textposition, aspnet, aspose.barcode, imageoutput

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, displays the human‑readable text
/// above the bars, and applies a small vertical offset for better visual separation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures text appearance,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample value "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Ensure the human‑readable text is displayed (ShowCodeText is true by default when location is set)
            // Position the text above the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Apply a small vertical offset (2 points) between the text and the bars
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2f;

            // Optional: customize the font for better visibility
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the generated barcode image to a file
            generator.Save("barcode.png");
        }
    }
}