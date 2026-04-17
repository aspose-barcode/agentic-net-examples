using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare MaxiCode Mode 2 codetext
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Sample message"
            }
        };

        // Generate the barcode and apply a custom foreground color
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Save the barcode image
            generator.Save("maxicode_mode2.png");
        }
    }
}