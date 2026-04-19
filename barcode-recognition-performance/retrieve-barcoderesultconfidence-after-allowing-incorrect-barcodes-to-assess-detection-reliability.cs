using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file name for the generated barcode image
        string imagePath = "qr.png";

        // Create a QR barcode with sample text and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the reader for QR codes
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Allow recognition of incorrect barcodes (even though this QR is correct)
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Optionally use a preset that enables maximum detection capabilities
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Read all barcodes from the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                Console.WriteLine("BarCode Confidence: " + result.Confidence);
                Console.WriteLine("BarCode ReadingQuality: " + result.ReadingQuality);
            }
        }
    }
}