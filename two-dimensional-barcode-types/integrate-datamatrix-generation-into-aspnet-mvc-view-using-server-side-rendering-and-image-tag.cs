using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageBytes);

                    // Build an HTML img tag that can be used in an MVC view
                    string htmlImgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"DataMatrix Barcode\" />";

                    // Output the HTML snippet
                    Console.WriteLine(htmlImgTag);
                }
            }
        }
    }
}