using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string barcodeFile = "sample_barcode.png";

        // Create a barcode image and save it to a file
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Example of setting a simple property
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Save(barcodeFile);
                Console.WriteLine($"Barcode image saved to '{barcodeFile}'.");
            }
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return;
        }

        // Verify that the file exists before attempting to read it
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine($"File '{barcodeFile}' does not exist. Cannot proceed with decoding.");
            return;
        }

        // Attempt to read the barcode with robust error handling
        try
        {
            using (var reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
            {
                // Optional: configure decoding settings
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                reader.QualitySettings.AllowIncorrectBarcodes = false;

                var results = reader.ReadBarCodes();

                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the image.");
                    return;
                }

                foreach (var result in results)
                {
                    // Check for incomplete or missing codetext
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine($"Barcode of type '{result.CodeTypeName}' was detected but contains no codetext.");
                    }
                    else
                    {
                        Console.WriteLine($"Detected Barcode:");
                        Console.WriteLine($"  Type      : {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText  : {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                    }
                }
            }
        }
        catch (BarCodeRecognitionException ex)
        {
            Console.WriteLine($"Recognition error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during decoding: {ex.Message}");
        }
    }
}