// Title: Determine optimal JPEG quality for barcode scanning using deconvolution
// Description: Generates a Code128 barcode, saves it as JPEG with different compression qualities, and evaluates scanning success using deconvolution.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on image compression effects. Shows how to use BarcodeGenerator, BarCodeReader, QualitySettings, and DeconvolutionMode to assess scan reliability across JPEG quality levels—common for developers optimizing barcode imaging pipelines.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: barcode, deconvolution, jpeg, quality, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode, compresses it to JPEG at various quality levels,
/// and uses deconvolution to determine the minimum quality that still allows reliable scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, compression, and recognition steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated JPEG test images.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDeconvolutionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate a Code128 barcode and keep it in memory as a PNG bitmap.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set a small X-dimension to improve the barcode's visual quality.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            using (var pngStream = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0;

                // Load the PNG into a Bitmap object for further processing.
                using (var bitmap = new Bitmap(pngStream))
                {
                    // Define the JPEG quality levels that will be tested.
                    int[] qualities = new int[] { 100, 80, 60, 40, 20, 10 };

                    // Locate the JPEG encoder from the system's image codecs.
                    ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(),
                        c => c.FormatID == ImageFormat.Jpeg.Guid);
                    if (jpegCodec == null)
                    {
                        Console.WriteLine("JPEG codec not found.");
                        return;
                    }

                    // Encode the bitmap to JPEG files using each quality setting.
                    foreach (int q in qualities)
                    {
                        string jpegPath = Path.Combine(tempDir, $"barcode_q{q}.jpg");
                        using (var encoderParams = new EncoderParameters(1))
                        {
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)q);
                            bitmap.Save(jpegPath, jpegCodec, encoderParams);
                        }
                    }

                    // Read each JPEG file back, apply deconvolution, and report whether the barcode is readable.
                    foreach (int q in qualities)
                    {
                        string jpegPath = Path.Combine(tempDir, $"barcode_q{q}.jpg");
                        if (!File.Exists(jpegPath))
                        {
                            Console.WriteLine($"File missing: {jpegPath}");
                            continue;
                        }

                        using (var reader = new BarCodeReader(jpegPath, DecodeType.AllSupportedTypes))
                        {
                            // Configure the reader for high-quality processing and fast deconvolution.
                            reader.QualitySettings = QualitySettings.HighQuality;
                            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                            // Attempt to read barcodes from the image.
                            var results = reader.ReadBarCodes();
                            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                            Console.WriteLine($"JPEG Quality {q}: {(success ? "Success" : "Failure")}");
                        }
                    }
                }
            }
        }

        // Attempt to delete the temporary folder and its contents; ignore any errors.
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Cleanup errors are non‑critical for this example.
        }
    }
}