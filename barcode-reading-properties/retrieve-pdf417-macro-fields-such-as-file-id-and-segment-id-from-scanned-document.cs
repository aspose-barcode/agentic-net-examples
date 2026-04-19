using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the scanned image that contains a Macro PDF417 barcode.
        string imagePath = "sample.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for MacroPdf417 decoding.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.MacroPdf417))
        {
            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access the extended MacroPdf417 parameters.
                Pdf417ExtendedParameters macroInfo = result.Extended.Pdf417;

                // Output the macro fields.
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"Macro Pdf417 FileID: {macroInfo.MacroPdf417FileID}");
                Console.WriteLine($"Macro Pdf417 SegmentID: {macroInfo.MacroPdf417SegmentID}");
            }
        }
    }
}