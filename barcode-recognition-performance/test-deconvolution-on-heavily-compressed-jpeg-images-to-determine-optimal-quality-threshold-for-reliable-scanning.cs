// Title: Determine optimal JPEG quality for barcode scanning using deconvolution
// Description: Generates a QR barcode, compresses it to JPEG at multiple quality levels, and attempts to read it back using high‑quality deconvolution to identify the lowest quality that still allows reliable detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to create a barcode with BarcodeGenerator, encode the image to JPEG with custom quality settings, and read it using BarCodeReader with QualitySettings and DeconvolutionMode. Typical use cases include testing image compression limits for reliable barcode scanning in mobile or web applications where image size matters.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: qr, barcode, jpeg, quality, deconvolution, recognition, generation, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to evaluate JPEG compression quality thresholds for reliable barcode scanning
/// using Aspose.BarCode generation, image encoding, and deconvolution settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, compresses it to JPEG at various
    /// quality levels, and attempts to read it back using high‑quality deconvolution.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory to store generated JPEG files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeDeconvTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Generate a QR barcode and keep it in memory as a bitmap
        BaseEncodeType encodeType = EncodeTypes.QR;
        using (var generator = new BarcodeGenerator(encodeType, "Test123"))
        {
            using (var pngStream = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0;

                using (var bitmap = new Bitmap(pngStream))
                {
                    // Locate the JPEG codec required for image compression
                    ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(),
                        c => c.FormatID == ImageFormat.Jpeg.Guid);
                    if (jpegCodec == null)
                    {
                        Console.WriteLine("JPEG codec not found.");
                        return;
                    }

                    // Iterate over a range of JPEG quality levels (100 down to 10)
                    for (int quality = 100; quality >= 10; quality -= 10)
                    {
                        string jpegPath = Path.Combine(workDir, $"barcode_q{quality}.jpg");

                        // Encode the bitmap to JPEG using the current quality setting
                        using (var encoderParams = new EncoderParameters(1))
                        {
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
                            bitmap.Save(jpegPath, jpegCodec, encoderParams);
                        }

                        // Attempt to read the barcode from the generated JPEG file
                        bool success = false;
                        if (File.Exists(jpegPath))
                        {
                            try
                            {
                                using (var reader = new BarCodeReader(jpegPath, DecodeType.AllSupportedTypes))
                                {
                                    // Apply high‑quality settings and fast deconvolution for better detection
                                    reader.QualitySettings = QualitySettings.HighQuality;
                                    reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                                    // Read all barcodes; if any are found, mark as success
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"Detected ({result.CodeTypeName}): {result.CodeText}");
                                        success = true;
                                        break;
                                    }
                                }
                            }
                            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                            {
                                Console.WriteLine($"Failed to load image at quality {quality}: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing image at quality {quality}: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"JPEG file not created for quality {quality}.");
                        }

                        // Report the result for the current quality level
                        Console.WriteLine($"Quality {quality}: {(success ? "Success" : "Failed")}");
                    }
                }
            }
        }

        // Cleanup temporary files and directory
        try
        {
            Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}