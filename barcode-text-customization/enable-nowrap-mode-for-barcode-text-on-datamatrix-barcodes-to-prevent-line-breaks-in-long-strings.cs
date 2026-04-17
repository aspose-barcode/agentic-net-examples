using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Long text that would normally wrap
        string longText = "ThisIsAVeryLongStringThatShouldNotWrapWhenNoWrapIsEnabledInDataMatrixBarcode";

        // Create a DataMatrix barcode generator with the long text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Enable NoWrap to prevent line breaks in the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Optional: set image size if needed
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the barcode image
            generator.Save("datamatrix_no_wrap.png");
        }

        Console.WriteLine("DataMatrix barcode generated with NoWrap enabled.");
    }
}