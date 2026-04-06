using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";
        const string codeText = "1234567890";

        // Create a barcode with a specific XDimension (2 points)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = codeText;
            generator.Parameters.Barcode.XDimension.Point = 2f; // set XDimension
            generator.Save(filePath);
        }

        // Attempt to read the barcode with MinimalXDimension higher than actual XDimension
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Configure recognition to use MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 5f; // higher than the barcode's XDimension

            Console.WriteLine("Reading with MinimalXDimension = 5 (higher than barcode XDimension):");
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeText}");
                found = true;
            }
            if (!found)
                Console.WriteLine("No barcode detected.");
        }

        // Attempt to read the same barcode with MinimalXDimension lower than actual XDimension
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Configure recognition to use MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f; // lower than the barcode's XDimension

            Console.WriteLine("\nReading with MinimalXDimension = 1 (lower than barcode XDimension):");
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeText}");
                found = true;
            }
            if (!found)
                Console.WriteLine("No barcode detected.");
        }
    }
}