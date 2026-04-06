using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: Generate a barcode image and obtain its Base64 representation.
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HelloWorld"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string.
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // (Optional) Display the Base64 string.
        Console.WriteLine("Base64 Barcode Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // Step 2: Decode the Base64 string back to an image stream.
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            // Step 3: Read the barcode from the image stream.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }
    }
}