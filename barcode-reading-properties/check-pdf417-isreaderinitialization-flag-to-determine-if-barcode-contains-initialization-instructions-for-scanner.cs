using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define output file path
        string filePath = "pdf417.png";

        // Create a PDF417 barcode with IsReaderInitialization flag set to true
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "INIT_DATA"))
        {
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;
            generator.Save(filePath);
        }

        // Verify that the file was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and check the IsReaderInitialization flag
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                bool isInit = result.Extended.Pdf417.IsReaderInitialization;
                Console.WriteLine($"IsReaderInitialization flag: {isInit}");
            }
        }
    }
}