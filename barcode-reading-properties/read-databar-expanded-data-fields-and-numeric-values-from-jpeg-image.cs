using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string imagePath = "databar_expanded.jpg";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DatabarExpanded))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No DataBar Expanded barcodes detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Extended DataBar information
                DataBarExtendedParameters dataBarExt = result.Extended.DataBar;
                Console.WriteLine($"Is 2D Composite Component: {dataBarExt.Is2DCompositeComponent}");
                Console.WriteLine($"Is Empty: {dataBarExt.IsEmpty}");
                Console.WriteLine();
            }
        }
    }
}