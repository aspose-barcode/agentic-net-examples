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
        // UPC‑A part + GS1 Code128 coupon part
        string codeText = "514141100906(8102)03";

        // Create generator for UPC‑A with GS1 Code128 coupon
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Optional: set image dimensions
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate barcode bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to memory as PNG
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(pngBytes);

                    // Build simple HTML embedding the image as base64
                    string html = $"<html><body><h2>UPC‑A with GS1 Code128 Coupon</h2>" +
                                  $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\"/></body></html>";

                    // Write HTML file
                    File.WriteAllText("barcode.html", html);
                }
            }
        }
    }
}