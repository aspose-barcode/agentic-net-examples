using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Adjust ThreadPool settings
        // Set minimum worker threads to 2, completion port threads to 0
        ThreadPool.SetMinThreads(2, 0);
        // Set maximum worker threads to 8, completion port threads to 0
        ThreadPool.SetMaxThreads(8, 0);

        // Generate a sample barcode image (Code128)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Generate the barcode as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Read the barcode from the generated bitmap
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.Code128))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}