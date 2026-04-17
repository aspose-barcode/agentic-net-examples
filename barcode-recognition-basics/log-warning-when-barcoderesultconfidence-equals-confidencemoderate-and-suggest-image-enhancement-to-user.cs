using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define the path for the generated barcode image
        string imagePath = "barcode.png";

        // Create a barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        // Read the barcode from the image and evaluate confidence
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Barcode confidence is moderate. Consider enhancing the image for better recognition.");
                }
                else
                {
                    Console.WriteLine($"Barcode confidence: {result.Confidence}");
                }
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }
    }
}