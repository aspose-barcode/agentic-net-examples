using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeDecodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Base64‑encoded PNG image (1×1 pixel). Replace with actual barcode image if needed.
            string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64Image);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Provided string is not a valid Base64-encoded image.", ex);
            }

            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                using (BarCodeReader reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    reader.BarcodeSettings.DetectEncoding = true;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded Text: " + result.CodeText);
                    }
                }
            }
        }
    }
}