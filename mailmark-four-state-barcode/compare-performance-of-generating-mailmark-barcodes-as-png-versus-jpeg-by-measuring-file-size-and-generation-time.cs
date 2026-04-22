using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a valid MailmarkCodetext instance (4‑state Mailmark)
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class
        mailmark.SupplychainID = 384224;         // supply chain ID
        mailmark.ItemID = 16563762;              // item ID
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // known‑valid postcode + DPS (trailing space)

        // Use ComplexBarcodeGenerator to create the barcode
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // PNG generation
            using (var pngStream = new MemoryStream())
            {
                var swPng = Stopwatch.StartNew();
                generator.Save(pngStream, BarCodeImageFormat.Png);
                swPng.Stop();

                long pngSize = pngStream.Length;
                Console.WriteLine($"PNG  - Time: {swPng.ElapsedMilliseconds} ms, Size: {pngSize} bytes");
            }

            // JPEG generation
            using (var jpegStream = new MemoryStream())
            {
                var swJpeg = Stopwatch.StartNew();
                generator.Save(jpegStream, BarCodeImageFormat.Jpeg);
                swJpeg.Stop();

                long jpegSize = jpegStream.Length;
                Console.WriteLine($"JPEG - Time: {swJpeg.ElapsedMilliseconds} ms, Size: {jpegSize} bytes");
            }
        }
    }
}