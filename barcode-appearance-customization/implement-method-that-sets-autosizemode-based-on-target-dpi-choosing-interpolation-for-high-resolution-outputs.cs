using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Desired output DPI
            float targetDpi = 350f;

            // Set AutoSizeMode based on DPI
            SetAutoSizeMode(generator, targetDpi);

            // If using Interpolation, specify target image dimensions
            if (generator.Parameters.AutoSizeMode == AutoSizeMode.Interpolation)
            {
                generator.Parameters.ImageWidth.Point = 600f;
                generator.Parameters.ImageHeight.Point = 300f;
            }

            // Save the barcode image
            generator.Save("barcode.png");
            Console.WriteLine($"Barcode saved with AutoSizeMode={generator.Parameters.AutoSizeMode} at DPI={targetDpi}");
        }
    }

    static void SetAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        if (targetDpi <= 0f) throw new ArgumentException("DPI must be greater than zero.", nameof(targetDpi));

        const float highResolutionThreshold = 300f;

        // Choose Interpolation for high‑resolution outputs
        if (targetDpi >= highResolutionThreshold)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }

        // Apply the target resolution
        generator.Parameters.Resolution = targetDpi;
    }
}