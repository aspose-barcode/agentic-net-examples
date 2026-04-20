using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to be encoded
            generator.CodeText = "1234567890";

            // Ensure the human‑readable text is displayed above the bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Apply a small vertical offset between the text and the bars
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2f; // 2 points offset

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}