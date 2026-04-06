using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a sample barcode image (Code128) and save it to a file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save("sample.png");
        }

        // Read barcodes from the saved image.
        using (var reader = new BarCodeReader("sample.png", DecodeType.Code128))
        {
            // Perform recognition.
            reader.ReadBarCodes();

            // Collection to store unique barcode texts.
            var uniqueBarcodes = new HashSet<string>();

            // Iterate through all found barcodes.
            for (int i = 0; i < reader.FoundCount; i++)
            {
                var result = reader.FoundBarCodes[i];
                // Add the code text to the set of unique values.
                uniqueBarcodes.Add(result.CodeText);

                // Display barcode information and its region (position).
                Console.WriteLine($"Barcode #{i + 1}:");
                Console.WriteLine($"  CodeText : {result.CodeText}");
                Console.WriteLine($"  Type     : {result.CodeTypeName}");
                Console.WriteLine($"  Region   : {result.Region}");
            }

            // Output the count of unique barcodes.
            Console.WriteLine($"\nTotal barcodes found   : {reader.FoundCount}");
            Console.WriteLine($"Unique barcode count   : {uniqueBarcodes.Count}");
        }
    }
}