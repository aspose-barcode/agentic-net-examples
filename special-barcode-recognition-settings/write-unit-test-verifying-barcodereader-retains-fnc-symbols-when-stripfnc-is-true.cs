using System;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample GS1-128 code containing FNC1 characters (represented by parentheses)
        const string codeWithFNC = "(02)04006664241007(37)1(400)7019590754";

        // Generate barcode image in memory
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeWithFNC))
        using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
        {
            // Read without stripping FNC characters
            using (BarCodeReader readerNoStrip = new BarCodeReader(barcodeImage, DecodeType.GS1Code128))
            {
                readerNoStrip.BarcodeSettings.StripFNC = false;
                BarCodeResult resultNoStrip = readerNoStrip.ReadBarCodes().FirstOrDefault();
                string textNoStrip = resultNoStrip?.CodeText ?? string.Empty;

                // Read with stripping FNC characters
                using (BarCodeReader readerStrip = new BarCodeReader(barcodeImage, DecodeType.GS1Code128))
                {
                    readerStrip.BarcodeSettings.StripFNC = true;
                    BarCodeResult resultStrip = readerStrip.ReadBarCodes().FirstOrDefault();
                    string textStrip = resultStrip?.CodeText ?? string.Empty;

                    // Verify that stripping reduces the length (FNC characters removed)
                    if (textNoStrip.Length > textStrip.Length)
                    {
                        Console.WriteLine("Test Passed: StripFNC removed FNC characters.");
                        Console.WriteLine($"Original: {textNoStrip}");
                        Console.WriteLine($"Stripped: {textStrip}");
                    }
                    else
                    {
                        Console.WriteLine("Test Failed: StripFNC did not remove FNC characters as expected.");
                        Console.WriteLine($"Original: {textNoStrip}");
                        Console.WriteLine($"Stripped: {textStrip}");
                    }
                }
            }
        }
    }
}