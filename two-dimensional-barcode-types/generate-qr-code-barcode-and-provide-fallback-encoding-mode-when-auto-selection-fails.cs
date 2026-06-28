using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code using Aspose.BarCode with automatic encoding,
/// and falls back to explicit ECI encoding if the automatic mode fails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, first attempting automatic encoding mode,
    /// then falling back to ECI (UTF-8) mode on failure.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string qrText = "https://example.com";

        // Define file paths for the generated images.
        string autoPath = "qr_auto.png";
        string fallbackPath = "qr_fallback.png";

        // Attempt to generate the QR code using the default (automatic) encoding mode.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Set encoding mode to Auto (default behavior).
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                // Save the generated QR code image to the specified path.
                generator.Save(autoPath);

                // Output the full path of the generated file.
                Console.WriteLine($"QR code generated with auto mode: {Path.GetFullPath(autoPath)}");
            }
        }
        catch (Exception ex)
        {
            // Log the failure of the automatic mode.
            Console.WriteLine($"Auto mode failed: {ex.Message}");

            // Attempt to generate the QR code using explicit ECI (UTF-8) encoding as a fallback.
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
                {
                    // Set encoding mode to ECI (Explicit Character Identification).
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;

                    // Specify UTF-8 as the character encoding.
                    generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                    // Save the fallback QR code image.
                    generator.Save(fallbackPath);

                    // Output the full path of the fallback file.
                    Console.WriteLine($"QR code generated with fallback ECI mode: {Path.GetFullPath(fallbackPath)}");
                }
            }
            catch (Exception fallbackEx)
            {
                // Log the failure of the fallback mode.
                Console.WriteLine($"Fallback mode also failed: {fallbackEx.Message}");
            }
        }
    }
}