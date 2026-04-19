using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set background to transparent for overlay usage
            generator.Parameters.BackColor = Color.Transparent;

            // Optional: set barcode foreground color (default is black)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as PNG with transparent background
            generator.Save("datamatrix.png");
        }
    }
}