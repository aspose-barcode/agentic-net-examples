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
        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Render barcode to bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to memory stream (PNG format)
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    byte[] barcodeBlob = memoryStream.ToArray(); // Simulated BLOB storage

                    // Simulate retrieval from database
                    byte[] retrievedBlob = barcodeBlob; // In real scenario, fetch from DB

                    // Convert BLOB to Base64 for web display
                    string base64Image = Convert.ToBase64String(retrievedBlob);
                    Console.WriteLine("data:image/png;base64," + base64Image);
                }
            }
        }
    }
}