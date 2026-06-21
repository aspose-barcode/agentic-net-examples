using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and reading with different deconvolution modes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample barcode image, then reads barcodes from JPEG files
    /// using various deconvolution settings.
    /// </summary>
    static void Main()
    {
        // Determine the folder that will hold test JPEG images.
        string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        if (!Directory.Exists(imagesFolder))
        {
            // Create the folder if it does not exist.
            Directory.CreateDirectory(imagesFolder);
        }

        // Path for a sample JPEG image that will be generated if missing.
        string sampleImagePath = Path.Combine(imagesFolder, "sample.jpg");
        if (!File.Exists(sampleImagePath))
        {
            // Generate a simple Code128 barcode and save it as a JPEG (default quality).
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Save(sampleImagePath, BarCodeImageFormat.Jpeg);
            }
        }

        // Retrieve all JPEG files in the folder (limit to 5 for safety).
        string[] jpegFiles = Directory.GetFiles(imagesFolder, "*.jpg");
        int maxFiles = Math.Min(jpegFiles.Length, 5);
        if (maxFiles == 0)
        {
            Console.WriteLine("No JPEG images found for testing.");
            return;
        }

        // Define the deconvolution modes that will be evaluated.
        DeconvolutionMode[] modes = new DeconvolutionMode[]
        {
            DeconvolutionMode.Fast,
            DeconvolutionMode.Normal,
            DeconvolutionMode.Slow
        };

        // Process each selected image with each deconvolution mode.
        for (int i = 0; i < maxFiles; i++)
        {
            string imagePath = jpegFiles[i];
            Console.WriteLine($"--- Processing Image: {Path.GetFileName(imagePath)} ---");

            foreach (var mode in modes)
            {
                // Initialize a barcode reader for the current image.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current deconvolution setting.
                    reader.QualitySettings.Deconvolution = mode;

                    // Read all barcodes from the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    Console.WriteLine($"Deconvolution Mode: {mode}");
                    if (results.Length == 0)
                    {
                        Console.WriteLine("  No barcodes detected.");
                    }
                    else
                    {
                        // Output details for each detected barcode.
                        foreach (var result in results)
                        {
                            // ReadingQuality is a double representing detection confidence.
                            double quality = result.ReadingQuality;
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}, ReadingQuality: {quality:F2}");
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("Deconvolution testing completed.");
    }
}