using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample GS1 DataMatrix code text (Application Identifier 01 with a 14‑digit GTIN)
        const string codeText = "(01)12345678901231";

        try
        {
            // Create the barcode generator for GS1 DataMatrix
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
            {
                // Set ECC to ECC200 (error correction level 200)
                generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;

                // Save the barcode as PNG
                generator.Save("gs1datamatrix.png");
            }

            Console.WriteLine("GS1 DataMatrix barcode generated successfully.");
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }
}