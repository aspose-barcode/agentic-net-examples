// Title: Generate GS1 DataMatrix barcode and save as JPEG with UTF‑8 encoding
// Description: Demonstrates creating a GS1 DataMatrix barcode, setting UTF‑8 ECI encoding, and exporting it to a JPEG image.
// Category-Description: This example is part of the Aspose.BarCode generation collection, showing how to configure a specific symbology (GS1 DataMatrix), apply character set encoding (UTF‑8 via ECI), and output the result as a high‑quality JPEG. It utilizes the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need to produce product identification codes and integrate them into imaging workflows.
// Prompt: Create a GS1 DataMatrix barcode, specify UTF‑8 encoding mode, and export to JPEG with quality 90.
// Tags: gs1datamatrix, barcode, generation, jpeg, eci, utf-8, aspose.barcode, encodetypes, imageexport

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode,
/// applies UTF‑8 ECI encoding, and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the image file.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated JPEG image
        const string outputPath = "gs1datamatrix.jpg";

        // GS1 DataMatrix payload (Application Identifier 01 with a 14‑digit GTIN)
        const string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix))
        {
            // Assign the code text to be encoded
            generator.CodeText = codeText;

            // Set the ECI (Extended Channel Interpretation) to UTF‑8 for proper character encoding
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode as a JPEG image (default quality; Aspose uses 90 if not specified)
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }
    }
}