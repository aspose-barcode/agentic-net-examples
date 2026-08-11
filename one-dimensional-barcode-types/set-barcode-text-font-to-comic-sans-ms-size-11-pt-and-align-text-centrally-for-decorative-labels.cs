// Title: Generate a Code128 barcode with custom Comic Sans text for decorative labels
// Description: Demonstrates how to set the human‑readable text font to Comic Sans MS, size 11 pt, and center‑align it when generating a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize text appearance. Typical use cases include creating branded or decorative labels where font style and alignment are important. Developers often need to adjust font family, size, and alignment to match design guidelines.
// Prompt: Set barcode text font to Comic Sans MS, size 11 pt, and align text centrally for decorative labels.
// Tags: code128, barcode generation, text formatting, font customization, image output, aspose.barcode, aspose.drawing

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of generating a Code128 barcode with custom text formatting.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point that creates the barcode image with Comic Sans font, 11 pt size, centered text, and saves it as PNG.
    /// </summary>
    public static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DecorativeLabel"))
        {
            // Set the human‑readable text font to Comic Sans MS, 11 pt.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the human‑readable text centrally beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Generate the barcode image (returned as Aspose.Drawing.Bitmap).
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the generated barcode image to a PNG file.
                generator.Save("decorative_label.png");
            }
        }
    }
}