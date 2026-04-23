using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        var samples = new (CustomerInformationInterpretingType Type, string CodeText, string FileName)[]
        {
            (CustomerInformationInterpretingType.CTable, "5912345678ABCde", "AustraliaPost_CTable.png"),
            (CustomerInformationInterpretingType.NTable, "4512345678", "AustraliaPost_NTable.png"),
            (CustomerInformationInterpretingType.Other, "6212", "AustraliaPost_Other.png")
        };

        foreach (var sample in samples)
        {
            if (!IsValidForInterpretingType(sample.Type, sample.CodeText))
            {
                Console.WriteLine($"Skipping generation for {sample.Type}: invalid code text \"{sample.CodeText}\".");
                continue;
            }

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost))
            {
                generator.Parameters.Barcode.AustralianPost.EncodingTable = sample.Type;
                generator.CodeText = sample.CodeText;
                generator.Save(sample.FileName, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode image: {sample.FileName}");
            }

            using (Bitmap image = (Bitmap)Image.FromFile(sample.FileName))
            using (BarCodeReader reader = new BarCodeReader(image, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = sample.Type;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded [{sample.Type}] - Type: {result.CodeType}, CodeText: {result.CodeText}");
                }
            }
        }
    }

    static bool IsValidForInterpretingType(CustomerInformationInterpretingType type, string text)
    {
        switch (type)
        {
            case CustomerInformationInterpretingType.CTable:
                foreach (char c in text)
                {
                    if (!(char.IsLetterOrDigit(c) || c == ' ' || c == '#'))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.NTable:
                foreach (char c in text)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.Other:
                if (text.Length > 3)
                    return false;
                foreach (char c in text)
                {
                    if (c < '0' || c > '3')
                        return false;
                }
                return true;

            default:
                return false;
        }
    }
}