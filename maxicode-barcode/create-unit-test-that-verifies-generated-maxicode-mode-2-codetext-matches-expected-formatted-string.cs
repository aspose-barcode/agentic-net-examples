using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare MaxiCode Mode 2 codetext
        var maxiCodeCodetext = new MaxiCodeCodetextMode2();
        maxiCodeCodetext.PostalCode = "524032140";
        maxiCodeCodetext.CountryCode = 56; // leading zeros are not required for integer
        maxiCodeCodetext.ServiceCategory = 999;

        var secondMessage = new MaxiCodeStandardSecondMessage();
        secondMessage.Message = "Test message";
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Expected formatted codetext (based on documentation example)
        const string expectedCodetext = "524032140056999Test message";

        // Get the constructed codetext from the object
        string actualCodetext = maxiCodeCodetext.GetConstructedCodetext();

        // Verify that the generated codetext matches the expected string
        if (actualCodetext == expectedCodetext)
        {
            Console.WriteLine("Test Passed: Codetext matches expected value.");
        }
        else
        {
            Console.WriteLine("Test Failed:");
            Console.WriteLine($"Expected: {expectedCodetext}");
            Console.WriteLine($"Actual:   {actualCodetext}");
        }

        // Generate the barcode image to ensure the codetext can be used for rendering
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.GenerateBarCodeImage();

            // Save the image to a memory stream (no file I/O required)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Optionally, you could verify that the stream contains data
                Console.WriteLine($"Generated image size: {ms.Length} bytes");
            }
        }
    }
}