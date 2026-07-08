// Title: Generate Code128 barcode with custom text positioning
// Description: Demonstrates creating a Code128 barcode, enabling the human‑readable text, and moving the text below the bars with a custom vertical offset.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It covers setting CodeTextParameters such as Location, Space, and Font to control the display of human‑readable text. Developers often need these settings when integrating barcodes into documents, labels, or UI elements where precise text placement is required.
// Prompt: Create a barcode, enable ShowCodeText, and position text below bars with custom vertical offset.
// Tags: code128, showcodetext, verticaloffset, png, barcodelibrary, generation, aspnet, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing; // For color handling if needed

/// <summary>
/// Example program that generates a Code128 barcode, shows the human‑readable text,
/// and positions the text below the bars with a custom vertical offset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures text display,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Ensure the human‑readable text is displayed below the bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set a custom vertical offset (10 points) between the bars and the text.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 10f;

            // Optionally adjust the font size of the displayed text (12 points).
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }
    }
}