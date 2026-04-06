using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "code11.png");

        // Generate a Code 11 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode with checksum validation disabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Disable checksum verification for Code 11
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                // For Code 11, checksum information is available in the extended parameters
                Console.WriteLine("BarCode Value: " + result.Extended.OneD.Value);
                Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}