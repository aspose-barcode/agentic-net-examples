using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize the reader with the generated bitmap and specify the decode type
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Iterate through detected barcodes (should be one in this case)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Obtain the rectangle that bounds the barcode region
                        var rect = result.Region.Rectangle;

                        // Output absolute pixel coordinates and size
                        Console.WriteLine($"Barcode Region:");
                        Console.WriteLine($"  X: {rect.X}");
                        Console.WriteLine($"  Y: {rect.Y}");
                        Console.WriteLine($"  Width: {rect.Width}");
                        Console.WriteLine($"  Height: {rect.Height}");
                    }
                }
            }
        }
    }
}