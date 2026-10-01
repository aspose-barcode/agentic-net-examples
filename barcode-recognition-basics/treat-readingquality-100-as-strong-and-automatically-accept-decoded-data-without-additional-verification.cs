// Title: QR Code Generation and Recognition with ReadingQuality Evaluation
// Description: Demonstrates generating a QR code, saving it as PNG, then reading it back while evaluating the ReadingQuality metric to automatically accept strong results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include creating QR codes for data exchange and validating scan quality in applications such as inventory, ticketing, or mobile payments. Developers often need to assess ReadingQuality to decide whether additional verification is required.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr code,generation,recognition,readingquality,aspose.barcode,barcodeimageformat,png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, saves it as a PNG image,
/// reads the barcode back, and evaluates the ReadingQuality of the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, decodes it, and prints the result,
    /// treating a ReadingQuality of 100 as a strong, automatically accepted read.
    /// </summary>
    static void Main()
    {
        // Sample data to encode into the QR code
        const string sampleText = "Aspose.BarCode Sample";

        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR code and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            // Optional: set high quality settings for generation if desired
            generator.Parameters.Barcode.XDimension.Point = 2.5f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode image using the recognition engine
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Explicitly set the image source as required by the rules
            reader.SetBarCodeImage(barcodePath);

            // Perform the read operation and obtain all detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Iterate through each detected barcode result
                foreach (BarCodeResult result in results)
                {
                    double quality = result.ReadingQuality; // 0‑100 scale

                    // Treat quality 100 as strong and accept automatically
                    if (quality == 100.0)
                    {
                        Console.WriteLine($"[Strong] CodeText: {result.CodeText}");
                    }
                    else
                    {
                        Console.WriteLine($"[Quality {quality}] CodeText: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}