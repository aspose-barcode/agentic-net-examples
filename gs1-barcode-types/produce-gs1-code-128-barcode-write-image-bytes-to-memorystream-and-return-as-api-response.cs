using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 Code 128 data (AI 01 for GTIN)
        string codeText = "(01)12345678901231";

        // Create barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Save barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Get the image bytes (simulating API response payload)
                byte[] imageBytes = memoryStream.ToArray();

                // Example: output Base64 string of the image
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}