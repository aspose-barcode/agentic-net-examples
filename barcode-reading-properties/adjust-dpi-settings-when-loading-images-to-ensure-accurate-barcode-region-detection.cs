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
        // Generate a barcode image with a high DPI to improve detection accuracy
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the resolution (DPI) for the generated image
            generator.Parameters.Resolution = 300f;

            // Create the barcode bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Ensure the bitmap itself has the same DPI
                barcodeBitmap.SetResolution(300f, 300f);

                // Save the bitmap to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    barcodeBitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0;

                    // Read the barcode from the stream
                    using (var reader = new BarCodeReader())
                    {
                        // Specify the barcode type to look for
                        reader.SetBarCodeReadType(DecodeType.Code128);
                        // Load the image stream
                        reader.SetBarCodeImage(memoryStream);

                        // Process detected barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        }
                    }
                }
            }
        }
    }
}