using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MaxiCodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a MaxiCode codetext for Mode 3 (world postal code) with structured second message.
            var maxiCodeCodetext = new MaxiCodeCodetextMode3
            {
                // Example postal information (6‑character alphanumeric postal code)
                PostalCode = "B1050",
                // 3‑digit ISO country code (e.g., 056 for United States)
                CountryCode = 56,
                // Service category (arbitrary example)
                ServiceCategory = 999
            };

            // Build the structured second message: recipient name, street, city.
            var structuredMessage = new MaxiCodeStructuredSecondMessage();
            structuredMessage.Add("John Doe");
            structuredMessage.Add("123 Main St");
            structuredMessage.Add("Anytown");

            // Assign the structured message to the codetext.
            maxiCodeCodetext.SecondMessage = structuredMessage;

            // Generate and save the MaxiCode barcode image.
            using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the barcode as PNG. The Save method generates the image internally.
                complexGenerator.Save("maxicode.png");
            }

            Console.WriteLine("MaxiCode barcode generated and saved as 'maxicode.png'.");
        }
    }
}