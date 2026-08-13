// Title: Configure deconvolution for blurred QR code recognition
// Description: Demonstrates generating a high‑error‑correction QR code, saving it as JPEG, and using Aspose.BarCode's deconvolution settings to improve detection of blurred images.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, QualitySettings, and DeconvolutionMode to handle low‑quality or blurred barcode images. Developers often need to adjust these settings when scanning QR codes from photographs or scanned documents where blur is present.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr code, deconvolution, image preprocessing, barcode recognition, aspose.barcode, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, saves it as JPEG, and reads it using high‑quality deconvolution settings.
    /// </summary>
    static void Main()
    {
        // Path for the sample QR code image
        string qrImagePath = "qr_sample.jpg";

        // -------------------------------------------------
        // 1. Generate a QR code with high error correction level
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Use the highest error correction to tolerate blur
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save as JPEG (Aspose.BarCode supports JPEG via BarCodeImageFormat)
            generator.Save(qrImagePath, BarCodeImageFormat.Jpeg);
        }

        // -------------------------------------------------
        // 2. Verify the image exists before attempting recognition
        // -------------------------------------------------
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine($"File not found: {qrImagePath}");
            return;
        }

        // -------------------------------------------------
        // 3. Read the QR code using deconvolution to improve
        //    recognition of blurred images
        // -------------------------------------------------
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            // Apply a high‑quality preset for better detection
            reader.QualitySettings = QualitySettings.HighQuality;

            // Enable deconvolution (image restoration) – Fast mode is sufficient
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Allow recognition of barcodes with minor errors (optional)
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform recognition and output detected text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected QR Code Text: {result.CodeText}");
            }
        }
    }
}