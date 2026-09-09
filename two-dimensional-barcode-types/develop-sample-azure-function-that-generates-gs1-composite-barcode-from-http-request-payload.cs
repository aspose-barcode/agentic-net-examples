// Title: Azure Function Sample: Generate GS1 Composite Barcode
// Description: Demonstrates generating a GS1 Composite barcode from a simulated HTTP request payload and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create GS1 Composite barcodes and BarCodeReader to decode them. Developers working with barcode creation, especially GS1 Composite symbology, often need to configure linear and 2D components, adjust dimensions, and extract metadata after scanning. The sample highlights key API classes such as BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and related parameter objects.
// Prompt: Develop a sample Azure Function that generates GS1 Composite barcode from HTTP request payload.
// Tags: gs1 composite, barcode generation, barcode recognition, png output, aspose.barcode, azure function, http request

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample console application that mimics an Azure Function to generate and read a GS1 Composite barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a GS1 Composite barcode from a simulated HTTP request payload,
    /// saves it to a temporary PNG file, and then reads back its components for verification.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Simulated HTTP request payload containing linear and 2D components
        // ------------------------------------------------------------
        string linearComponent = "(01)12345678901231";
        string twoDComponent = "(01)00123456789012";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Determine a temporary file path for the generated barcode image
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite.png");

        // ------------------------------------------------------------
        // Generate GS1 Composite barcode using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the composite components
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // ------------------------------------------------------------
        // Read back the barcode and display its metadata
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Full CodeText: {result.CodeText}");
                Console.WriteLine($"2D Component Text: {result.Extended.GS1CompositeBar.TwoDCodeText}");
                Console.WriteLine($"1D Component Type: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"2D Component Type: {result.Extended.GS1CompositeBar.TwoDType}");
            }
        }
    }
}