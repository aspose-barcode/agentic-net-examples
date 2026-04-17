using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "temp_barcode.png");

        // Ensure any previous file is removed
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // Generate a valid EAN13 barcode (checksum is correct)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Verify the file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with default settings (AllowIncorrectBarcodes = false)
        string codeTextDefault;
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Default AllowIncorrectBarcodes is false, no change needed
            var result = reader.ReadBarCodes();
            if (result.Length == 0)
            {
                Console.WriteLine("No barcode detected with default settings.");
                return;
            }
            codeTextDefault = result[0].CodeText;
        }

        // Read the same barcode with AllowIncorrectBarcodes enabled
        string codeTextAllowIncorrect;
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Enable recognition of incorrect barcodes (should not affect a valid barcode)
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            var result = reader.ReadBarCodes();
            if (result.Length == 0)
            {
                Console.WriteLine("No barcode detected with AllowIncorrectBarcodes enabled.");
                return;
            }
            codeTextAllowIncorrect = result[0].CodeText;
        }

        // Compare the decoded CodeText values
        bool areEqual = string.Equals(codeTextDefault, codeTextAllowIncorrect, StringComparison.Ordinal);
        Console.WriteLine($"CodeText with default settings: {codeTextDefault}");
        Console.WriteLine($"CodeText with AllowIncorrectBarcodes enabled: {codeTextAllowIncorrect}");
        Console.WriteLine($"CodeText values are {(areEqual ? "identical" : "different")}.");

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}