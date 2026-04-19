using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Change only the human‑readable text color to orange.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

            // Save the barcode image (default background is retained).
            generator.Save("datamatrix_orange_text.png");
        }
    }
}