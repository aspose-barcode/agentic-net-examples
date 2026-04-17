using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample codetext containing Unicode characters
        string codetext = "犬Right狗";

        // Create a MaxiCode generator with the sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Set ECI encoding to UTF‑8 (character set identifier will be embedded)
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode to a memory stream
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0;

                // Decode the barcode from the memory stream
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                    }
                }

                // Verify that the generator's ECI encoding is set to UTF‑8
                Console.WriteLine($"ECI Encoding set on generator: {generator.Parameters.Barcode.MaxiCode.ECIEncoding}");
            }
        }
    }
}