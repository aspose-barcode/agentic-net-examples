// Title: Configure deconvolution for blurred QR code recognition
// Description: Demonstrates how to set deconvolution and quality settings on Aspose.BarCode's BarCodeReader to improve detection of a blurred QR code stored in a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on image preprocessing and quality configuration. It shows usage of BarCodeReader, DecodeType, QualitySettings, DeconvolutionMode, BarcodeQualityMode, and XDimensionMode to handle low‑quality or blurred barcodes. Developers often need to adjust these settings when scanning images captured under poor lighting or motion blur to achieve reliable decoding.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr code, deconvolution, barcode recognition, quality settings, aspnet, aspose.barcode, jpeg

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates configuring deconvolution and other quality settings to read a blurred QR code from a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads a JPEG image, applies quality settings, and outputs detected barcode information.
    /// </summary>
    static void Main()
    {
        // Define the path to the sample JPEG file containing a blurred QR code
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "blurred_qr.jpg");

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for all supported barcode types (including QR)
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Configure quality settings to improve recognition of blurred images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Slow; // heavy deconvolution for blurred images
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low; // optimize for low‑quality barcodes

            // Optional: use minimal XDimension to help with small modules
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 5f;

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}