// Title: Barcode generation with Interpolation mode at 150 DPI and readability verification
// Description: Demonstrates generating a Code128 barcode using Aspose.BarCode with Interpolation auto‑size mode at 150 dpi, then reads it back to verify readability.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them). Typical scenarios include testing image resolution, auto‑size settings, and ensuring that generated barcodes meet scanning requirements. Developers often need to experiment with DPI and interpolation settings to balance image size and scan reliability, making this pattern useful for quality‑control automation.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: barcode symbology, generation, recognition, interpolation, dpi, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode using Interpolation auto‑size mode at 150 dpi,
/// then attempts to read it back to verify that the image is still scannable.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, reports readability, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "Interpolation150dpi.png");

        // Generate barcode with Interpolation mode at 150 dpi
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "TEST123"))
        {
            // Set auto‑size mode to Interpolation to let Aspose adjust dimensions based on DPI
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            // Define image resolution
            generator.Parameters.Resolution = 150f;
            // Set explicit image dimensions (pixels)
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;
            // Define X‑dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            // Save the generated barcode as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Attempt to read the generated barcode
        bool readable = false;
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            try
            {
                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        readable = true;
                        Console.WriteLine($"Read barcode: {result.CodeText} (Type: {result.CodeTypeName})");
                        break; // Stop after first successful read
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine("Failed to load barcode image: " + ex.Message);
            }
            catch (BarCodeException ex)
            {
                Console.WriteLine("Barcode processing error: " + ex.Message);
            }
        }

        // Report the result of the readability test
        if (readable)
        {
            Console.WriteLine("Barcode generated with Interpolation mode at 150 dpi is readable.");
        }
        else
        {
            Console.WriteLine("Barcode not readable at 150 dpi with Interpolation mode. Consider using a higher DPI.");
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}