using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the Code 11 barcode image
        string imagePath = "code11.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for Code 11 symbology
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enable checksum validation
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read and process all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                Console.WriteLine("BarCode Value: " + result.Extended.OneD.Value);
                Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}