using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567"))
        {
            // Ensure the human‑readable text is shown below the bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set a custom vertical offset (space) between the barcode and the text, in points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 10f;

            // Optional: align the text to the center
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}