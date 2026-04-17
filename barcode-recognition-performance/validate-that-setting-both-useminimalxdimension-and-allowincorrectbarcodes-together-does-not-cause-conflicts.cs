using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define temporary file path
        string filePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");

        // Ensure any existing file is removed
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode image
            generator.Save(filePath);
        }

        // Verify the file was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with both UseMinimalXDimension and AllowIncorrectBarcodes enabled
        try
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Set recognition mode to UseMinimalXDimension
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                // Allow recognition of incorrect barcodes
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Perform reading
                bool anyFound = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }

                if (!anyFound)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during recognition: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}