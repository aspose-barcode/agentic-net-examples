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
        // Simulated query parameters
        string codeText = "1234567890123456789012345678901234"; // sample codetext
        float aspectRatio = 1.5f; // Height/Width ratio for Code 16K
        int quietZoneLeftCoef = 12; // left quiet zone coefficient
        int quietZoneRightCoef = 2; // right quiet zone coefficient

        // Create the barcode generator for Code 16K
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply Code 16K specific parameters
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietZoneLeftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietZoneRightCoef;

            // Optionally set image dimensions (using Point units)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f * aspectRatio;

            // Generate the barcode image into a memory stream (PNG format)
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // For demonstration purposes, write the image to a file
                File.WriteAllBytes("code16k.png", memoryStream.ToArray());
                Console.WriteLine("Code 16K barcode image generated: code16k.png");
            }
        }
    }
}