// Title: Invert barcode image colors and evaluate recognition performance
// Description: Demonstrates generating a QR barcode, inverting its colors to create a negative‑image, and comparing recognition results with the InverseImage setting enabled and disabled.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to use BarcodeGenerator to create barcodes, manipulate images with Aspose.Drawing, and employ BarCodeReader with QualitySettings.InverseImage to handle negative‑image barcodes. Developers often need to assess barcode readability under varying color schemes, especially for scanning printed materials with inverted colors.
// Prompt: Adjust color scheme to invert colors and assess recognition performance on negative‑image barcodes.
// Tags: qr, barcode, image inversion, inverseimage, recognition, aspose.barcode, asposedrawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, creates a negative‑image by inverting colors,
/// and evaluates barcode recognition with different InverseImage settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, inversion, and recognition steps.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated images
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeInv_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original and inverted images
        string originalPath = Path.Combine(workFolder, "original.png");
        string invertedPath = Path.Combine(workFolder, "inverted.png");

        // Generate a QR barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.InverseTest"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Invert colors of the generated image to produce a negative‑image
        using (var bitmap = new Bitmap(originalPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color orig = bitmap.GetPixel(x, y);
                    Color inv = Color.FromArgb(255 - orig.R, 255 - orig.G, 255 - orig.B);
                    bitmap.SetPixel(x, y, inv);
                }
            }

            bitmap.Save(invertedPath, ImageFormat.Png);
        }

        // Local function to read a barcode from an image with a specified InverseImage mode
        void ReadAndReport(string imagePath, InverseImageMode mode, string label)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"{label}: File not found -> {imagePath}");
                return;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Configure the reader to treat the image as inverted or not
                reader.QualitySettings.InverseImage = mode;

                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"{label} (InverseImage={mode}): Barcodes read = {results.Length}");

                foreach (var result in results)
                {
                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Assess recognition on the inverted image with InverseImage disabled and enabled
        ReadAndReport(invertedPath, InverseImageMode.Disabled, "InvertedImage - Disabled");
        ReadAndReport(invertedPath, InverseImageMode.Enabled, "InvertedImage - Enabled");

        // Optional cleanup: delete the temporary working folder
        // Directory.Delete(workFolder, true);
    }
}