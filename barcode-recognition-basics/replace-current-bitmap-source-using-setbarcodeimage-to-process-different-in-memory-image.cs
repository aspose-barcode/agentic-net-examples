using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap with the same size
                using (Bitmap newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                {
                    // Draw the original barcode onto the new bitmap (you could modify it here)
                    using (Graphics graphics = Graphics.FromImage(newBitmap))
                    {
                        graphics.Clear(Aspose.Drawing.Color.White);
                        graphics.DrawImage(originalBitmap, 0, 0, originalBitmap.Width, originalBitmap.Height);
                    }

                    // Save images for visual verification (optional)
                    originalBitmap.Save("original.png", ImageFormat.Png);
                    newBitmap.Save("new.png", ImageFormat.Png);

                    // Use BarCodeReader with SetBarCodeImage to recognize from the new bitmap
                    using (BarCodeReader reader = new BarCodeReader())
                    {
                        reader.SetBarCodeReadType(DecodeType.Code128);
                        reader.SetBarCodeImage(newBitmap);
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}