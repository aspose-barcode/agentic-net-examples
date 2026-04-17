using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 4 with a simple message)
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Create the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set image resolution (DPI) to 300
            generator.Parameters.Resolution = 300;

            // Define output file path
            string outputPath = Path.Combine(Environment.CurrentDirectory, "maxicode.png");

            // Save the generated barcode image
            generator.Save(outputPath);
        }

        Console.WriteLine("MaxiCode generated with 300 DPI at: " + Path.Combine(Environment.CurrentDirectory, "maxicode.png"));
    }
}