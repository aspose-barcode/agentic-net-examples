using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a barcode image with DPI‑dependent auto‑size mode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Adjusts the <see cref="AutoSizeMode"/> of a <see cref="BarcodeGenerator"/> based on the desired DPI.
    /// High‑resolution (>= 300 DPI) uses <see cref="AutoSizeMode.Interpolation"/>, otherwise disables automatic sizing (<see cref="AutoSizeMode.None"/>).
    /// </summary>
    /// <param name="generator">The barcode generator to configure.</param>
    /// <param name="targetDpi">The target image resolution in dots per inch.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="generator"/> is null.</exception>
    static void SetAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        // Validate input.
        if (generator == null)
            throw new ArgumentNullException(nameof(generator));

        // Set the image resolution (DPI).
        generator.Parameters.Resolution = targetDpi;

        // Choose the appropriate AutoSizeMode based on DPI.
        if (targetDpi >= 300f)
        {
            // For high‑resolution output, use interpolation to improve quality.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            // For lower DPI, disable automatic sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Barcode data to encode.
        const string codeText = "1234567890";

        // Output file path.
        const string outputPath = "barcode.png";

        // Desired DPI for the output image.
        const float targetDpi = 300f;

        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply DPI‑based AutoSizeMode configuration.
            SetAutoSizeMode(generator, targetDpi);

            // Optional: customize barcode appearance (height and X‑dimension).
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to '{outputPath}' with DPI {targetDpi}.");
    }
}