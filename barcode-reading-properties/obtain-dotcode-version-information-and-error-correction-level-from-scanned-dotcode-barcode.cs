using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing the DotCode barcode.
        const string imagePath = "dotcode.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file '{imagePath}' not found.");
            return;
        }

        // Create a BarCodeReader for DotCode symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            bool found = false;

            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Code text: {result.CodeText}");

                // Access DotCode specific extended parameters, if present.
                var dotParams = result.Extended?.DotCode;
                if (dotParams != null)
                {
                    Console.WriteLine($"Structured Append Mode Barcode ID: {dotParams.StructuredAppendModeBarcodeId}");
                    Console.WriteLine($"Structured Append Mode Barcodes Count: {dotParams.StructuredAppendModeBarcodesCount}");
                    Console.WriteLine($"Is Reader Initialization: {dotParams.IsReaderInitialization}");
                }
                else
                {
                    Console.WriteLine("No DotCode extended parameters available.");
                }

                // DotCode does not expose an error‑correction level; indicate that.
                Console.WriteLine("Error correction level: N/A (DotCode does not provide this information)");
                Console.WriteLine();
            }

            if (!found)
            {
                Console.WriteLine("No DotCode barcodes detected in the image.");
            }
        }
    }
}