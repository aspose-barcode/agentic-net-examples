// Title: GS1 Composite Barcode Linear Component Type Integration Test
// Description: Demonstrates how changing the linear component type in a GS1 Composite barcode affects the generated image, useful for integration testing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType to create composite barcodes with different linear components. Developers often need to verify that configuration changes produce distinct outputs, especially when automating tests for barcode rendering pipelines.
// Prompt: Write integration test confirming linear component type changes reflect correctly in the final GS1 Composite image.
// Tags: gs1 composite barcode, linear component, encode types, integration test, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains an integration test that verifies changing the linear component type of a GS1 Composite barcode
/// results in distinct generated images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application. Generates two GS1 Composite barcodes with different linear components,
    /// saves them, and checks that the output files differ in size.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory for generated barcode images
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "GS1CompositeTest");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define common GS1 Composite codetext (1D|2D parts)
        // --------------------------------------------------------------------
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // --------------------------------------------------------------------
        // First barcode: Linear component set to GS1Code128
        // --------------------------------------------------------------------
        string filePath1 = Path.Combine(outputDir, "Composite_GS1Code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear and 2D component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Configure size and appearance for a fair comparison
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated image
            generator.Save(filePath1);
        }

        // --------------------------------------------------------------------
        // Second barcode: Linear component set to EAN13
        // --------------------------------------------------------------------
        string filePath2 = Path.Combine(outputDir, "Composite_EAN13.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear and 2D component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.EAN13;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Apply the same size settings for comparison
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated image
            generator.Save(filePath2);
        }

        // --------------------------------------------------------------------
        // Verify that the two generated images differ (e.g., by file size)
        // --------------------------------------------------------------------
        long size1 = new FileInfo(filePath1).Length;
        long size2 = new FileInfo(filePath2).Length;

        if (size1 != size2)
        {
            Console.WriteLine("Test passed: Linear component type change reflected in the generated images.");
            Console.WriteLine($"Image 1 ({Path.GetFileName(filePath1)}) size: {size1} bytes");
            Console.WriteLine($"Image 2 ({Path.GetFileName(filePath2)}) size: {size2} bytes");
        }
        else
        {
            Console.WriteLine("Test failed: Images have identical size, change may not be reflected.");
        }
    }
}