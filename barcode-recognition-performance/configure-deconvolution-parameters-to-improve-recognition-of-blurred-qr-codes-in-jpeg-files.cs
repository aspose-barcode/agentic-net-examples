using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the temporary QR code image
        string qrImagePath = "qr.jpg";

        // -----------------------------------------------------------------
        // Step 1: Generate a QR code and save it as a JPEG image.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: increase image size for better visibility
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save as JPEG
            generator.Save(qrImagePath, BarCodeImageFormat.Jpeg);
        }

        // -----------------------------------------------------------------
        // Step 2: Read the JPEG image with deconvolution enabled to improve
        //         recognition of blurred QR codes.
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            // Use a high-quality preset as a base
            reader.QualitySettings = QualitySettings.HighQuality;

            // Enable deconvolution (slow mode gives the strongest restoration)
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Slow;

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected QR Code Text: " + result.CodeText);
            }
        }

        // Clean up the temporary image file
        if (File.Exists(qrImagePath))
        {
            File.Delete(qrImagePath);
        }
    }
}