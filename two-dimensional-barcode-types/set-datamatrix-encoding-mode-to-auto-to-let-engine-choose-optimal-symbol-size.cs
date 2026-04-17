using System;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Create a DataMatrix barcode generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello World"))
            {
                // Set encoding mode to Auto (engine chooses optimal symbol size)
                generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

                // Save the generated barcode image
                generator.Save("datamatrix.png");
            }
        }
    }
}