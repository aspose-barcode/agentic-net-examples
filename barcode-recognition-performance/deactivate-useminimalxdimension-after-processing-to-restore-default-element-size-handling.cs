using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define the barcode image file path
        string barcodePath = "barcode.png";

        // Create a Code128 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using UseMinimalXDimension mode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Activate UseMinimalXDimension for this read operation
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            Console.WriteLine("Reading with UseMinimalXDimension:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Deactivate UseMinimalXDimension to restore default handling (Auto)
            reader.QualitySettings.XDimension = XDimensionMode.Auto;
        }

        // Optional: read again with default XDimension to confirm restoration
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            Console.WriteLine("Reading with default XDimension (Auto):");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}