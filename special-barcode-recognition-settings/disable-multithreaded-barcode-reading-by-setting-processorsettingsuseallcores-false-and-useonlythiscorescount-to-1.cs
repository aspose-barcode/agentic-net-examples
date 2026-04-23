using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Disable multithreaded barcode reading
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

        string imagePath = "barcode.png";

        // Generate a simple Code128 barcode and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the saved image using the configured processor settings
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
    }
}