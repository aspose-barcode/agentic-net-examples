// Title: Mailmark 2D Barcode Generation Time and Image Size Comparison
// Description: Demonstrates how to generate Mailmark type 7 and type 29 barcodes, measuring the time taken and the resulting PNG file size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex 2‑D symbologies. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and AutoSizeMode to create Mailmark barcodes, a common requirement for postal and logistics applications where performance and payload size matter. Developers often need to benchmark different Mailmark matrix types to choose the optimal configuration for their workflow.
// Prompt: Compare generation time and image size between Mailmark type 7 and type 29 barcodes.
// Tags: mailmark, barcode, generation, performance, image size, aspose.barcode, complexbarcode, 2d symbology

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides an entry point that compares the generation time and PNG image size of
/// Mailmark type 7 (24×24) and type 29 (16×48) barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Prepares common Mailmark fields and invokes the comparison for both matrix types.
    /// </summary>
    static void Main()
    {
        // Common Mailmark2D fields shared by both barcode types
        const string versionId = "1";
        const string informationTypeId = "0";
        const string classCode = "0";
        const string rtsFlag = "0";
        int supplyChainId = 384224;
        int itemId = 16563762;
        const string destinationPostCodePlusDps = "EF61AH8T ";

        // Compare Type 7 (24x24) and Type 29 (16x48) matrix configurations
        CompareMailmark2D(
            Mailmark2DType.Type_7,
            "Type 7 (24x24)",
            versionId,
            informationTypeId,
            classCode,
            rtsFlag,
            supplyChainId,
            itemId,
            destinationPostCodePlusDps);

        CompareMailmark2D(
            Mailmark2DType.Type_29,
            "Type 29 (16x48)",
            versionId,
            informationTypeId,
            classCode,
            rtsFlag,
            supplyChainId,
            itemId,
            destinationPostCodePlusDps);
    }

    /// <summary>
    /// Generates a Mailmark barcode of the specified matrix type, measures the generation time,
    /// and reports the resulting PNG image size.
    /// </summary>
    /// <param name="matrixType">The Mailmark matrix type (e.g., Type_7 or Type_29).</param>
    /// <param name="label">A friendly label used in console output.</param>
    /// <param name="versionId">Version identifier for the Mailmark.</param>
    /// <param name="informationTypeId">Information type identifier.</param>
    /// <param name="classCode">Class code of the Mailmark.</param>
    /// <param name="rtsFlag">RTS flag value.</param>
    /// <param name="supplyChainId">Supply chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS.</param>
    static void CompareMailmark2D(
        Mailmark2DType matrixType,
        string label,
        string versionId,
        string informationTypeId,
        string classCode,
        string rtsFlag,
        int supplyChainId,
        int itemId,
        string destinationPostCodePlusDps)
    {
        // Build the Mailmark2DCodetext object with all required fields
        var mailmark = new Mailmark2DCodetext
        {
            VersionID = versionId,
            InformationTypeID = informationTypeId,
            Class = classCode,
            RTSFlag = rtsFlag,
            SupplyChainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodeAndDPS = destinationPostCodePlusDps,
            DataMatrixType = matrixType
        };

        // Start timing the barcode generation process
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Explicitly set auto‑size mode to interpolation (default, but clarified)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                stopwatch.Stop(); // Stop timing after the image is saved

                long imageSize = ms.Length; // Size in bytes of the generated PNG

                // Output the results to the console
                Console.WriteLine($"{label}:");
                Console.WriteLine($"  Generation time: {stopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"  Image size: {imageSize} bytes");
                Console.WriteLine();
            }
        }
    }
}