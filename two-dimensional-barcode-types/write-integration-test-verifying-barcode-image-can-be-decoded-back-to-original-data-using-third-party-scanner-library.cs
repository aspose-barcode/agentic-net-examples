using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Data to encode
        const string originalData = "Test12345";

        // Temporary file path for the barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "barcode_test.png");

        // Generate and save the barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalData))
        {
            // Save as PNG
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Verify the image file was created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        // Decode the barcode image and verify the data
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            bool decodedSuccessfully = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result.CodeText == originalData)
                {
                    decodedSuccessfully = true;
                    Console.WriteLine("Success: Decoded barcode matches original data: " + result.CodeText);
                    break;
                }
            }

            if (!decodedSuccessfully)
            {
                Console.WriteLine("Failure: Decoded data does not match the original.");
            }
        }

        // Clean up the temporary file
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}