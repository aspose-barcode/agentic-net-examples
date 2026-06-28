using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a file,
/// and then attempting to read it with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its existence,
    /// and tries to decode it using Aspose.BarCode with fallback logic.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a sample barcode image and save it to disk.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        bool decoded = false; // Tracks whether decoding succeeded.

        // ------------------------------------------------------------
        // First decoding attempt: use default StripFNC = false.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false; // Explicitly set to false for clarity.
            var results = reader.ReadBarCodes();

            if (results.Length > 0)
            {
                decoded = true; // Mark as successfully decoded.
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded (first attempt): {result.CodeText}");
                }
            }
        }

        // ------------------------------------------------------------
        // Fallback decoding attempt: enable StripFNC if first attempt failed.
        // ------------------------------------------------------------
        if (!decoded)
        {
            using (var fallbackReader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                fallbackReader.BarcodeSettings.StripFNC = true; // Enable StripFNC for fallback.
                var fallbackResults = fallbackReader.ReadBarCodes();

                if (fallbackResults.Length > 0)
                {
                    foreach (var result in fallbackResults)
                    {
                        Console.WriteLine($"Decoded (fallback with StripFNC): {result.CodeText}");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to decode barcode even after fallback.");
                }
            }
        }
    }
}