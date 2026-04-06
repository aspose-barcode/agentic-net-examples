using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Long text that would normally wrap in the human‑readable part of the barcode
        string longText = "ThisIsAVeryLongStringThatShouldNotWrapWhenRenderedInTheBarcode";

        // Create a DataMatrix barcode generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Enable NoWrap mode to prevent line breaks in the displayed code text
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Save the generated barcode image
            generator.Save("datamatrix.png");
        }
    }
}