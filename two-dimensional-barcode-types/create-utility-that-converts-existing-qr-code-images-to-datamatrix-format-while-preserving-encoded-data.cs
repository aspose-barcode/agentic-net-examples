using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Input QR code image path (adjust as needed)
        string inputPath = "qr.png";
        // Output DataMatrix image path
        string outputPath = "datamatrix.png";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Decode the QR code to obtain the encoded text
        using (var reader = new BarCodeReader(inputPath, DecodeType.QR))
        {
            var result = reader.ReadBarCodes().FirstOrDefault();
            if (result == null)
            {
                Console.WriteLine("No QR code detected in the image.");
                return;
            }

            string codeText = result.CodeText;
            Console.WriteLine($"Decoded QR code text: {codeText}");

            // Generate a DataMatrix barcode with the same text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Optional: customize DataMatrix parameters here if needed
                // e.g., generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;

                // Save the DataMatrix image
                generator.Save(outputPath);
                Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
            }
        }
    }
}