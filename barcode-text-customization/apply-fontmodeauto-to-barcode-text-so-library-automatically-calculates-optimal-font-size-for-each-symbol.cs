using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set FontMode to Auto so the library calculates optimal font size
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}