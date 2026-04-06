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
            // Set the text to be encoded
            generator.CodeText = "123ABC";

            // Save the barcode directly to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset stream position before reading
                memoryStream.Position = 0;

                // Write the stream to a file to simulate immediate download
                // In an ASP.NET scenario, you would write memoryStream to Response.OutputStream
                File.WriteAllBytes("barcode.png", memoryStream.ToArray());
            }
        }

        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}