using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Create a simple Code128 barcode and save it.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            generator.Save(filePath);
        }

        // Read the barcode with both UseMinimalXDimension and AllowIncorrectBarcodes enabled.
        try
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Set recognition mode to use minimal X dimension.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Allow recognition of incorrect barcodes.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Read successfully:");
                    Console.WriteLine("  CodeText: " + result.CodeText);
                    Console.WriteLine("  Symbology: " + result.CodeTypeName);
                }
            }

            Console.WriteLine("No conflict detected when both settings are applied.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An exception occurred while reading with both settings:");
            Console.WriteLine(ex.Message);
        }
    }
}