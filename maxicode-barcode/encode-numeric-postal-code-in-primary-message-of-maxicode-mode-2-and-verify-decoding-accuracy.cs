using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Define the data for the MaxiCode Mode 2 barcode
        const string postalCode = "123456789"; // 9‑digit numeric postal code
        const int countryCode = 840; // USA
        const int serviceCategory = 999;
        const string secondMessageText = "Sample message";

        // Create the codetext object for Mode 2
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.GenerateBarCodeImage();

            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream for reading

                // Decode the barcode from the generated image
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    bool decoded = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Obtain the MaxiCode mode from the extended result
                        var mode = result.Extended?.MaxiCode?.MaxiCodeMode ?? MaxiCodeMode.Mode2;

                        // Decode the raw codetext into a structured object
                        MaxiCodeCodetext decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                        if (decodedCodetext is MaxiCodeCodetextMode2 decodedMode2)
                        {
                            // Verify that the postal code matches the original value
                            if (decodedMode2.PostalCode == postalCode)
                            {
                                Console.WriteLine("Decoding successful. Postal code matches: " + decodedMode2.PostalCode);
                            }
                            else
                            {
                                Console.WriteLine("Decoding failed. Expected postal code: " + postalCode +
                                                  ", but got: " + decodedMode2.PostalCode);
                            }
                            decoded = true;
                        }
                    }

                    if (!decoded)
                    {
                        Console.WriteLine("No MaxiCode barcode was decoded from the image.");
                    }
                }
            }
        }
    }
}