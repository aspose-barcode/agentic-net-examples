using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create standard MaxiCode codetext for Mode 6
        var maxiCodeCodetext = new MaxiCodeStandardCodetext();
        maxiCodeCodetext.Mode = MaxiCodeMode.Mode6;
        maxiCodeCodetext.Message = "Test message";

        // Initialize ComplexBarcodeGenerator with the codetext
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set transparent background
            complexGenerator.Parameters.BackColor = Color.Transparent;

            // Generate barcode and save to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                complexGenerator.Save(memoryStream, BarCodeImageFormat.Png);
                // Optionally, demonstrate that the stream contains data
                Console.WriteLine($"Generated barcode size: {memoryStream.Length} bytes");
            }
        }
    }
}