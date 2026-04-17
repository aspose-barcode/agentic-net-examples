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
        const string barcodePath = "barcode.png";

        // Generate a simple Code128 barcode and save it to a file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the saved image.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                double quality = result.ReadingQuality;
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode ReadingQuality: {quality}");

                if (quality == 0.0)
                {
                    Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                }
                else
                {
                    Console.WriteLine("Reading quality is acceptable.");
                }
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Clean up the generated image file (optional).
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignored – file may be in use or deletion may fail.
        }
    }
}