// Title: Embedding Non‑GS1 Data in the 2D Component of a GS1 Composite Barcode
// Description: Demonstrates how to generate a GS1 Composite barcode where the 2D component contains non‑GS1 data, and how to read back both the linear and 2D components.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes (e.g., TwoDComponentType, EncodeTypes) to embed custom data in the 2D part of a composite symbol—common when combining GS1‑compliant linear data with additional application‑specific information. Developers often need to create such barcodes for packaging, logistics, or product labeling where extra data must travel alongside standard GS1 identifiers.
// Prompt: Provide option to embed non‑GS1 data in the 2D component of a GS1 Composite barcode.
// Tags: gs1 composite barcode, non-gs1 data, barcode generation, barcode recognition, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 Composite barcode with non‑GS1 data in the 2D component,
/// saves it as a PNG file, and then reads back the barcode to display both
/// the full code text and the extracted 2D component text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and reading.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory where the barcode image will be stored.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------
        // Define the full file path for the generated barcode image.
        // --------------------------------------------------------------
        string barcodePath = Path.Combine(outputDir, "GS1Composite_NonGS1_2D.png");

        // --------------------------------------------------------------
        // Linear part (GS1) and non‑GS1 data for the 2D component.
        // The pipe character (|) separates the linear and 2D data sections.
        // --------------------------------------------------------------
        string codeText = "(01)98898765432106|Aspose.BarCode";

        // --------------------------------------------------------------
        // Generate the GS1 Composite barcode with the specified settings.
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text for the linear component.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Specify that the 2D component should be a CC‑C (Composite Component) type.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Use GS1‑Code128 for the linear component of the composite barcode.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Allow non‑GS1 data in the 2D component.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // --------------------------------------------------------------
        // Read back the barcode image and display the detected texts.
        // --------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Full code text (linear + 2D) detected by the reader.
                    Console.WriteLine("Detected Code Text: " + result.CodeText);

                    // If extended GS1 Composite information is available, show the 2D component text.
                    if (result.Extended?.GS1CompositeBar != null)
                    {
                        Console.WriteLine("2D Component Text: " + result.Extended.GS1CompositeBar.TwoDCodeText);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to locate the generated barcode image.");
        }
    }
}