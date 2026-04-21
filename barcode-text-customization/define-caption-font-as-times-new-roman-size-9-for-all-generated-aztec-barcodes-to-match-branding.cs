using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Aztec symbology with sample code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, "Sample"))
        {
            // Set caption font to Times New Roman, size 9 points
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 9f;
            // Make the caption visible and assign a sample caption text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Aztec Barcode";

            // Optional: set other visual properties (e.g., background and bar colors)
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode image to a PNG file
            generator.Save("AztecBarcode.png");
        }

        Console.WriteLine("Aztec barcode generated with caption font Times New Roman, size 9.");
    }
}