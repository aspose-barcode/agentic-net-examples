using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample code text for Australia Post barcode
        const string codeText = "5912345678ABCde";

        // Validate customer information for CTable (A-Z, a-z, 0-9, space, #)
        if (!IsValidCTable(codeText))
        {
            Console.WriteLine("Warning: Code text contains characters not allowed for CTable interpreting type.");
            return;
        }

        // Generate Australia Post barcode with CTable interpreting type
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save barcode image
            const string imagePath = "AustraliaPostCTable.png";
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to {Path.GetFullPath(imagePath)}");

            // Recognize the barcode and set decoding interpreting type to CTable
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeType}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                }
            }
        }
    }

    // Helper method to validate CTable allowed characters
    static bool IsValidCTable(string text)
    {
        foreach (char ch in text)
        {
            if (char.IsLetterOrDigit(ch) || ch == ' ' || ch == '#')
                continue;
            return false;
        }
        return true;
    }
}