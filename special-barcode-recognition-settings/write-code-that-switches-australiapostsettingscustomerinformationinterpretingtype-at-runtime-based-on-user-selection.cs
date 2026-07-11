// Title: Switch AustraliaPost CustomerInformationInterpretingType at Runtime
// Description: Demonstrates how to set the CustomerInformationInterpretingType for Australia Post barcodes based on a command‑line argument, then generate and read the barcode using the same setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to work with the AustraliaPostSettings class, specifically the CustomerInformationInterpretingType property, which controls how customer information is interpreted during encoding and decoding. Developers creating shipping labels or postal barcodes often need to switch this setting at runtime to match different postal service requirements.
// Prompt: Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on user selection.
// Tags: barcode symbology, australia post, runtime configuration, generation, recognition, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that switches <c>AustraliaPostSettings.CustomerInformationInterpretingType</c> at runtime,
/// generates an Australia Post barcode, and then reads it back using the same interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional command‑line argument specifying the desired <c>CustomerInformationInterpretingType</c>.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument should be a valid <c>CustomerInformationInterpretingType</c> value.</param>
    static void Main(string[] args)
    {
        // Determine interpreting type from command‑line argument; default to Other if parsing fails.
        CustomerInformationInterpretingType interpretingType;
        if (args.Length > 0 && Enum.TryParse(args[0], true, out CustomerInformationInterpretingType parsed))
        {
            interpretingType = parsed;
        }
        else
        {
            interpretingType = CustomerInformationInterpretingType.Other;
        }

        // Sample Australia Post code text (FCC=11, DPID=8 digits, no customer info).
        const string codeText = "1100000000";

        // Generate barcode with the selected interpreting type.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Apply the runtime interpreting type to the generator settings.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = interpretingType;

            const string imagePath = "AustraliaPost.png";

            // Save the generated barcode image to disk.
            generator.Save(imagePath);
            Console.WriteLine($"Barcode generated with CustomerInformationInterpretingType = {interpretingType}");
            Console.WriteLine($"Image saved to: {imagePath}");

            // Recognize the barcode and apply the same interpreting type for decoding.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
            {
                // Configure the reader to use the same interpreting type as the generator.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

                // Iterate through all detected barcodes (should be one) and output details.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeType}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}