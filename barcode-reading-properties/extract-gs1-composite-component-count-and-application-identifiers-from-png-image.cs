// Title: Extract GS1 Composite component count and application identifiers from a PNG image
// Description: Demonstrates how to generate a GS1 Composite barcode image if missing, then read it to obtain the component count and AI list.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator for creating sample barcodes and BarCodeReader with DecodeType.GS1CompositeBar to extract extended GS1 Composite data such as component count and application identifiers. Developers working with supply‑chain labeling, inventory tracking, or any GS1‑based systems can use these APIs to validate and parse composite barcodes.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: gs1 composite, barcode recognition, barcode generation, png, aspnet.barcode, extended data, application identifiers

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates (if necessary) a GS1 Composite barcode image
/// and extracts its component count and application identifiers using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode when missing, then reads the image
    /// to display GS1 Composite extended information.
    /// </summary>
    static void Main()
    {
        const string imagePath = "gs1composite.png";

        // ------------------------------------------------------------
        // Ensure a sample image exists; create one if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Generate a GS1 Composite barcode with linear and 2‑D components.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, "(01)01234567890123|(21)ABC123"))
            {
                // Specify the component types explicitly (optional).
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Save the generated barcode image to disk.
                generator.Save(imagePath);
                Console.WriteLine($"Sample barcode created at: {Path.GetFullPath(imagePath)}");
            }
        }

        // ------------------------------------------------------------
        // Read the barcode and extract GS1 Composite extended data.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Access the GS1 Composite extended information.
                var gs1Ext = result.Extended.GS1CompositeBar;
                if (gs1Ext == null)
                {
                    Console.WriteLine("No GS1 Composite extended data available.");
                    continue;
                }

                // ----- Component count (via reflection to stay safe against API changes) -----
                var compCountProp = gs1Ext.GetType().GetProperty("ComponentCount", BindingFlags.Public | BindingFlags.Instance);
                if (compCountProp != null)
                {
                    var countValue = compCountProp.GetValue(gs1Ext);
                    Console.WriteLine($"Component Count: {countValue}");
                }
                else
                {
                    Console.WriteLine("ComponentCount property not found.");
                }

                // ----- Application identifiers (via reflection) -----
                var aiProp = gs1Ext.GetType().GetProperty("ApplicationIdentifiers", BindingFlags.Public | BindingFlags.Instance);
                if (aiProp != null)
                {
                    var ais = aiProp.GetValue(gs1Ext) as string[];
                    if (ais != null && ais.Length > 0)
                    {
                        Console.WriteLine("Application Identifiers:");
                        foreach (var ai in ais)
                        {
                            Console.WriteLine($"  {ai}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Application Identifiers found.");
                    }
                }
                else
                {
                    Console.WriteLine("ApplicationIdentifiers property not found.");
                }
            }
        }
    }
}