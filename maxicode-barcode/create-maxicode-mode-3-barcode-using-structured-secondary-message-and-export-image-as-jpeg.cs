using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create MaxiCode codetext for Mode 3
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",          // 6 alphanumeric characters
            CountryCode = 56,              // 3‑digit country code
            ServiceCategory = 999
        };

        // Structured second message
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate and save as JPEG
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save("MaxiCodeMode3.jpg");
        }
    }
}