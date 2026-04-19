using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the JPEG image containing a blurred QR code
        string imagePath = "blurred_qr.jpg";

        // Verify that the file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader for QR codes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Use a high‑quality preset suitable for low‑quality images
            reader.QualitySettings = QualitySettings.HighQuality;

            // Configure deconvolution to the most thorough mode to help restore blurred images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Slow;

            // Optional: enable recognition of inverted images if needed
            // reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

            // Perform recognition and output results
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
            }
        }
    }
}