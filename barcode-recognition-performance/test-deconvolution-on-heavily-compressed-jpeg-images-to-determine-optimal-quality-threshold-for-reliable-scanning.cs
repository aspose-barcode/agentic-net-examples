// Title: Determine optimal JPEG quality for QR code scanning with deconvolution
// Description: Generates a QR code, recompresses it into JPEG images at multiple quality levels, and evaluates scanning success using Aspose.BarCode deconvolution.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to create barcodes with BarcodeGenerator, recompress images, and read them with BarCodeReader while applying QualitySettings such as DeconvolutionMode and XDimensionMode. Developers often use these APIs to assess image quality thresholds, improve scan reliability on compressed media, and fine‑tune barcode processing pipelines.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: barcode, qr, deconvolution, jpeg, quality, recognition, generation, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to evaluate the impact of JPEG compression quality on QR code readability
/// by applying deconvolution and minimal X‑dimension settings during barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, creates JPEG variants at different
    /// quality levels, and reports whether each variant can be successfully decoded.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDeconvTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the original PNG image that will serve as the source for JPEG recompression
        string basePngPath = Path.Combine(tempFolder, "base.png");

        // Generate a sample QR code and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(basePngPath, BarCodeImageFormat.Png);
        }

        // Load the PNG into a bitmap so it can be re‑encoded as JPEG with varying quality settings
        using (var bitmap = new Bitmap(basePngPath))
        {
            // Define the JPEG quality levels to be tested
            int[] qualities = new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 };

            // Locate the JPEG codec once to avoid repeated look‑ups
            ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(),
                c => c.FormatID == ImageFormat.Jpeg.Guid);
            if (jpegCodec == null)
            {
                Console.WriteLine("JPEG codec not found.");
                return;
            }

            // Iterate over each quality level, create a JPEG, and attempt to read the barcode
            foreach (int quality in qualities)
            {
                string jpegPath = Path.Combine(tempFolder, $"qr_{quality}.jpg");

                // Re‑encode the bitmap as JPEG using the current quality setting
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
                    bitmap.Save(jpegPath, jpegCodec, encoderParams);
                }

                // Initialize a barcode reader for the newly created JPEG file
                using (var reader = new BarCodeReader(jpegPath, DecodeType.AllSupportedTypes))
                {
                    // Enable fast deconvolution and minimal X‑dimension to improve detection on low‑quality images
                    reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                    // Perform the recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Determine success based on the presence of a decoded result
                    bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                    Console.WriteLine($"Quality {quality}: {(success ? "Success" : "Failure")}");
                }
            }
        }

        // Cleanup: attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails (e.g., files still in use), ignore – the OS will clean up temp files later.
        }
    }
}