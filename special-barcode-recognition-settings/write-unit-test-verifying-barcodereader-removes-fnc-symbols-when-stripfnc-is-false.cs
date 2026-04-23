using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample GS1-128 code with FNC1 separators (parentheses)
        const string originalCodeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate barcode image into a memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, originalCodeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream is ready for reading
            ms.Position = 0;

            // ---------- Test with StripFNC = false (FNC characters should remain) ----------
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                reader.BarcodeSettings.StripFNC = false;
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("FAIL: No barcode detected (StripFNC = false).");
                    return;
                }

                var codeText = results[0].CodeText;
                if (codeText.Contains("(") && codeText.Contains(")"))
                {
                    Console.WriteLine("PASS: StripFNC = false retains FNC characters.");
                }
                else
                {
                    Console.WriteLine("FAIL: StripFNC = false removed FNC characters unexpectedly.");
                    return;
                }
            }

            // Reset stream position for the second read
            ms.Position = 0;

            // ---------- Test with StripFNC = true (FNC characters should be removed) ----------
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                reader.BarcodeSettings.StripFNC = true;
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("FAIL: No barcode detected (StripFNC = true).");
                    return;
                }

                var codeText = results[0].CodeText;
                if (!codeText.Contains("(") && !codeText.Contains(")"))
                {
                    Console.WriteLine("PASS: StripFNC = true correctly removes FNC characters.");
                }
                else
                {
                    Console.WriteLine("FAIL: StripFNC = true did not remove FNC characters.");
                }
            }
        }
    }
}