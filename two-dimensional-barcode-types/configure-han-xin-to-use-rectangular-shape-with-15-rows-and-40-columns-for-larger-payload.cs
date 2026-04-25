using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample payload that benefits from a larger symbol size
        string payload = "This is a longer text payload intended to demonstrate a larger Han Xin barcode generation.";

        // Create a Han Xin barcode generator with the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, payload))
        {
            // Han Xin supports only square symbols. Rectangular shapes or custom row/column
            // configurations are not available. The version is set to Auto so the library
            // selects the appropriate square size based on the payload length.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto; // Han Xin supports square formats only

            // Optional: set error correction level (L2 provides moderate recovery)
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the generated barcode image
            generator.Save("HanXin.png");
        }

        Console.WriteLine("Han Xin barcode generated and saved as HanXin.png");
    }
}