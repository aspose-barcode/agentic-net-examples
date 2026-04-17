using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Output file path
        string outputFile = "maxicode.tif";

        // Create standard MaxiCode codetext (Mode 4) with a simple message
        var maxiCodeCodetext = new MaxiCodeStandardCodetext();
        maxiCodeCodetext.Mode = MaxiCodeMode.Mode4;
        maxiCodeCodetext.Message = "Test message";

        // Generate the MaxiCode barcode and save as lossless TIFF
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set high resolution for printing (e.g., 300 DPI)
            generator.Parameters.Resolution = 300;

            // Save the barcode as TIFF (lossless compression)
            generator.Save(outputFile, BarCodeImageFormat.Tiff);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputFile)}");
    }
}