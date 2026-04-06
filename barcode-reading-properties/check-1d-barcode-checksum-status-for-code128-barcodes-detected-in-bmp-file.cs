using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the temporary BMP file
        string bmpPath = "code128.bmp";

        // Generate a Code128 barcode and save it as BMP
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Save as BMP (default format is PNG, specify BMP via extension)
            generator.Save(bmpPath);
        }

        // Read the barcode from the BMP file with checksum validation enabled
        using (var reader = new BarCodeReader(bmpPath, DecodeType.Code128))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Full CodeText: " + result.CodeText);

                // For 1D barcodes, Extended.OneD provides Value (without checksum) and CheckSum
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine("Value (without checksum): " + result.Extended.OneD.Value);
                    Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
                }
                else
                {
                    Console.WriteLine("No OneD extended data available.");
                }

                Console.WriteLine();
            }
        }
    }
}