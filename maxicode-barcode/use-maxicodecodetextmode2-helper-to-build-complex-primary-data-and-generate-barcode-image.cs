using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create MaxiCode codetext for Mode 2
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Numeric ISO country code
            ServiceCategory = 999       // Example service category
        };

        // Add a standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample second message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Generate and save the barcode image
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Optional: set image dimensions
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save as PNG
            generator.Save("MaxiCodeMode2.png");
        }
    }
}