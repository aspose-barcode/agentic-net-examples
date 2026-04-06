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
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Save the barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Build the HTML <img> tag with a data URI
                string imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML tag
                Console.WriteLine(imgTag);
            }
        }
    }
}