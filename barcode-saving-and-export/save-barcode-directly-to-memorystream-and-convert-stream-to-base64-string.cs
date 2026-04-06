using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Save the barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string
                Console.WriteLine(base64String);
            }
        }
    }
}