using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image with a known rotation angle.
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Set a rotation angle to simulate an oriented barcode.
                generator.Parameters.RotationAngle = 30f;
                // Save the barcode to the memory stream in PNG format.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position before reading.
            imageStream.Position = 0;

            // Read barcodes from the generated image.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                }
            }
        }
    }
}