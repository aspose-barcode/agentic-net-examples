using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a QR code generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the resolution to 300 dpi
            generator.Parameters.Resolution = 300f;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Write the bitmap to a memory stream in PNG format
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    // Example usage: output the size of the generated image data
                    Console.WriteLine($"Generated QR code image size: {memoryStream.Length} bytes");
                }
            }
        }
    }
}