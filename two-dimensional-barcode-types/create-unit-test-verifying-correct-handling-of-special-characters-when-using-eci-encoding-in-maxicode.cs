using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Original text containing special Unicode characters
        string originalText = "犬Right狗";

        // Generate MaxiCode barcode with ECI encoding (UTF-8)
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Set ECI mode and encoding
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.ECI;
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save barcode to memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Decode the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Assume first result is the generated barcode
                    string decodedText = results[0].CodeText;

                    // Verify that the decoded text matches the original
                    if (decodedText == originalText)
                    {
                        Console.WriteLine("Success: Decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"Failure: Decoded text does not match.\nOriginal: {originalText}\nDecoded: {decodedText}");
                    }
                }
            }
        }
    }
}