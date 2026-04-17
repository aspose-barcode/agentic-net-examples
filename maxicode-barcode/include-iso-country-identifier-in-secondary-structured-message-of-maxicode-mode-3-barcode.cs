using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create MaxiCode Mode 3 codetext
        var maxiCodeCodetext = new MaxiCodeCodetextMode3();
        maxiCodeCodetext.PostalCode = "B1050";
        maxiCodeCodetext.CountryCode = 056; // numeric ISO country code (e.g., US = 056)
        maxiCodeCodetext.ServiceCategory = 999;

        // Build structured second message and include ISO country identifier
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Add("US"); // ISO country identifier
        structuredMessage.Year = 99;

        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate and save the barcode image
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            complexGenerator.Save("maxicode_mode3.png");
        }

        Console.WriteLine("MaxiCode Mode 3 barcode generated successfully.");
    }
}