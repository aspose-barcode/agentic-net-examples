using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, creating a negative image,
/// and recognizing the barcode from the negative image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates a barcode, inverts its colors, saves the negative image,
    /// and attempts to read the barcode from the negative image.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode with the text "Test123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set barcode colors: black bars on a white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the PNG image from the memory stream into a bitmap
                using (var bitmap = new Bitmap(ms))
                {
                    // Create a new bitmap to hold the negative (color‑inverted) image
                    using (var negative = new Bitmap(bitmap.Width, bitmap.Height))
                    {
                        // Iterate over each pixel to invert its color
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                Color original = bitmap.GetPixel(x, y);
                                Color inverted = Color.FromArgb(
                                    255 - original.R,
                                    255 - original.G,
                                    255 - original.B);
                                negative.SetPixel(x, y, inverted);
                            }
                        }

                        // Save the negative image to a file (optional step)
                        const string negativePath = "negative.png";
                        negative.Save(negativePath, ImageFormat.Png);
                        Console.WriteLine($"Negative image saved to {negativePath}");

                        // Initialize a barcode reader for the negative image
                        using (var reader = new BarCodeReader(negative, DecodeType.AllSupportedTypes))
                        {
                            // Enable detection of inverse (negative) images
                            reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

                            // Measure recognition time
                            var stopwatch = Stopwatch.StartNew();
                            var results = reader.ReadBarCodes();
                            stopwatch.Stop();

                            Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");

                            // Output details for each detected barcode
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Code Text: {result.CodeText}");
                                Console.WriteLine($"Confidence: {result.Confidence}");
                            }

                            // Inform the user if no barcodes were found
                            if (results.Length == 0)
                            {
                                Console.WriteLine("No barcode detected in the negative image.");
                            }
                        }
                    }
                }
            }
        }
    }
}