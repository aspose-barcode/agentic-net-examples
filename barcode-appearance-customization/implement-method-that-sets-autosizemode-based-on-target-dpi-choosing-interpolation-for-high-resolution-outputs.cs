// Title: Barcode Generation with DPI‑Based AutoSizeMode
// Description: Demonstrates setting the AutoSizeMode of a BarcodeGenerator based on the target DPI, using interpolation for high‑resolution outputs.
// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
// Tags: barcode, autosizemode, dpi, interpolation, aspose.barcode, c#
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a barcode image and adjusts its AutoSizeMode
/// according to the desired output DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Sets the AutoSizeMode of the provided <see cref="BarcodeGenerator"/> based on the target DPI.
    /// For DPI values greater than 300, <see cref="AutoSizeMode.Interpolation"/> is used;
    /// otherwise, <see cref="AutoSizeMode.None"/> is applied.
    /// </summary>
    /// <param name="generator">The barcode generator to configure.</param>
    /// <param name="targetDpi">The desired resolution in dots per inch.</param>
    static void SetAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        // Validate input arguments.
        if (generator == null)
            throw new ArgumentNullException(nameof(generator));

        if (targetDpi <= 0f)
            throw new ArgumentOutOfRangeException(nameof(targetDpi), "DPI must be greater than zero.");

        // Apply the requested resolution to the generator.
        generator.Parameters.Resolution = targetDpi;

        if (targetDpi > 300f)
        {
            // High‑resolution output: enable interpolation auto‑sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Example dimensions – adjust as needed for your use case.
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 150f;
        }
        else
        {
            // Lower DPI: keep default sizing (no auto‑size).
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }

    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, configures DPI‑based
    /// auto‑size settings, and saves the image to disk.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample numeric data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Define the target DPI for the output image.
            float targetDpi = 350f; // Modify to test different DPI thresholds.

            // Configure the generator's AutoSizeMode based on the target DPI.
            SetAutoSizeMode(generator, targetDpi);

            // Optional: set a foreground color to illustrate additional parameter usage.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;

            // Save the generated barcode image to a file.
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Inform the user about the saved file and the applied settings.
            Console.WriteLine($"Barcode saved to '{outputPath}' with DPI {targetDpi} and AutoSizeMode {generator.Parameters.AutoSizeMode}.");
        }
    }
}