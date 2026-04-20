using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the BMP image containing the Swiss Post Parcel barcode
        string imagePath = "SwissPostParcel.bmp";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // First read the barcode with checksum validation disabled (to see raw data)
        using (var readerNoChecksum = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            readerNoChecksum.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
            Console.WriteLine("Reading with checksum validation OFF:");
            foreach (BarCodeResult result in readerNoChecksum.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                // For 1D barcodes the checksum is available in the extended parameters
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"  Extracted Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"  Extracted CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Now read the barcode with checksum validation enabled
        using (var readerWithChecksum = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            readerWithChecksum.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            Console.WriteLine("\nReading with checksum validation ON:");
            foreach (BarCodeResult result in readerWithChecksum.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"  Extracted Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"  Extracted CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}