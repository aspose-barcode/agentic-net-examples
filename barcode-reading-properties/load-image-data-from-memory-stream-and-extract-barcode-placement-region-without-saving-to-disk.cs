using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    // Save the bitmap to a PNG stream
                    barcodeBitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    // Read the barcode from the memory stream
                    using (var reader = new BarCodeReader())
                    {
                        // Specify the barcode type to look for
                        reader.SetBarCodeReadType(DecodeType.Code128);
                        // Assign the image stream for recognition
                        reader.SetBarCodeImage(imageStream);

                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Extract the bounding rectangle of the recognized barcode
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Barcode Text: {result.CodeText}");
                            Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        }
                    }
                }
            }
        }
    }
}