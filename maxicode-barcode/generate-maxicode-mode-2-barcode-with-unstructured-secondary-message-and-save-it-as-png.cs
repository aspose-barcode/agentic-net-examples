using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MaxiCodeExample
{
    class Program
    {
        static void Main()
        {
            // Create MaxiCode codetext for Mode 2
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",   // 9‑digit US postal code
                CountryCode = 056,          // USA country code
                ServiceCategory = 999       // Example service category
            };

            // Unstructured (standard) secondary message
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Unstructured secondary message"
            };
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate and save the barcode as PNG
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save("maxicode_mode2.png");
            }
        }
    }
}