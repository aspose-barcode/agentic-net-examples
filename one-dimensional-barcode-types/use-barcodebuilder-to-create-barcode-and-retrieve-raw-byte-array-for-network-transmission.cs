using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "123ABC";

                // Generate the barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        // Retrieve the raw byte array for transmission
                        byte[] barcodeBytes = ms.ToArray();

                        // Example output: display the byte array length
                        Console.WriteLine($"Barcode byte array length: {barcodeBytes.Length}");
                    }
                }
            }
        }
    }
}