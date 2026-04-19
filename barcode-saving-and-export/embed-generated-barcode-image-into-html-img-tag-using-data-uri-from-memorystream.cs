using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Save the barcode image to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Ensure the stream position is at the beginning
                ms.Position = 0;

                // Convert the image bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the HTML img tag with a data URI
                string imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML tag
                Console.WriteLine(imgTag);
            }
        }
    }
}