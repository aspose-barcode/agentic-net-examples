// Title: Evaluate JPEG compression impact on Code128 barcode readability using deconvolution
// Description: Demonstrates generating a Code128 barcode, saving it as JPEG at various compression levels, and scanning each image with deconvolution to find the quality threshold for reliable decoding.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to use BarcodeGenerator for barcode creation, ImageCodecInfo for JPEG compression, and BarCodeReader with QualitySettings for deconvolution-based recognition. Developers often need to test how image quality affects scan reliability, especially when working with compressed formats in mobile or web applications.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: code128, deconvolution, jpeg, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, JPEG compression at multiple quality levels,
/// and barcode reading with deconvolution to assess scanning reliability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, creates JPEG variants,
    /// and attempts to read each variant using high‑quality deconvolution settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated JPEG files.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDeconvolutionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the JPEG quality levels that will be tested.
        int[] qualities = new int[] { 100, 80, 60, 40, 20 };

        // Generate a sample Code128 barcode image.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set barcode foreground and background colors (optional).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            using (Image barcodeImage = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG encoder from the system's image codecs.
                ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(),
                    c => c.FormatID == ImageFormat.Jpeg.Guid);
                if (jpegCodec == null)
                {
                    Console.WriteLine("JPEG codec not found.");
                    return;
                }

                // Iterate over each quality level, save the JPEG, and attempt to read it.
                foreach (int quality in qualities)
                {
                    string jpegPath = Path.Combine(tempDir, $"barcode_q{quality}.jpg");

                    // Save the barcode image as JPEG with the current quality setting.
                    using (EncoderParameters encoderParams = new EncoderParameters(1))
                    {
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
                        barcodeImage.Save(jpegPath, jpegCodec, encoderParams);
                    }

                    // Verify that the JPEG file was created successfully.
                    if (!File.Exists(jpegPath))
                    {
                        Console.WriteLine($"Failed to create JPEG at quality {quality}.");
                        continue;
                    }

                    // Read the barcode using deconvolution with high‑quality settings.
                    using (BarCodeReader reader = new BarCodeReader(jpegPath, DecodeType.AllSupportedTypes))
                    {
                        reader.QualitySettings = QualitySettings.HighQuality;
                        reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                        BarCodeResult[] results = reader.ReadBarCodes();
                        bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                        Console.WriteLine($"JPEG Quality {quality}: {(success ? "Success" : "Failure")}");
                    }
                }
            }
        }

        // Attempt to clean up the temporary directory and its contents.
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the example flow.
        }
    }
}