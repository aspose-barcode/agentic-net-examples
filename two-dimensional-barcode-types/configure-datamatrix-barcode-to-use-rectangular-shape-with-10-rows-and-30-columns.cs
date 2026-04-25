using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample text to encode
        const string codeText = "Rectangular DataMatrix";

        // Create a DataMatrix barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // The exact 10x30 rectangular size is not available in the DataMatrixVersion enum.
            // Choose the nearest rectangular ECC200 version (12 rows x 36 columns).
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_12x36;

            // Save the barcode image as PNG
            generator.Save("datamatrix.png");
        }

        Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
    }
}