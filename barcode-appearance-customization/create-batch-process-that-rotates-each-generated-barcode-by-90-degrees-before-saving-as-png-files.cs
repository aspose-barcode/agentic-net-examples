using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define a list of barcodes to generate: each entry contains the symbology, the code text and the output file name.
        var barcodes = new List<(BaseEncodeType type, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "datamatrix.png")
        };

        foreach (var (type, codeText, fileName) in barcodes)
        {
            // Create a barcode generator for the specified type and code text.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Rotate the generated barcode image by 90 degrees.
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode as a PNG file.
                generator.Save(fileName);
                Console.WriteLine($"Saved rotated barcode to {fileName}");
            }
        }
    }
}