using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a DataMatrix barcode generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HelloWorld"))
            {
                // Place the human‑readable text above the symbol
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

                // Optionally set image dimensions
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 200f;

                // Save the barcode image to a file
                generator.Save("datamatrix_above.png");
            }
        }
    }
}