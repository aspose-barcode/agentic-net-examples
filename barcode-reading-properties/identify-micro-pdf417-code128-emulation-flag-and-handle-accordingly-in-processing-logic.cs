using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Example data with Application Indicator "a" and FNC1 separator
        string codeText = "a\u001d1234567890";

        // Generate MicroPdf417 with Code128 emulation enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, codeText))
        {
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;
            generator.Save("MicroPdf417_Emulation.png");
        }

        // Generate MicroPdf417 without Code128 emulation
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, codeText))
        {
            // IsCode128Emulation defaults to false, no need to set explicitly
            generator.Save("MicroPdf417_Standard.png");
        }

        // Read and display the emulation flag for the first image
        Console.WriteLine("Reading MicroPdf417 with Code128 emulation:");
        using (var reader = new BarCodeReader("MicroPdf417_Emulation.png", DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsCode128Emulation: {result.Extended.Pdf417.IsCode128Emulation}");
            }
        }

        // Read and display the emulation flag for the second image
        Console.WriteLine("\nReading MicroPdf417 without Code128 emulation:");
        using (var reader = new BarCodeReader("MicroPdf417_Standard.png", DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsCode128Emulation: {result.Extended.Pdf417.IsCode128Emulation}");
            }
        }
    }
}