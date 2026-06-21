using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, conversion to grayscale, and recognition comparison
/// using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Converts a color <see cref="Bitmap"/> to a grayscale image.
    /// </summary>
    /// <param name="source">The source color bitmap.</param>
    /// <returns>A new bitmap containing the grayscale representation of the source.</returns>
    static Bitmap ConvertToGrayscale(Bitmap source)
    {
        // Create a new bitmap with the same dimensions as the source.
        var gray = new Bitmap(source.Width, source.Height);

        // Iterate over each pixel to compute its luminance.
        for (int y = 0; y < source.Height; y++)
        {
            for (int x = 0; x < source.Width; x++)
            {
                // Retrieve the original pixel color.
                Color c = source.GetPixel(x, y);

                // Calculate luminance using the standard NTSC coefficients.
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);

                // Create a gray color where R, G, and B are equal to the luminance.
                Color grayColor = Color.FromArgb(lum, lum, lum);

                // Set the pixel in the grayscale bitmap.
                gray.SetPixel(x, y, grayColor);
            }
        }

        return gray;
    }

    /// <summary>
    /// Entry point of the program. Generates barcodes, creates grayscale versions,
    /// and compares detection results between color and grayscale images.
    /// </summary>
    static void Main()
    {
        // Define a list of barcode symbologies and the corresponding sample texts.
        var symbologies = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417Test")
        };

        // Process each symbology/text pair.
        foreach (var (sym, txt) in symbologies)
        {
            // Generate a barcode image in color.
            using (var generator = new BarcodeGenerator(sym, txt))
            {
                using (Bitmap colorBmp = generator.GenerateBarCodeImage())
                {
                    // Convert the color barcode image to grayscale.
                    using (Bitmap grayBmp = ConvertToGrayscale(colorBmp))
                    {
                        // -------------------- Recognition on Color Image --------------------
                        bool detectedColor;
                        BarCodeConfidence confidenceColor = BarCodeConfidence.None;

                        // Initialize a reader for the color bitmap.
                        using (var readerColor = new BarCodeReader(colorBmp, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes present in the image.
                            var results = readerColor.ReadBarCodes();

                            // Determine if any barcode was detected.
                            detectedColor = results.Length > 0;

                            // Capture confidence of the first detected barcode, if any.
                            if (detectedColor)
                                confidenceColor = results[0].Confidence;
                        }

                        // -------------------- Recognition on Grayscale Image --------------------
                        bool detectedGray;
                        BarCodeConfidence confidenceGray = BarCodeConfidence.None;

                        // Initialize a reader for the grayscale bitmap.
                        using (var readerGray = new BarCodeReader(grayBmp, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes present in the image.
                            var results = readerGray.ReadBarCodes();

                            // Determine if any barcode was detected.
                            detectedGray = results.Length > 0;

                            // Capture confidence of the first detected barcode, if any.
                            if (detectedGray)
                                confidenceGray = results[0].Confidence;
                        }

                        // -------------------- Output Comparison --------------------
                        Console.WriteLine($"{sym.TypeName}:");
                        Console.WriteLine($"  Color     - Detected: {detectedColor}, Confidence: {confidenceColor}");
                        Console.WriteLine($"  Grayscale - Detected: {detectedGray}, Confidence: {confidenceGray}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}