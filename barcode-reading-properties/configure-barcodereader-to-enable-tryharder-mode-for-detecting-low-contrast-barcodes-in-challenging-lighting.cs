using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a low‑contrast barcode image and reading it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a low‑contrast barcode, saves it to disk, and then attempts to read it.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a low‑contrast barcode image and save it to a file.
        // --------------------------------------------------------------------
        string imagePath = "low_contrast.png";

        // Use a BarcodeGenerator to create a Code128 barcode with the text "LowContrast".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrast"))
        {
            // Set barcode foreground color to a dark gray.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.FromArgb(100, 100, 100);
            // Set background color to a light gray, creating low contrast.
            generator.Parameters.BackColor = Aspose.Drawing.Color.FromArgb(200, 200, 200);
            // Save the generated image to the specified path.
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // 2. Verify that the image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' not found.");
            return;
        }

        // --------------------------------------------------------------------
        // 3. Inform the user about the lack of a dedicated 'TryHarder' option.
        // --------------------------------------------------------------------
        Console.WriteLine("Note: Aspose.BarCode.BarCodeReader does not have a 'TryHarder' property.");
        Console.WriteLine("Enabling high‑quality recognition settings instead.");

        // --------------------------------------------------------------------
        // 4. Read the barcode from the generated image using high‑quality settings.
        // --------------------------------------------------------------------
        using (var bitmap = new Bitmap(imagePath))
        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            // Apply high‑quality settings to improve detection of low‑contrast images.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Enable fast deconvolution to help with blurred or low‑contrast content.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}