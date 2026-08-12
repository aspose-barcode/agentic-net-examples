// Title: GS1 Composite Barcode Delimiter Split Verification
// Description: Demonstrates how to generate a GS1 Composite barcode, split its CodeText using the delimiter, and verify the linear and 2D components.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator, BarCodeReader, and GS1CompositeBar extended parameters. It illustrates typical scenarios where developers need to validate delimiter handling in GS1 Composite symbology, such as splitting linear and 2D parts for inventory or logistics applications.
// Prompt: Create unit test verifying correct delimiter handling when splitting CodeText for GS1 Composite.
// Tags: gs1-composite, barcode-generation, barcode-recognition, delimiter-handling, aspose-barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode, reads it back,
/// and verifies that the delimiter correctly separates the linear and 2D parts of the CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the GS1 Composite delimiter verification test.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        RunGs1CompositeDelimiterTest();
    }

    /// <summary>
    /// Generates a GS1 Composite barcode, saves it to a temporary file,
    /// reads it back, and checks that the extended parameters contain the expected split parts.
    /// </summary>
    static void RunGs1CompositeDelimiterTest()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Expected parts of the GS1 Composite code text
        const string linearPart = "(01)03212345678906";
        const string twoDPart = "(21)A1B2C3D4E5F6G7H8";
        string fullCodeText = $"{linearPart}|{twoDPart}";

        string imagePath = Path.Combine(tempFolder, "gs1composite.png");

        try
        {
            // ---------- Generate GS1 Composite barcode ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
            {
                // Set component types (optional but makes the barcode valid)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Save the barcode image
                generator.Save(imagePath);
            }

            // Verify that the image was created
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("FAILED: Barcode image was not created.");
                return;
            }

            // ---------- Read and validate the barcode ----------
            using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
            {
                bool testPassed = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access GS1 Composite specific extended parameters
                    var ext = result.Extended.GS1CompositeBar;
                    if (ext == null)
                    {
                        Console.WriteLine("FAILED: Extended parameters for GS1 Composite not available.");
                        continue;
                    }

                    // Compare the split parts with the expected values
                    bool linearMatch = string.Equals(ext.OneDCodeText, linearPart, StringComparison.Ordinal);
                    bool twoDMatch = string.Equals(ext.TwoDCodeText, twoDPart, StringComparison.Ordinal);

                    if (linearMatch && twoDMatch)
                    {
                        testPassed = true;
                        Console.WriteLine("PASSED: Delimiter correctly split CodeText into linear and 2D parts.");
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Split parts do not match.\n  Expected Linear: {linearPart}\n  Actual Linear:   {ext.OneDCodeText}\n  Expected 2D:    {twoDPart}\n  Actual 2D:      {ext.TwoDCodeText}");
                    }
                }

                if (!testPassed)
                {
                    Console.WriteLine("FAILED: No valid barcode result was found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception occurred - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    foreach (string file in Directory.GetFiles(tempFolder))
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(tempFolder);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}