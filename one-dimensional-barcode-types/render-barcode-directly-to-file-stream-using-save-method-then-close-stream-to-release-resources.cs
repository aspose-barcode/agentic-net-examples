using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Define the output file path
            string outputPath = "barcode.png";

            // Open a file stream for writing the barcode image
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the barcode directly to the stream in PNG format
                generator.Save(fileStream, BarCodeImageFormat.Png);
                // The using block ensures the stream is closed and resources are released
            }
        }

        Console.WriteLine("Barcode image saved successfully.");
    }
}