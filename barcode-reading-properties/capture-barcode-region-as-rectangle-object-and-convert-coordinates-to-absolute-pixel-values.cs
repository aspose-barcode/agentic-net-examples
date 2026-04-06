using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set image size using unit members
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image in memory
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Initialize the reader with the bitmap and specify the decode type
                using (var reader = new BarCodeReader(barcodeBitmap, DecodeType.Code128))
                {
                    // Read all barcodes found in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Obtain the rectangle that bounds the detected barcode
                        var rect = result.Region.Rectangle;

                        // Output the barcode text and absolute pixel coordinates of the region
                        Console.WriteLine($"Barcode Text: {result.CodeText}");
                        Console.WriteLine($"Region X: {rect.X}, Y: {rect.Y}, Width: {rect.Width}, Height: {rect.Height}");
                    }
                }
            }
        }
    }
}