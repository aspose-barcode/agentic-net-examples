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
        const string imagePath = "code39.png";

        // Create a Code39 barcode with sample text and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Save the barcode image
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode image with checksum validation enabled
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable checksum validation for optional symbologies like Code39
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                // For 1D barcodes, extended data provides value and checksum
                if (result.Extended != null && result.Extended.OneD != null)
                {
                    Console.WriteLine("BarCode Value: " + result.Extended.OneD.Value);
                    Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
                }
                Console.WriteLine();
            }
        }
    }
}