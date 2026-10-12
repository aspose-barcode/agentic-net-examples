// Title: Deconvolution Settings for Recognizing Blurred QR Codes in JPEG Images
// Description: Demonstrates how to configure deconvolution and high‑quality settings to improve detection of blurred QR codes stored as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on image preprocessing options such as QualitySettings and DeconvolutionMode. It shows how to use BarCodeReader with DecodeType.QR, adjust QualitySettings.HighQuality, and set Deconvolution to Fast to enhance recognition of low‑quality or blurred images. Developers working with QR code scanning in challenging imaging conditions can reference this pattern for better results.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr code, deconvolution, qualitysettings, barcode recognition, aspose.barcode, jpeg

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates configuring deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a JPEG image, applies high‑quality and fast deconvolution settings, and outputs detected QR code information.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing a blurred QR code
        string imagePath = "blurred_qr.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Specify that we are interested in decoding QR codes
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize the barcode reader with the image path and decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply quality settings suitable for blurred QR codes
            reader.QualitySettings = QualitySettings.HighQuality;
            // Use fast deconvolution to enhance image clarity for better recognition
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            try
            {
                // Attempt to read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    // Output details for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during barcode reading
                Console.WriteLine($"Error during barcode reading: {ex.Message}");
            }
        }
    }
}