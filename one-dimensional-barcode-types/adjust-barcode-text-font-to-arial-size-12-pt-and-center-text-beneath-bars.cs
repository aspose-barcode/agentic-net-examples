// Title: Generate Code128 barcode with centered Arial text
// Description: Demonstrates creating a Code128 barcode, placing human‑readable text below the bars, centering it, and applying an Arial‑like 12 pt font.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and CodeTextParameters to produce barcodes with customized human‑readable text. Typical use cases include product labeling, inventory tracking, and shipping documents where readable text must accompany the barcode. Developers often need to adjust text position, alignment, and font styling to meet branding or regulatory requirements.
// Prompt: Adjust barcode text font to Arial, size 12 pt, and center the text beneath the bars.
// Tags: code128, barcode generation, text formatting, png output, aspose.barcode, font settings

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with centered, Arial‑style text placed below the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that configures barcode parameters and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample value "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Position the human‑readable text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Align the text to the center of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Set the font to Helvetica (Arial equivalent) with a size of 12 points
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }
    }
}