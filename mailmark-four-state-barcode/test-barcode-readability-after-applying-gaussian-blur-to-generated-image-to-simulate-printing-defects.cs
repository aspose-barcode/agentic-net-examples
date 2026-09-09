// Title: QR Code Generation with Gaussian Blur Simulation and Readability Test
// Description: Demonstrates generating a QR barcode, applying a simple Gaussian‑like blur by down‑ and up‑scaling, and verifying that the barcode remains readable.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to generate barcodes (BarcodeGenerator), manipulate images (Aspose.Drawing), and recognize barcodes (BarCodeReader). Typical use cases include testing barcode robustness against printing defects, applying image filters, and evaluating recognition quality. Developers often need to simulate real‑world degradations and verify decoding success using quality settings.
// Prompt: Test barcode readability after applying Gaussian blur to the generated image to simulate printing defects.
// Tags: qr, barcode, blur, image processing, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the QR barcode blur readability demonstration.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code, applies a blur effect, and attempts to read it back.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR barcode.
        string codeText = "Test123";

        // Create a QR barcode generator with high error correction level.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Increase error correction to improve readability after blur.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalStream.Position = 0; // Reset stream position for reading.

                // Load the original image from the memory stream.
                using (var originalBitmap = new Bitmap(originalStream))
                {
                    // Calculate reduced dimensions for downscaling (simulates blur).
                    int smallWidth = Math.Max(1, originalBitmap.Width / 2);
                    int smallHeight = Math.Max(1, originalBitmap.Height / 2);

                    // Create a smaller bitmap to downscale the original image.
                    using (var smallBitmap = new Bitmap(smallWidth, smallHeight))
                    {
                        // Draw the original image onto the smaller bitmap.
                        using (var gSmall = Graphics.FromImage(smallBitmap))
                        {
                            gSmall.DrawImage(originalBitmap, 0, 0, smallWidth, smallHeight);
                        }

                        // Create a bitmap with the original dimensions to upscale back.
                        using (var blurredBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                        {
                            // Upscale the small bitmap back to original size, creating a blur effect.
                            using (var gBlur = Graphics.FromImage(blurredBitmap))
                            {
                                gBlur.DrawImage(smallBitmap, 0, 0, originalBitmap.Width, originalBitmap.Height);
                            }

                            // Save the blurred image to a new memory stream.
                            using (var blurredStream = new MemoryStream())
                            {
                                blurredBitmap.Save(blurredStream, ImageFormat.Png);
                                blurredStream.Position = 0; // Reset for reading.

                                // Initialize a barcode reader for QR codes on the blurred image.
                                using (var reader = new BarCodeReader(blurredStream, DecodeType.QR))
                                {
                                    // Apply a fast deconvolution mode to aid recognition of blurred images.
                                    reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                                    // Attempt to read barcodes from the blurred image.
                                    BarCodeResult[] results = reader.ReadBarCodes();

                                    // Output the result of the recognition attempt.
                                    if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                                    {
                                        Console.WriteLine($"Barcode read successfully: {results[0].CodeText}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to read barcode from blurred image.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}