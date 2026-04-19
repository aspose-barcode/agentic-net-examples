using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Path to the image containing QR codes.
        string imagePath = args.Length > 0 ? args[0] : "sample_qr.png";

        // Verify that the file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader for QR codes.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Read all barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No QR barcodes detected.");
                return;
            }

            // Output version and error correction level for each QR barcode.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Extended QR parameters contain version and error level.
                var qrParams = result.Extended?.QR;
                if (qrParams != null)
                {
                    Console.WriteLine($"QR Version: {qrParams.Version}");
                    Console.WriteLine($"QR Error Level: {qrParams.ErrorLevel}");
                }
                else
                {
                    Console.WriteLine("QR extended parameters not available.");
                }

                Console.WriteLine(); // Blank line between results.
            }
        }
    }
}