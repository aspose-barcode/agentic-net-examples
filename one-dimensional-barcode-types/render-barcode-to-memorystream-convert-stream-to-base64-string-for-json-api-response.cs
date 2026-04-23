using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Create a barcode generator for Code128 symbology with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Render the barcode into a memory stream in PNG format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the stream's bytes to a Base64 string for JSON response
                string base64Barcode = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string (simulating JSON API response)
                Console.WriteLine(base64Barcode);
            }
        }
    }
}