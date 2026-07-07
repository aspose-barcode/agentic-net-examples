// Title: Decode MaxiCode with checksum errors ignored
// Description: Demonstrates configuring BarCodeReader to ignore checksum validation while decoding MaxiCode barcodes in noisy images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on error‑tolerant decoding. It shows how to use BarCodeReader, QualitySettings, and BarcodeSettings to handle damaged or high‑noise MaxiCode symbols, a common requirement for logistics and shipping applications where barcode integrity may be compromised.
// Prompt: Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a high‑noise environment.
// Tags: maxicode, checksum, ignore, barcodereader, qualitysettings, barcodesettings, decoding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates configuring the BarCodeReader to ignore checksum errors when decoding MaxiCode barcodes,
/// useful in high‑noise environments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode image, then reads it while allowing incorrect checksums.
    /// </summary>
    static void Main()
    {
        // Create a simple MaxiCode codetext (Mode4 with a short message)
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Test"
        };

        // Generate the MaxiCode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Generate bitmap representation of the barcode
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Initialize BarCodeReader for MaxiCode with high-quality settings
                    using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
                    {
                        // Allow recognition of barcodes with incorrect checksum or damaged data
                        reader.QualitySettings.AllowIncorrectBarcodes = true;

                        // Disable checksum validation (reinforces ignoring checksum errors)
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                        // Read barcodes from the image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine("No MaxiCode barcode detected.");
                        }
                        else
                        {
                            // Output details of each detected barcode
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Code Text: {result.CodeText}");
                                Console.WriteLine($"Confidence: {result.Confidence}");
                                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                            }
                        }
                    }
                }
            }
        }
    }
}