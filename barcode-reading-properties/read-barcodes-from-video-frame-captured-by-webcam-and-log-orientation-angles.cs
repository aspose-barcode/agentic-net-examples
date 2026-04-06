using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a barcode image with a known rotation angle.
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Set rotation angle to simulate a tilted barcode in a video frame.
                generator.Parameters.RotationAngle = 45f;
                // Save the barcode to the memory stream in PNG format.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading.
            imageStream.Position = 0;

            // Read the barcode from the generated image and log its orientation angle.
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Angle: {result.Region.Angle}");
                }
            }
        }
    }
}