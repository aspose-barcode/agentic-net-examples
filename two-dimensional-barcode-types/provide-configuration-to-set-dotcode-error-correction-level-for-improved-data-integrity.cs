using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample codetext to encode
        string codeText = "Sample DotCode";

        // Create a DotCode barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set the encode mode to Extended, which provides better data integrity
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Extended;

            // Optionally specify the ECI encoding (e.g., UTF-8)
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image
            generator.Save("dotcode.png");
        }
    }
}