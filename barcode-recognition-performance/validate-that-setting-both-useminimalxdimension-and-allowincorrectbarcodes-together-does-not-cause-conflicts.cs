// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode, then reading it while configuring QualitySettings to use minimal X dimension and allow incorrect barcodes, confirming no conflicts.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the interaction between the BarcodeGenerator, BarCodeReader, and QualitySettings classes, which developers commonly use to create barcodes, customize decoding parameters, and handle imperfect scans. Typical use cases include validating decoding options, optimizing scan performance, and ensuring tolerant reading of barcodes in real‑world applications.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: code128, barcode generation, barcode recognition, qualitysettings, useminimalxdimension, allowincorrectbarcodes, bitmap

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it with specific QualitySettings
/// to verify that UseMinimalXDimension and AllowIncorrectBarcodes can be used together without conflict.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image in memory, configures the reader,
    /// and attempts to decode the barcode while reporting the outcome.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a Bitmap object (in memory)
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for the generated image, specifying the expected decode type
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Configure QualitySettings:
                    // - UseMinimalXDimension: let the reader choose the smallest possible X dimension
                    // - AllowIncorrectBarcodes: enable tolerant reading of imperfect barcodes
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    try
                    {
                        // Attempt to read all barcodes from the image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Check if any barcodes were detected
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected.");
                        }
                        else
                        {
                            // Output details of each detected barcode
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Detected Type: {result.CodeType}");
                                Console.WriteLine($"CodeText: {result.CodeText}");
                            }

                            // Indicate successful reading without conflicts
                            Console.WriteLine("Reading succeeded without conflicts.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Report any errors that occurred during reading (including potential conflicts)
                        Console.WriteLine($"Error during barcode reading: {ex.Message}");
                    }
                }
            }
        }
    }
}