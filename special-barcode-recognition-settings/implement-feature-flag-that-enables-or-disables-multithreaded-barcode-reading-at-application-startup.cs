using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates configuring multithreaded barcode reading, generating a sample barcode image,
/// and reading the barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures processor settings, optionally generates a sample barcode image,
    /// and reads the barcode from the image.
    /// </summary>
    /// <param name="args">Command‑line arguments; use "disable" to turn off multithreading.</param>
    static void Main(string[] args)
    {
        // Determine whether multithreaded barcode reading should be enabled.
        bool enableMultithreading = true;
        if (args.Length > 0 && args[0].Equals("disable", StringComparison.OrdinalIgnoreCase))
        {
            enableMultithreading = false;
        }

        // Apply processor settings based on the feature flag.
        BarCodeReader.ProcessorSettings.UseAllCores = enableMultithreading;
        if (!enableMultithreading)
        {
            // Restrict processing to a single core when multithreading is disabled.
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        }

        Console.WriteLine($"Multithreaded barcode reading enabled: {enableMultithreading}");

        // Path to the sample barcode image.
        const string imagePath = "sample.png";

        // Generate the sample image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Read the barcode from the image using the configured processor settings.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the detected barcode type and its decoded text.
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
            }
        }
    }
}