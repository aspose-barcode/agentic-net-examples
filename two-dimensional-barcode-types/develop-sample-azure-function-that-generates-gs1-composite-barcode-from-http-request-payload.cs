// Title: Azure Function sample generating GS1 Composite barcode
// Description: Demonstrates how to create a GS1 Composite barcode from an HTTP request payload using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the BarcodeGenerator class with EncodeTypes.GS1CompositeBar, configuring linear and 2D components, and saving the result as an image. Developers building web services or Azure Functions that need to produce barcodes on‑the‑fly can use this pattern as a reference.
// Prompt: Develop a sample Azure Function that generates GS1 Composite barcode from HTTP request payload.
// Tags: gs1 composite, barcode generation, png output, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application illustrating the core barcode generation logic
/// that would be used inside an Azure Function to produce a GS1 Composite barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample application.
    /// Generates a GS1 Composite barcode from a hard‑coded payload and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // NOTE: Azure Functions cannot be demonstrated in this console runner.
        // The core logic below generates a GS1 Composite barcode from a sample payload.

        // Sample payload representing linear and 2D components separated by '|'
        string payload = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the barcode generator with GS1 Composite symbology and the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, payload))
        {
            // Configure the linear component (e.g., GS1 Code128)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component (e.g., Composite Component A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set visual properties: X‑dimension and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Define the output file path and save the barcode image
            string outputPath = "gs1composite.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"GS1 Composite barcode saved to {outputPath}");
        }
    }
}