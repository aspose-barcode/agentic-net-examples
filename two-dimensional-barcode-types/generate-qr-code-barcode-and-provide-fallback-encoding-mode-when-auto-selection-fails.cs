// Title: Generate QR Code with fallback encoding modes
// Description: Demonstrates creating a QR Code barcode, first using automatic encoding selection, then falling back to binary or ECI modes when auto selection fails.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to work with QR Code symbology using the BarcodeGenerator class. It shows typical use cases such as setting X-dimension, handling encoding mode selection, and providing fallback strategies for characters that may not be supported by automatic mode. Developers often need these patterns when generating QR codes with mixed-language content or when ensuring compatibility across different scanning devices.
// Prompt: Generate a QR Code barcode and provide fallback encoding mode when auto selection fails.
// Tags: qr code, fallback encoding, auto mode, binary mode, eci mode, aspose.barcode, c#, barcode generation, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and demonstrates fallback encoding strategies
/// when automatic mode selection cannot encode the provided text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes using auto mode and, if needed,
    /// binary or ECI fallback modes, saving the results as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the auto-generated and fallback QR code images
        string autoPath = Path.Combine(outputDir, "qr_auto.png");
        string fallbackPath = Path.Combine(outputDir, "qr_fallback.png");

        // Text containing mixed characters that may require special encoding
        string codeText = "Sample文字";

        bool autoSucceeded = false;

        // Attempt to generate QR code using automatic encoding selection (default mode)
        try
        {
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set the size of a single QR module (pixel dimension)
                gen.Parameters.Barcode.XDimension.Pixels = 4f;
                // Auto mode is default; no need to set EncodeMode explicitly
                gen.Save(autoPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR generated with auto mode saved to: {autoPath}");
                autoSucceeded = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auto mode generation failed: {ex.Message}");
        }

        // If auto mode failed, try binary encoding as a fallback
        if (!autoSucceeded)
        {
            try
            {
                using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR))
                {
                    gen.Parameters.Barcode.XDimension.Pixels = 4f;
                    // Convert the text to a UTF-8 byte array for binary mode
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(codeText);
                    gen.SetCodeText(data);
                    gen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
                    gen.Save(fallbackPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"QR generated with binary fallback saved to: {fallbackPath}");
                }
            }
            catch (Exception exBinary)
            {
                Console.WriteLine($"Binary fallback failed: {exBinary.Message}");
                // If binary fallback also fails, try ECI (Extended Channel Interpretation) mode
                try
                {
                    using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, codeText))
                    {
                        gen.Parameters.Barcode.XDimension.Pixels = 4f;
                        gen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                        gen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                        gen.Save(fallbackPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"QR generated with ECI fallback saved to: {fallbackPath}");
                    }
                }
                catch (Exception exEci)
                {
                    Console.WriteLine($"ECI fallback also failed: {exEci.Message}");
                }
            }
        }
    }
}