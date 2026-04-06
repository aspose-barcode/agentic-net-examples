using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a Code128 barcode and save it to a file
        string filePath = "barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(filePath);
        }

        // Read the barcode from the saved image
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                Console.WriteLine("BarCode Confidence: " + result.Confidence);
                Console.WriteLine("BarCode ReadingQuality: " + result.ReadingQuality);

                // Log a warning if confidence is moderate
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Moderate confidence detected. Consider enhancing the image for better recognition.");
                }
            }
        }
    }
}