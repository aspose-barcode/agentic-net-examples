using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample HIBC DataMatrix LIC code text (primary data only for demonstration)
        const string codeText = "A12345";

        // Create a barcode generator for HIBC DataMatrix LIC
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC, codeText))
        {
            // Set high resolution (300 DPI) for sharper output
            generator.Parameters.Resolution = 300f;

            // Optional: define image size if needed (e.g., 300x300 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the barcode image as PNG
            generator.Save("HIBC_DataMatrix_LIC.png");
        }

        Console.WriteLine("Barcode image generated successfully.");
    }
}