using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare output directory and file path
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string imagePath = Path.Combine(outputFolder, "code11.png");

        // Generate a Code11 barcode and save it
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            generator.Save(imagePath);
        }

        // Verify the file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found: " + imagePath);
            return;
        }

        // Read the barcode with checksum validation disabled
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}