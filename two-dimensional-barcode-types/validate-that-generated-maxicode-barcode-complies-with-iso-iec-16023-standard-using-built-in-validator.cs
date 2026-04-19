using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Output file for the generated MaxiCode barcode
        string outputPath = "maxicode.png";

        // Create MaxiCode codetext for Mode 2 with a standard second message
        var maxiCodeCodetext = new MaxiCodeCodetextMode2();
        maxiCodeCodetext.PostalCode = "524032140";      // 9‑digit US postal code
        maxiCodeCodetext.CountryCode = 56;              // Country code (e.g., USA)
        maxiCodeCodetext.ServiceCategory = 999;         // Service category

        var secondMessage = new MaxiCodeStandardSecondMessage();
        secondMessage.Message = "Test message";
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode image and save it to a file
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.GenerateBarCodeImage();
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the MaxiCode image.");
            return;
        }

        // Validate the generated barcode using the built‑in recognizer
        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No MaxiCode barcode detected in the image.");
                return;
            }

            foreach (var result in results)
            {
                // Decode the raw codetext according to the detected MaxiCode mode
                var decoded = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.MaxiCodeMode, result.CodeText);
                if (decoded != null)
                {
                    Console.WriteLine("Validation succeeded.");
                    Console.WriteLine("Decoded MaxiCode mode: " + decoded.GetMode());
                    Console.WriteLine("Postal code: " + decoded.GetMode()); // example of accessing decoded data
                }
                else
                {
                    Console.WriteLine("Validation failed: unable to decode the MaxiCode codetext.");
                }
            }
        }
    }
}