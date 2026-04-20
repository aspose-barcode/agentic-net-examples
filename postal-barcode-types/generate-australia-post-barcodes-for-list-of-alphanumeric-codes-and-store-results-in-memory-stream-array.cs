using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        var codes = new List<string>
        {
            "5912345678ABCde",
            "1234567890",
            "ABCD1234#",
            "INVALID$CODE",
            "9876543210XYZ"
        };

        var memoryStreams = new List<MemoryStream>();

        foreach (var code in codes)
        {
            if (!IsValidAustraliaPostCode(code))
            {
                Console.WriteLine($"Skipping invalid code: {code}");
                continue;
            }

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                memoryStreams.Add(ms);
            }
        }

        MemoryStream[] barcodeImages = memoryStreams.ToArray();

        Console.WriteLine($"Generated {barcodeImages.Length} barcode image(s).");
    }

    static bool IsValidAustraliaPostCode(string text)
    {
        if (text.Length < 2)
            return false;

        string prefix = text.Substring(0, 2);
        var validPrefixes = new HashSet<string> { "11", "45", "59", "62", "87", "92" };
        if (!validPrefixes.Contains(prefix))
            return false;

        foreach (char c in text)
        {
            if (char.IsLetterOrDigit(c) || c == ' ' || c == '#')
                continue;
            return false;
        }

        return true;
    }
}