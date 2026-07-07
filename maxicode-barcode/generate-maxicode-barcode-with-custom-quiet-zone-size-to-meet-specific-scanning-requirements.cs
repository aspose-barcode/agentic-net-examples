// Title: Generate MaxiCode Barcode with Custom Quiet Zone
// Description: Demonstrates creating a MaxiCode barcode (Mode 2) with a custom quiet zone and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related classes to produce a MaxiCode symbol. Typical use cases include shipping labels, parcel tracking, and inventory management where MaxiCode is required. Developers often need to adjust quiet zone (padding) settings to meet scanner specifications, making this example a useful reference for customizing barcode layout.
/// Prompt: Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requirements.
/// Tags: maxicode, barcode, quiet zone, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with a custom quiet zone,
/// saves it to a file, and optionally reads it back to verify decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode (Mode 2) barcode,
    /// applies a 10‑point padding on all sides, saves the image, and
    /// demonstrates decoding the saved barcode.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 2 with a standard second message)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // USA
            ServiceCategory = 999
        };
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Create the complex barcode generator with the codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set a custom quiet zone (padding) – 10 points on each side
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated MaxiCode image
            string outputPath = "maxicode.png";
            generator.Save(outputPath);
            Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
        }

        // Optional: read back the barcode to verify it can be decoded
        if (File.Exists("maxicode.png"))
        {
            using (var reader = new BarCodeReader("maxicode.png", DecodeType.MaxiCode))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the MaxiCode codetext using the complex codetext reader
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                    {
                        Console.WriteLine("Decoded MaxiCode:");
                        Console.WriteLine($"  PostalCode: {decodedMode2.PostalCode}");
                        Console.WriteLine($"  CountryCode: {decodedMode2.CountryCode}");
                        Console.WriteLine($"  ServiceCategory: {decodedMode2.ServiceCategory}");
                        if (decodedMode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                        {
                            Console.WriteLine($"  Message: {stdMsg.Message}");
                        }
                    }
                }
            }
        }
    }
}