using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output file path
        string outputFile = "gs1_datamatrix.jpg";

        // Sample GS1 DataMatrix code text (AI 01)
        string codeText = "(01)12345678901231";

        // Create barcode generator for GS1 DataMatrix
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set encoding mode to ECI with UTF‑8 encoding
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save barcode as JPEG (default quality is 90)
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputFile)}");
    }
}