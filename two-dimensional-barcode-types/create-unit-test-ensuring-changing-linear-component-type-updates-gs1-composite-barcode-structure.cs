using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample GS1 Composite codetext: linear part (01) and 2D part (21) separated by '|'
        string codeText = "(01)12345678901231|(21)A123";

        // Create a generator for GS1 Composite Bar with default linear component (GS1Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Save image to memory stream
            using (var defaultStream = new MemoryStream())
            {
                generator.Save(defaultStream, BarCodeImageFormat.Png);
                defaultStream.Position = 0;

                // Read barcode and verify default linear component type
                using (var reader = new BarCodeReader(defaultStream, DecodeType.GS1CompositeBar))
                {
                    var result = reader.ReadBarCodes()[0];
                    var extended = result.Extended.GS1CompositeBar;
                    if (extended.OneDType != DecodeType.GS1Code128)
                    {
                        Console.WriteLine("FAILED: Default linear component type is not GS1Code128.");
                    }
                    else
                    {
                        Console.WriteLine("PASS: Default linear component type is GS1Code128.");
                    }
                }
            }

            // Change linear component type to EAN13
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.EAN13;

            // Save updated barcode to another memory stream
            using (var changedStream = new MemoryStream())
            {
                generator.Save(changedStream, BarCodeImageFormat.Png);
                changedStream.Position = 0;

                // Read barcode and verify changed linear component type
                using (var reader = new BarCodeReader(changedStream, DecodeType.GS1CompositeBar))
                {
                    var result = reader.ReadBarCodes()[0];
                    var extended = result.Extended.GS1CompositeBar;
                    if (extended.OneDType != DecodeType.EAN13)
                    {
                        Console.WriteLine("FAILED: Linear component type change to EAN13 not reflected in decoded data.");
                    }
                    else
                    {
                        Console.WriteLine("PASS: Linear component type successfully changed to EAN13.");
                    }
                }
            }
        }
    }
}