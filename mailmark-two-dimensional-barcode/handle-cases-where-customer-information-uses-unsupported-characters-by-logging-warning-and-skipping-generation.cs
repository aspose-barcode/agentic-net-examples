using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample data: some strings contain unsupported characters for CTable (only A-Z, a-z, 0-9, space, #)
        var customerInfos = new List<string>
        {
            "ABC123",          // valid
            "Hello World",     // valid (space allowed)
            "Test#2021",       // valid (# allowed)
            "Invalid@Char",    // invalid (@)
            "中文字符",          // invalid (non‑ASCII)
            "Mix#中文",         // invalid (contains Chinese)
        };

        int index = 1;
        foreach (var info in customerInfos)
        {
            // Construct a simple Australia Post code text: base part + customer information
            string codeText = "5912345678" + info;

            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
                {
                    // Use CTable interpreting type to enforce allowed characters
                    generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                    // Save the barcode image; filename includes index for uniqueness
                    string fileName = $"AustraliaPost_{index}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Generated barcode for \"{info}\" -> {fileName}");
                }
            }
            catch (InvalidCodeException ex)
            {
                // Log warning and skip this entry
                Console.WriteLine($"Warning: Customer information \"{info}\" contains unsupported characters. Skipping generation. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Unexpected errors
                Console.WriteLine($"Error processing \"{info}\": {ex.Message}");
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}