using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a barcode image and save it to a file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            generator.Save("barcode.png");
        }

        // First read with UseMinimalXDimension mode.
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            // Activate the UseMinimalXDimension mode.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            Console.WriteLine("Reading with UseMinimalXDimension:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
            }

            // Deactivate UseMinimalXDimension to restore default handling.
            reader.QualitySettings.XDimension = XDimensionMode.Normal;

            Console.WriteLine("\nReading after deactivating UseMinimalXDimension:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}