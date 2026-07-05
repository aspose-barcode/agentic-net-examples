// Title: Barcode false positive detection with low MinimalXDimension
// Description: Demonstrates how setting MinimalXDimension too low can cause the reader to report barcodes on a blank image, counting false positives.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: barcode, false-positive, minimalxdimension, code128, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, creates a blank image of the same size,
/// and measures false positives when the MinimalXDimension quality setting is set too low.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, prepares a blank image, and counts false positives
    /// produced by the barcode reader under deliberately poor MinimalXDimension settings.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode image and keep it in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save the barcode image to a memory stream for later reuse.
                using (var barcodeStream = new MemoryStream())
                {
                    barcodeImage.Save(barcodeStream, ImageFormat.Png);
                    barcodeStream.Position = 0;

                    // Create a blank (white) image of the same dimensions – this image contains no barcode.
                    using (var blankImage = new Bitmap(barcodeImage.Width, barcodeImage.Height))
                    {
                        using (var graphics = Graphics.FromImage(blankImage))
                        {
                            graphics.Clear(Aspose.Drawing.Color.White);
                        }

                        int falsePositiveCount = 0;
                        const int attempts = 5; // small safe sample size

                        // Perform multiple recognition attempts on the blank image.
                        for (int i = 0; i < attempts; i++)
                        {
                            // Use the same blank image for each attempt.
                            using (var reader = new BarCodeReader(blankImage, DecodeType.AllSupportedTypes))
                            {
                                // Set MinimalXDimension deliberately low to provoke false positives.
                                reader.QualitySettings.MinimalXDimension = 0.1f; // too low

                                // Optionally allow recognition of incorrect barcodes.
                                reader.QualitySettings.AllowIncorrectBarcodes = true;

                                // Perform recognition.
                                var results = reader.ReadBarCodes();

                                // If any barcode is reported, count it as a false positive.
                                if (results != null && results.Length > 0)
                                {
                                    falsePositiveCount++;
                                }
                            }
                        }

                        Console.WriteLine($"False positives detected with MinimalXDimension set too low: {falsePositiveCount}");
                    }
                }
            }
        }
    }
}