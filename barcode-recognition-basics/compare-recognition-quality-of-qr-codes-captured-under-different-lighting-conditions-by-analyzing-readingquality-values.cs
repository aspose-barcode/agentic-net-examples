// Title: QR Code ReadingQuality Comparison Under Varying Lighting
// Description: Generates QR codes with different background shades to simulate lighting conditions and compares their ReadingQuality values after recognition.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr, readingquality, lighting, barcode, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how lighting (background color) affects QR code recognition quality
/// by generating three QR images and reporting their <c>ReadingQuality</c> values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes with normal, bright, and dark backgrounds,
    /// then reads each image and prints the ReadingQuality metric.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        const string qrText = "Lighting Test QR";

        // Create three memory streams – one for each simulated lighting condition
        using (var streamNormal = new MemoryStream())
        using (var streamBright = new MemoryStream())
        using (var streamDark = new MemoryStream())
        {
            // ---------- Generate QR code with normal lighting (white background) ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                generator.Parameters.BackColor = Color.White; // normal background
                generator.Save(streamNormal, BarCodeImageFormat.Png);
            }

            // ---------- Generate QR code with bright lighting (light gray background) ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                generator.Parameters.BackColor = Color.LightGray; // brighter appearance
                generator.Save(streamBright, BarCodeImageFormat.Png);
            }

            // ---------- Generate QR code with dark lighting (dark gray background) ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                generator.Parameters.BackColor = Color.DarkGray; // darker appearance
                generator.Save(streamDark, BarCodeImageFormat.Png);
            }

            // Reset stream positions so the reader starts from the beginning of each image
            streamNormal.Position = 0;
            streamBright.Position = 0;
            streamDark.Position = 0;

            // ---------- Read and compare ReadingQuality values ----------
            Console.WriteLine("ReadingQuality comparison for QR codes under different lighting conditions:");
            ReadAndReport(streamNormal, "Normal Lighting (White BG)");
            ReadAndReport(streamBright, "Bright Lighting (LightGray BG)");
            ReadAndReport(streamDark, "Dark Lighting (DarkGray BG)");
        }
    }

    /// <summary>
    /// Reads a QR code from the provided image stream and writes its ReadingQuality to the console.
    /// </summary>
    /// <param name="imageStream">Stream containing the QR code image.</param>
    /// <param name="description">Human‑readable description of the lighting condition.</param>
    static void ReadAndReport(Stream imageStream, string description)
    {
        // Initialize the reader for QR codes
        using (var reader = new BarCodeReader(imageStream, DecodeType.QR))
        {
            // Iterate through all detected barcodes (normally just one)
            foreach (var result in reader.ReadBarCodes())
            {
                // ReadingQuality is a double representing the confidence percentage
                Console.WriteLine($"{description}: ReadingQuality = {result.ReadingQuality}%");
            }
        }
    }
}