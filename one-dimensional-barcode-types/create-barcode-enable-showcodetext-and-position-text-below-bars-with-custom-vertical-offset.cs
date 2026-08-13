// Title: Generate Code128 barcode with custom text positioning
// Description: Demonstrates creating a Code128 barcode, enabling human-readable text, and positioning the text below the bars with a custom vertical offset.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure CodeTextParameters such as location and spacing. It uses BarcodeGenerator, EncodeTypes, and CodeLocation classes to customize human-readable text placement, a common requirement when integrating barcodes into printed labels, invoices, or product packaging.
// Prompt: Create a barcode, enable ShowCodeText, and position text below bars with custom vertical offset.
// Tags: barcode, code128, showcodetext, textposition, verticaloffset, aspnet, aspose.barcode, imageoutput

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with human‑readable text positioned below the bars and a custom vertical offset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the specified code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable human‑readable text and set its location to appear below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Define a custom vertical space (offset) between the bars and the text.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 8f; // adjust offset as needed

            // Save the generated barcode image as PNG.
            generator.Save("barcode.png");
        }
    }
}