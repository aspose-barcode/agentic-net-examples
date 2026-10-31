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
        // Sample QR code text
        string codeText = "https://example.com";

        // Path to simulate database BLOB storage
        string blobPath = "barcode_blob.bin";

        // Generate QR Code and store as BLOB
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                File.WriteAllBytes(blobPath, imageBytes);
                Console.WriteLine($"QR Code generated and saved to simulated BLOB file '{blobPath}'.");
            }
        }

        // Retrieve BLOB and output as Base64 string (suitable for embedding in a web page)
        if (File.Exists(blobPath))
        {
            byte[] retrievedBytes = File.ReadAllBytes(blobPath);
            using (var ms = new MemoryStream(retrievedBytes))
            {
                using (var bitmap = new Bitmap(ms))
                {
                    using (var outMs = new MemoryStream())
                    {
                        bitmap.Save(outMs, ImageFormat.Png);
                        string base64 = Convert.ToBase64String(outMs.ToArray());
                        Console.WriteLine("Base64 PNG for web display:");
                        Console.WriteLine(base64);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to locate the simulated BLOB file.");
        }
    }
}