using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // GTIN‑12 value (12 digits) padded to 14 digits for AI 01.
        const string gtin12 = "(01)00123456789012";

        // Combine linear and 2D parts with the '|' separator as required for GS1 Composite.
        string compositeCode = $"{gtin12}|{gtin12}";

        // File name for the generated barcode image.
        const string fileName = "gs1composite.png";

        // Create the GS1 Composite barcode generator.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCode))
        {
            // Linear component: GS1 Code 128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // 2D component: CC‑A (MicroPDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: set dimensions for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image.
            generator.Save(fileName);
        }

        // Verify that the file was created.
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode back and extract the 2D component text.
        using (BarCodeReader reader = new BarCodeReader(fileName, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation (optional, does not affect this test).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes from the image (there should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access the extended parameters specific to GS1 Composite.
                GS1CompositeBarExtendedParameters ext = result.Extended.GS1CompositeBar;

                // The TwoDCodeText should match the 2D part we supplied.
                string expectedTwoD = gtin12;
                string actualTwoD = ext.TwoDCodeText;

                if (actualTwoD == expectedTwoD)
                {
                    Console.WriteLine("Test passed: 2D component correctly contains zero‑suppressed GTIN‑12.");
                }
                else
                {
                    Console.WriteLine($"Test failed: Expected 2D text '{expectedTwoD}', but got '{actualTwoD}'.");
                }

                // Also verify the linear component for completeness.
                string expectedOneD = gtin12;
                string actualOneD = ext.OneDCodeText;
                if (actualOneD == expectedOneD)
                {
                    Console.WriteLine("Linear component also matches expected GTIN‑12.");
                }
                else
                {
                    Console.WriteLine($"Linear component mismatch: Expected '{expectedOneD}', but got '{actualOneD}'.");
                }

                // No need to continue after the first result.
                break;
            }
        }

        // Clean up the generated image (optional).
        try
        {
            File.Delete(fileName);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}