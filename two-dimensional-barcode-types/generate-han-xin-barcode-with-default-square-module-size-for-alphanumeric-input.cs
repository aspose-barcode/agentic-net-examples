using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Alphanumeric text to encode
        string codeText = "ABC123XYZ";

        // Create a barcode generator for Han Xin symbology with the given text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Default settings already use square modules, so no additional configuration is required

            // Save the generated barcode image to a file
            generator.Save("hanxin.png");
        }
    }
}