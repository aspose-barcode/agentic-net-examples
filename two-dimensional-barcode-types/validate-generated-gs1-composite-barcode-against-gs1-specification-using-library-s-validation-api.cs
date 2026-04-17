using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define barcode data: linear part and 2D part separated by '|'
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";
        string outputFile = "gs1composite.png";

        // Generate GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set linear and 2D component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save image
            generator.Save(outputFile);
        }

        // Verify that the file was created
        if (!File.Exists(outputFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Recognize and validate the generated barcode
        using (var reader = new BarCodeReader(outputFile, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation (optional but demonstrates API usage)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool validationPassed = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output raw code text
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");

                // Access GS1 Composite extended parameters
                var ext = result.Extended.GS1CompositeBar;
                if (ext != null && !ext.IsEmpty)
                {
                    Console.WriteLine($"Linear Component Type: {ext.OneDType}");
                    Console.WriteLine($"Linear Component Text: {ext.OneDCodeText}");
                    Console.WriteLine($"2D Component Type: {ext.TwoDType}");
                    Console.WriteLine($"2D Component Text: {ext.TwoDCodeText}");

                    // Simple validation: compare with original parts
                    string expectedLinear = "(01)03212345678906";
                    string expectedTwoD = "(21)A1B2C3D4E5F6G7H8";

                    if (ext.OneDCodeText == expectedLinear && ext.TwoDCodeText == expectedTwoD)
                    {
                        validationPassed = true;
                    }
                }
            }

            Console.WriteLine(validationPassed
                ? "GS1 Composite barcode validation succeeded."
                : "GS1 Composite barcode validation failed.");
        }
    }
}