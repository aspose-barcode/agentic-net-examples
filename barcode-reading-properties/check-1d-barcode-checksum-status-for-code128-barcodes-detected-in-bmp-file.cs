using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the BMP image containing Code128 barcodes
        string imagePath = "barcode.bmp";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Code128 barcode:");
                Console.WriteLine($"  CodeText : {result.CodeText}");

                // Retrieve the checksum from the extended 1D parameters
                string checksum = result.Extended.OneD.CheckSum;
                Console.WriteLine($"  Checksum : {(string.IsNullOrEmpty(checksum) ? "N/A" : checksum)}");
            }
        }
    }
}