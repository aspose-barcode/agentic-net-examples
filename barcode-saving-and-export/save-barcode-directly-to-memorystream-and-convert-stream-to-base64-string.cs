using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Save the barcode image directly to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Ensure the stream position is at the beginning before reading
                memoryStream.Position = 0;
                // Convert the image bytes to a Base64 string
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                // Output the Base64 string
                Console.WriteLine(base64String);
            }
        }
    }
}