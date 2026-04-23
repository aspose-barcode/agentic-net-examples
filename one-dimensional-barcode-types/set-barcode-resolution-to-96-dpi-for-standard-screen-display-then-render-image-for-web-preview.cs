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
        const string outputFile = "barcode.png";
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set resolution to 96 DPI (standard screen display)
            generator.Parameters.Resolution = 96f;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a file (web preview source)
                bitmap.Save(outputFile, ImageFormat.Png);

                // Also output a Base64 data URI for quick web preview
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    Console.WriteLine("data:image/png;base64," + base64);
                }
            }
        }
    }
}