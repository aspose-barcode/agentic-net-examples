using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Sets the resolution and chooses an appropriate AutoSizeMode.
    // For DPI values greater than 300, Interpolation mode is used to preserve quality.
    // Otherwise, AutoSizeMode is set to None (default sizing).
    static void SetAutoSizeModeByResolution(BarcodeGenerator generator, float targetDpi)
    {
        if (generator == null)
            throw new ArgumentNullException(nameof(generator));

        if (targetDpi <= 0f)
            throw new ArgumentOutOfRangeException(nameof(targetDpi), "DPI must be greater than zero.");

        // Apply the requested resolution.
        generator.Parameters.Resolution = targetDpi;

        // Choose AutoSizeMode based on the DPI threshold.
        const float HighResolutionThreshold = 300f;
        if (targetDpi > HighResolutionThreshold)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        else
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
    }

    static void Main()
    {
        // Example: generate a Code128 barcode with a high‑resolution setting.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set desired DPI (e.g., 600) and automatically configure AutoSizeMode.
            SetAutoSizeModeByResolution(generator, 600f);

            // Optionally, specify the output image size when using Interpolation mode.
            // Here we set a larger width/height to demonstrate scaling.
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image.
            generator.Save("code128_highres.png");
            Console.WriteLine("Barcode saved with DPI 600 and AutoSizeMode.Interpolation.");
        }

        // Example: generate a QR code with a standard DPI.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            SetAutoSizeModeByResolution(generator, 96f); // standard screen DPI
            generator.Save("qr_standard.png");
            Console.WriteLine("QR code saved with DPI 96 and AutoSizeMode.None.");
        }
    }
}