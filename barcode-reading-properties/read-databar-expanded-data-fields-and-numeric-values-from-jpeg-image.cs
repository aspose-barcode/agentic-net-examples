using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample data for a GS1 DataBar Expanded barcode
        const string barcodeText = "01234567890123";

        // Generate the barcode image in memory (JPEG format)
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpanded, barcodeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save generated barcode to the memory stream
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                ms.Position = 0; // Reset stream position for reading

                // Read the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.DatabarExpanded))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);

                        // Access DataBar extended parameters (if any)
                        var dataBarExt = result.Extended.DataBar;
                        Console.WriteLine("Is 2D Composite Component: " + dataBarExt.Is2DCompositeComponent);
                    }
                }
            }
        }
    }
}