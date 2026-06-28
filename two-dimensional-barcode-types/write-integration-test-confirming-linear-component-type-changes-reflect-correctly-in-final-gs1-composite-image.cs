using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and verification of GS1 Composite barcodes
/// using different linear component types (GS1Code128 and UPCA).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two GS1 Composite barcodes with different linear components,
    /// compares the resulting images, and verifies the encoded data.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite codetext: linear part and 2D part separated by '|'
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Create a temporary directory for output images
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest");
        Directory.CreateDirectory(outputDir);

        // Define full paths for the generated barcode images
        string imgPathGs1Code128 = Path.Combine(outputDir, "gs1code128.png");
        string imgPathUpca = Path.Combine(outputDir, "upca.png");

        // Generate barcode with LinearComponentType = GS1Code128
        GenerateGs1Composite(codetext, EncodeTypes.GS1Code128, imgPathGs1Code128);
        // Generate barcode with LinearComponentType = UPCA
        GenerateGs1Composite(codetext, EncodeTypes.UPCA, imgPathUpca);

        // Verify that the two images are different (different linear component)
        bool imagesDiffer = !File.ReadAllBytes(imgPathGs1Code128)
                                 .SequenceEqual(File.ReadAllBytes(imgPathUpca));
        Console.WriteLine($"Images differ after changing LinearComponentType: {imagesDiffer}");

        // Read back the first barcode and verify codetext
        VerifyBarcode(imgPathGs1Code128, codetext);
        // Read back the second barcode and verify codetext
        VerifyBarcode(imgPathUpca, codetext);
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image using the specified linear component type.
    /// </summary>
    /// <param name="codeText">The GS1 Composite codetext to encode.</param>
    /// <param name="linearType">The linear component type (e.g., GS1Code128, UPCA).</param>
    /// <param name="outputPath">File path where the generated image will be saved.</param>
    static void GenerateGs1Composite(string codeText, BaseEncodeType linearType, string outputPath)
    {
        // Initialize the barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component type (GS1Code128, UPCA, etc.)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;

            // Use a simple 2D component type (CC-A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings for better readability
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3;
            generator.Parameters.Barcode.XDimension.Pixels = 3;
            generator.Parameters.Barcode.BarHeight.Pixels = 100;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Reads a barcode image, decodes its content, and verifies it against the expected codetext.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="expectedCodeText">The expected codetext to compare against.</param>
    static void VerifyBarcode(string imagePath, string expectedCodeText)
    {
        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for GS1 Composite symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            // Read all barcodes present in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, report and exit
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                return;
            }

            // Iterate through each detected barcode (typically only one)
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded from {Path.GetFileName(imagePath)}: {result.CodeText}");

                // Compare the decoded text with the expected codetext (case-sensitive)
                bool match = string.Equals(result.CodeText, expectedCodeText, StringComparison.Ordinal);
                Console.WriteLine($"Codetext matches expected: {match}");
            }
        }
    }
}