using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 DataMatrix barcode and saves it as a BMP file.
    /// </summary>
    static void Main()
    {
        // Define the barcode content: AI (01) followed by a 14‑digit GTIN.
        string codeText = "(01)12345678901231";

        // Choose the GS1 DataMatrix symbology.
        BaseEncodeType encodeType = EncodeTypes.GS1DataMatrix;

        // Create a barcode generator with the specified type and content.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the X‑dimension (module size) to 5 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Render and save the barcode as a BMP image.
            generator.Save("gs1datamatrix.bmp");
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("GS1 DataMatrix barcode saved as gs1datamatrix.bmp");
    }
}