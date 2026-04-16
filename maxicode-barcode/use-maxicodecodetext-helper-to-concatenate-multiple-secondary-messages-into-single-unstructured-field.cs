using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare multiple secondary messages
        string[] secondaryMessages = new[]
        {
            "First secondary message",
            "Second part of data",
            "Additional info"
        };

        // Concatenate messages into a single unstructured field
        string concatenatedMessage = string.Join(" ", secondaryMessages);

        // Create MaxiCode standard codetext and set mode and concatenated message
        var maxiCodeCodetext = new MaxiCodeStandardCodetext();
        maxiCodeCodetext.Mode = MaxiCodeMode.Mode4; // data in both messages, short ECC
        maxiCodeCodetext.Message = concatenatedMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Save barcode to a memory stream as PNG
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Write the PNG to a file
                File.WriteAllBytes("maxicode.png", memoryStream.ToArray());
            }
        }

        Console.WriteLine("MaxiCode barcode generated and saved as 'maxicode.png'.");
    }
}