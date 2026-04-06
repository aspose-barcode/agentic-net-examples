using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string pdfPath = "maxicode.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(pdfPath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                var mode = result.Extended.MaxiCode.Mode;

                MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);

                if (decoded is MaxiCodeStructuredCodetext structured)
                {
                    Console.WriteLine($"MaxiCode Mode: {mode}");
                    Console.WriteLine($"Postal Code: {structured.PostalCode}");
                }
                else
                {
                    Console.WriteLine($"MaxiCode Mode: {mode} (no structured codetext available)");
                }
            }
        }
    }
}