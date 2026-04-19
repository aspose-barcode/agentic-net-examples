using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string filePath = "gs1composite.png";

        // CodeText with the GS1 Composite delimiter '|'
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Generate the GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Optional: specify component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the barcode image
            generator.Save(filePath);
        }

        // Ensure the image was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode and verify delimiter handling
        using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
        {
            bool testPassed = false;
            foreach (var result in reader.ReadBarCodes())
            {
                var ext = result.Extended.GS1CompositeBar;

                // Expected split parts
                string expectedOneD = "(01)03212345678906";
                string expectedTwoD = "(21)A1B2C3D4E5F6G7H8";

                bool oneDMatch = ext.OneDCodeText == expectedOneD;
                bool twoDMatch = ext.TwoDCodeText == expectedTwoD;

                if (oneDMatch && twoDMatch)
                {
                    testPassed = true;
                    Console.WriteLine("Delimiter handling test passed.");
                }
                else
                {
                    Console.WriteLine($"Delimiter handling test failed. OneD: '{ext.OneDCodeText}', TwoD: '{ext.TwoDCodeText}'");
                }
            }

            if (!testPassed)
            {
                Console.WriteLine("No barcode recognized or split parts mismatched.");
            }
        }
    }
}