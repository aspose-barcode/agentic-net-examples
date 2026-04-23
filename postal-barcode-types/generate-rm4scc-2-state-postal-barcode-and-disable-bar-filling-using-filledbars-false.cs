using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample RM4SCC code text
        const string codeText = "A123456";

        // Create barcode generator for RM4SCC
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Disable filled bars
            generator.Parameters.Barcode.FilledBars = false;

            // Save the barcode image to a file
            generator.Save("rm4scc.png");
        }
    }
}