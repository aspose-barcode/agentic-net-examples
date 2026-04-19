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
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "pdf417_linked.png");

        // Create a PDF417 barcode with the linked flag set
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "ABCDE123456789012345678"))
        {
            // Enable linked mode (linkage flag 918)
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the barcode image
            generator.Save(filePath);
        }

        // Verify the file was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode and access extended parameters
        using (var reader = new BarCodeReader(filePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output whether the barcode is linked to another segment
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }
    }
}