// Title: Configure Deconvolution for Blurred QR Code Recognition in JPEG Images
// Description: Demonstrates how to set deconvolution parameters on Aspose.BarCode's BarCodeReader to improve detection of blurred QR codes stored in JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on image preprocessing techniques such as deconvolution and inverse image handling. It showcases the use of BarCodeReader, DecodeType, and QualitySettings classes to enhance recognition accuracy for low‑quality or blurred images. Developers working with QR code scanning in challenging imaging conditions can refer to this pattern for configuring recognition settings.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr code, deconvolution, image preprocessing, barcode recognition, aspnet, aspose.barcode, jpeg, blurred images

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that configures deconvolution settings to improve QR code recognition
/// in blurred JPEG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads a JPEG image, configures deconvolution,
    /// and reads any QR codes found.
    /// </summary>
    static void Main()
    {
        // Build the full path to the JPEG image containing a blurred QR code.
        // Update the file name as needed for your environment.
        string imagePath = Path.Combine(Environment.CurrentDirectory, "blurred_qr.jpg");

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for QR codes using the specified image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Set deconvolution mode to Fast to help the reader handle blurred images.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Optional: enable inverse image detection if the QR code colors are inverted.
            // reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

            // Perform the barcode detection and retrieve all results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the detection results to the console.
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text : {result.CodeText}");
                    Console.WriteLine($"Code Type : {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality : {result.ReadingQuality}");
                    Console.WriteLine();
                }
            }
        }
    }
}