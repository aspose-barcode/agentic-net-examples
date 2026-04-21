using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define paths
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "code39.png");

        // -----------------------------------------------------------------
        // Generate a Code39FullASCII barcode with checksum enabled
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
        {
            // Enable checksum generation and always show it in the human‑readable text
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image
            generator.Save(barcodePath);
        }

        // -----------------------------------------------------------------
        // Recognize the barcode with checksum validation turned ON
        // -----------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39FullASCII))
            {
                // Enable strict checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                bool anyResult = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyResult = true;
                    Console.WriteLine("=== ChecksumValidation.On ===");
                    Console.WriteLine("Type      : " + result.CodeTypeName);
                    Console.WriteLine("CodeText  : " + result.CodeText);
                    Console.WriteLine("Value     : " + result.Extended.OneD.Value);
                    Console.WriteLine("Checksum  : " + result.Extended.OneD.CheckSum);
                }

                if (!anyResult)
                {
                    Console.WriteLine("No barcode detected – checksum validation failed.");
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Handle any Aspose.BarCode specific errors
            Console.WriteLine("BarCodeException: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        // -----------------------------------------------------------------
        // Recognize the same barcode with checksum validation turned OFF
        // -----------------------------------------------------------------
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39FullASCII))
            {
                // Disable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("=== ChecksumValidation.Off ===");
                    Console.WriteLine("Type      : " + result.CodeTypeName);
                    Console.WriteLine("CodeText  : " + result.CodeText);
                    Console.WriteLine("Value     : " + result.Extended.OneD.Value);
                    Console.WriteLine("Checksum  : " + result.Extended.OneD.CheckSum);
                }
            }
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine("BarCodeException: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}