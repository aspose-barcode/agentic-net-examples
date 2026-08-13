// Title: Decode MaxiCode with error handling using Aspose.BarCode
// Description: Demonstrates how to decode a MaxiCode barcode from an image while handling potential decoding errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, DecodeType, and QualitySettings to read MaxiCode symbols. Typical scenarios include processing shipping labels or inventory tags where MaxiCode is common, and developers often need robust error handling for damaged or unreadable images.
// Prompt: Implement error handling that catches BarcodeException when decoding an unreadable MaxiCode image.
// Tags: maxicode, barcode decoding, error handling, aspose.barcode, barcodereader, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition; // Provides BarCodeReader, DecodeType, QualitySettings
using Aspose.BarCode.Generation;        // Provides DecodeType enum (static members)

/// <summary>
/// Example program that attempts to read a MaxiCode barcode from an image file
/// and demonstrates error handling for unreadable or damaged barcodes.
/// </summary>
class MaxiCodeDecoder
{
    /// <summary>
    /// Entry point of the program. Reads the specified image, configures the reader,
    /// and outputs any detected MaxiCode values while safely handling decoding exceptions.
    /// </summary>
    static void Main()
    {
        // Path to the image that may contain an unreadable MaxiCode
        string imagePath = "unreadable_maxicode.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // DecodeType is a static class; store the specific type in a BaseDecodeType variable
        BaseDecodeType decodeType = DecodeType.MaxiCode;

        // Create the reader and assign the image source
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Use the highest quality preset to improve chances of reading a damaged barcode
            reader.QualitySettings = QualitySettings.MaxQuality;

            try
            {
                // Attempt to read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No MaxiCode detected in the image.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected MaxiCode: {result.CodeText}");
                        // Additional extended parameters can be accessed if needed, e.g.:
                        // var mode = result.Extended.MaxiCode.Mode;
                    }
                }
            }
            // Catch any exception that occurs during decoding (e.g., unreadable image)
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding MaxiCode: {ex.Message}");
            }
        }
    }
}