using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the temporary barcode image
        const string barcodePath = "macroPdf417.png";

        // Create a MacroPdf417 barcode with sample macro data
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
        {
            // Set macro fields: File ID and Segment ID
            generator.Parameters.Barcode.Pdf417.Pdf417MacroFileID = 10;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentID = 1;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentsCount = 2;

            // Save the barcode image
            generator.Save(barcodePath);
        }

        // Read the barcode and extract macro information
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MacroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);

                // Access macro fields via the Extended property
                var macroInfo = result.Extended.Pdf417;
                Console.WriteLine("Macro Pdf417 File ID: " + macroInfo.MacroPdf417FileID);
                Console.WriteLine("Macro Pdf417 Segment ID: " + macroInfo.MacroPdf417SegmentID);
                Console.WriteLine("Macro Pdf417 Segments Count: " + macroInfo.MacroPdf417SegmentsCount);
            }
        }

        // Clean up the temporary image file
        if (System.IO.File.Exists(barcodePath))
        {
            System.IO.File.Delete(barcodePath);
        }
    }
}