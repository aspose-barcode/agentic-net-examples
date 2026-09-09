// Title: Extract GS1 Composite Component Count and Application Identifiers from PNG
// Description: Demonstrates generating a GS1 Composite barcode, saving it as a PNG image, then reading the image to obtain the component count and the set of Application Identifiers (AIs) present in the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator for creating a composite barcode, BarCodeReader for decoding, and the Extended.GS1CompositeBar properties to access component details. Developers working with GS1 standards often need to extract component types, counts, and AI data for inventory, logistics, and compliance scenarios.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: gs1 composite, barcode generation, barcode recognition, png, application identifiers, c#, aspose.barcode

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates GS1 Composite barcode generation, saving to PNG, and extraction of component count and Application Identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a GS1 Composite barcode, reads it back, and prints component information and AIs.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated PNG image
        string barcodePath = Path.Combine(tempFolder, "gs1_composite.png");

        // -------------------------------------------------
        // Generate a sample GS1 Composite barcode and save it as PNG
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(
            EncodeTypes.GS1CompositeBar,
            "(01)12345678901231(10)ABCD1234|(21)XYZ"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure GS1 Composite specific settings
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Save the barcode image to the temporary location
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode image and extract GS1 Composite details
        // -------------------------------------------------
        BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                var ext = result.Extended.GS1CompositeBar;

                // Determine how many components (1D and/or 2D) are present
                int componentCount = 0;
                if (!string.IsNullOrEmpty(ext.OneDCodeText)) componentCount++;
                if (!string.IsNullOrEmpty(ext.TwoDCodeText)) componentCount++;

                Console.WriteLine($"Component Count: {componentCount}");
                Console.WriteLine($"1D Component Type: {ext.OneDType}");
                Console.WriteLine($"2D Component Type: {ext.TwoDType}");

                // Collect unique Application Identifiers from both components
                var aiSet = new System.Collections.Generic.HashSet<string>();
                ExtractAIs(ext.OneDCodeText, aiSet);
                ExtractAIs(ext.TwoDCodeText, aiSet);

                Console.WriteLine("Application Identifiers found:");
                foreach (string ai in aiSet)
                {
                    Console.WriteLine($"  {ai}");
                }
            }
        }

        // -------------------------------------------------
        // Cleanup temporary files and folder
        // -------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demo
        }
    }

    /// <summary>
    /// Extracts Application Identifier patterns (e.g., (01), (10), (21)) from a code text string and adds them to the provided set.
    /// </summary>
    /// <param name="codeText">The raw barcode text containing AI patterns.</param>
    /// <param name="aiSet">A hash set that will receive the extracted AI strings.</param>
    static void ExtractAIs(string codeText, System.Collections.Generic.HashSet<string> aiSet)
    {
        if (string.IsNullOrEmpty(codeText))
            return;

        // Match patterns like (01), (10), (21) etc. using a regular expression
        foreach (Match m in Regex.Matches(codeText, @"\(\d{2,4}\)"))
        {
            aiSet.Add(m.Value);
        }
    }
}