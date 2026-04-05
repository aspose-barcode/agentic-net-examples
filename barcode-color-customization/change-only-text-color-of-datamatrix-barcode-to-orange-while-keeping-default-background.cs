using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace DataMatrixColorExample
{
    class Program
    {
        static void Main()
        {
            // Create a DataMatrix barcode generator with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
            {
                generator.CodeText = "Sample Text";

                // Set the code text (human readable) color to orange.
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

                // Save the barcode image; background remains default (white).
                generator.Save("datamatrix_orange_text.png");
            }
        }
    }
}