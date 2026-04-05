using System;
using Aspose.BarCode.Generation;

namespace HighDensityDataMatrix
{
    class Program
    {
        static void Main()
        {
            // Create a DataMatrix barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
            {
                // Text to encode
                generator.CodeText = "HighDensity";

                // Reduce the XDimension for higher density (points)
                generator.Parameters.Barcode.XDimension.Point = 0.5f;

                // Enable bar width reduction to compensate ink spread (points)
                generator.Parameters.Barcode.BarWidthReduction.Point = 0.2f;

                // Save the barcode image
                generator.Save("datamatrix.png");
            }

            Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
        }
    }
}