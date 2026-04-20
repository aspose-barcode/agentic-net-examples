using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align the human‑readable text to the right side of the image
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the barcode image to a file
            generator.Save("right_aligned_barcode.png");
        }

        Console.WriteLine("Barcode generated with right‑aligned text.");
    }
}