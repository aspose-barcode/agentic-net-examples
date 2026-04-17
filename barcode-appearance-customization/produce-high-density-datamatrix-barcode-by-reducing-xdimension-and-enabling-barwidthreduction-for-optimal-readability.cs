using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample data to encode
        const string codeText = "HighDensityDataMatrix";

        // Create a DataMatrix barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Reduce XDimension to increase density (value in points)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Enable bar width reduction to compensate ink spread (value in points)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Optional: set a higher resolution for sharper output
            generator.Parameters.Resolution = 300;

            // Save the barcode image
            generator.Save("datamatrix_high_density.png");
        }

        Console.WriteLine("DataMatrix barcode generated: datamatrix_high_density.png");
    }
}