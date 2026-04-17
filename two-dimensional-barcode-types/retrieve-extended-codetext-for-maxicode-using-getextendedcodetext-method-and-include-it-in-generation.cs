using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Build extended codetext for MaxiCode
        var extBuilder = new MaxiCodeExtCodetextBuilder();
        extBuilder.AddECICodetext(ECIEncodings.UTF8, "Hello");
        extBuilder.AddPlainCodetext("World");
        string extendedCodetext = extBuilder.GetExtendedCodetext();

        // Generate MaxiCode barcode using the extended codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, extendedCodetext))
        {
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Extended;
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample MaxiCode";
            generator.Save("maxicode_extended.png");
        }

        Console.WriteLine("Extended Codetext: " + extendedCodetext);
        Console.WriteLine("MaxiCode barcode generated successfully.");
    }
}