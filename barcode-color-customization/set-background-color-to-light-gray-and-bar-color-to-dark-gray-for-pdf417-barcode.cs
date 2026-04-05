using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            // Set the text to encode
            generator.CodeText = "Sample PDF417 Barcode";

            // Set background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Set bar (foreground) color to dark gray
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the barcode image as PNG
            generator.Save("pdf417.png");
        }
    }
}