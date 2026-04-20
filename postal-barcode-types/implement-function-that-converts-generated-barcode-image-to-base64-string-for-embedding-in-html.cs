using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    barcodeImage.Save(memoryStream, ImageFormat.Png);
                    // Convert the image bytes to a Base64 string
                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    // Output the Base64 string (can be embedded in HTML)
                    Console.WriteLine(base64String);
                }
            }
        }
    }
}