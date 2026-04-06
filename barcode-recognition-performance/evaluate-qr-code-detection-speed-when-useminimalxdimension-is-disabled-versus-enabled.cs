using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated QR code image
        string imagePath = "qr.png";

        // Generate a QR code with some sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://www.example.com"))
        {
            // Optional: set image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;
            generator.Save(imagePath);
        }

        // Ensure the image exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // Measure detection time with UseMinimalXDimension disabled (Normal mode)
        long timeNormal = MeasureRecognitionTime(imagePath, XDimensionMode.Normal, false);

        // Measure detection time with UseMinimalXDimension enabled
        long timeUseMinimal = MeasureRecognitionTime(imagePath, XDimensionMode.UseMinimalXDimension, true);

        Console.WriteLine($"Recognition time (UseMinimalXDimension disabled): {timeNormal} ms");
        Console.WriteLine($"Recognition time (UseMinimalXDimension enabled): {timeUseMinimal} ms");
    }

    static long MeasureRecognitionTime(string imagePath, XDimensionMode mode, bool enableUseMinimal)
    {
        // Prepare the reader
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Set the desired XDimension mode
            reader.QualitySettings.XDimension = mode;

            // If UseMinimalXDimension is enabled, set MinimalXDimension value
            if (enableUseMinimal)
            {
                // Example minimal size of 2 pixels
                reader.QualitySettings.MinimalXDimension = 2;
            }

            // Warm up the reader (optional)
            reader.ReadBarCodes();

            // Measure the time for recognition
            Stopwatch sw = Stopwatch.StartNew();
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access result to ensure full processing
                Console.WriteLine($"Detected: {result.CodeText}");
            }
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }
    }
}