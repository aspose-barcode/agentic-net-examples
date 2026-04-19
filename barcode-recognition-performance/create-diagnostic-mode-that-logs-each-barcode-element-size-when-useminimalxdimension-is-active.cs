using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        // Ensure a barcode image exists; generate one if missing.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123456";
                // Set a visible X-dimension for generation.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(imagePath);
            }
        }

        // Verify the file exists before attempting recognition.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using the UseMinimalXDimension mode.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Load the image to obtain its pixel dimensions.
                using (var bitmap = (Bitmap)Image.FromFile(imagePath))
                {
                    Console.WriteLine($"Image Size: {bitmap.Width}x{bitmap.Height} pixels");
                }
            }
        }
    }
}