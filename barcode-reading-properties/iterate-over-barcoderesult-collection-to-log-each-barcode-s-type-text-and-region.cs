using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    // Save the bitmap to a PNG stream
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    // Read barcodes from the generated image
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Log barcode type, text, and region rectangle
                            Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                            Console.WriteLine("BarCode Text: " + result.CodeText);
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"BarCode Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                            Console.WriteLine(); // blank line for readability
                        }
                    }
                }
            }
        }
    }
}