// Title: Generate QR Code with fallback ECI encoding
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode, first attempting automatic encoding mode and falling back to ECI mode when auto selection fails.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation and encoding category. It showcases the use of BarcodeGenerator, QREncodeMode, ECIEncodings, and BarCodeReader to create QR codes that contain characters outside the default range, handle auto‑mode failures by switching to explicit ECI encoding, and optionally decode the generated barcode. Developers commonly need these patterns when working with international text, custom encoding requirements, or robust barcode creation workflows.
// Prompt: Generate a QR Code barcode and provide fallback encoding mode when auto selection fails.
// Tags: qr code, barcode generation, eci encoding, fallback mode, aspose.barcode, png output, barcode reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code, falls back to ECI encoding if auto mode fails,
/// and optionally reads back the generated barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the QR Code,
    /// applies fallback encoding when necessary, and reads the barcode to verify the content.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare the output directory where the barcode image will be saved
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample QR code text containing Greek letters, which may cause auto mode failure
        string codeText = "ΑΒΓΔΕ";

        // Full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "QrCode.png");

        // ------------------------------------------------------------
        // Attempt to generate the QR Code using the default (Auto) encoding mode
        // ------------------------------------------------------------
        bool generated = false;
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Auto mode is applied automatically by the generator
                generator.Save(outputPath, BarCodeImageFormat.Png);
                generated = true;
                Console.WriteLine($"QR Code generated with Auto mode: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auto mode generation failed: {ex.Message}");
        }

        // ------------------------------------------------------------
        // If Auto mode failed, fall back to explicit ECI encoding (UTF‑8)
        // ------------------------------------------------------------
        if (!generated)
        {
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                    generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"QR Code generated with ECI fallback mode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ECI fallback generation failed: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // Optional: read and display the decoded text from the generated barcode
        // ------------------------------------------------------------
        if (File.Exists(outputPath))
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.QR))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded text: {result.CodeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reading barcode failed: {ex.Message}");
            }
        }
    }
}