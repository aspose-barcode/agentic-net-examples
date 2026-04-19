using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MaxiCodeExample
{
    class Program
    {
        static void Main()
        {
            // Prepare MaxiCode codetext for mode 3
            var maxiCodeCodetext = new MaxiCodeCodetextMode3();
            maxiCodeCodetext.PostalCode = "B1050";          // 6‑character alphanumeric postal code
            maxiCodeCodetext.CountryCode = 56;             // Numeric country code
            maxiCodeCodetext.ServiceCategory = 999;       // Service category

            // Add a standard second message
            var secondMessage = new MaxiCodeStandardSecondMessage();
            secondMessage.Message = "Sample MaxiCode Mode 3";
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate the barcode and save it as PNG
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save("maxicode_mode3.png");
            }
        }
    }
}