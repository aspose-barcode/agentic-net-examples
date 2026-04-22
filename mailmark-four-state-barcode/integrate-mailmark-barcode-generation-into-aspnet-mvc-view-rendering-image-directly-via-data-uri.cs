using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext with valid sample values
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set a positive bar height (e.g., 5 mm)
            generator.Parameters.Barcode.BarHeight.Millimeters = 5;

            // Ensure the image size is positive
            generator.Parameters.ImageWidth.Pixels = 300;
            generator.Parameters.ImageHeight.Pixels = 150;

            // Create the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    string dataUri = "data:image/png;base64," + base64;
                    Console.WriteLine(dataUri);
                }
            }
        }
    }
}