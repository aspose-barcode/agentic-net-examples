// Title: Compare barcode detection accuracy between grayscale and full‑color images
// Description: Demonstrates how to generate color and grayscale barcodes for multiple symbologies and compare detection confidence using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarcodeGenerator, BarCodeReader, and related parameters to evaluate detection performance across different image types. Developers often need to verify that barcodes are readable in various color schemes, especially when integrating scanning functionality into applications that handle both color and grayscale assets.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode symbology, detection, grayscale, color, aspose.barcode, generation, recognition, confidence

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates color and grayscale barcodes for several symbologies,
/// reads them back, and records detection confidence to compare accuracy between image types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, performs recognition, and prints a summary.
    /// </summary>
    static void Main()
    {
        // Define the set of symbologies and corresponding sample texts to test.
        var symbologies = new List<(BaseEncodeType Encode, string CodeText)>
        {
            (EncodeTypes.Code128, "Test123"),
            (EncodeTypes.QR, "TestQR"),
            (EncodeTypes.DataMatrix, "TestDM")
        };

        // Collect result strings for later output.
        var results = new List<string>();

        // Iterate over each symbology, generating and testing both color and grayscale images.
        foreach (var (encode, codeText) in symbologies)
        {
            // ----- Generate a full‑color barcode (red bars on light yellow background) -----
            using (var colorGenerator = new BarcodeGenerator(encode, codeText))
            {
                colorGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
                colorGenerator.Parameters.BackColor = Aspose.Drawing.Color.LightYellow;

                using (var colorStream = new MemoryStream())
                {
                    // Save the color barcode to a memory stream as PNG.
                    colorGenerator.Save(colorStream, BarCodeImageFormat.Png);
                    colorStream.Position = 0; // Reset stream position for reading.

                    // ----- Recognize the color barcode -----
                    bool colorDetected = false;
                    BarCodeConfidence colorConfidence = BarCodeConfidence.None;

                    using (var colorReader = new BarCodeReader(colorStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in colorReader.ReadBarCodes())
                        {
                            colorDetected = true;
                            colorConfidence = result.Confidence;
                            break; // Only need the first detected barcode.
                        }
                    }

                    // ----- Generate a grayscale barcode (black bars on white background) -----
                    using (var grayGenerator = new BarcodeGenerator(encode, codeText))
                    {
                        grayGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        grayGenerator.Parameters.BackColor = Aspose.Drawing.Color.White;

                        using (var grayStream = new MemoryStream())
                        {
                            // Save the grayscale barcode to a memory stream as PNG.
                            grayGenerator.Save(grayStream, BarCodeImageFormat.Png);
                            grayStream.Position = 0; // Reset stream position for reading.

                            // ----- Recognize the grayscale barcode -----
                            bool grayDetected = false;
                            BarCodeConfidence grayConfidence = BarCodeConfidence.None;

                            using (var grayReader = new BarCodeReader(grayStream, DecodeType.AllSupportedTypes))
                            {
                                foreach (var result in grayReader.ReadBarCodes())
                                {
                                    grayDetected = true;
                                    grayConfidence = result.Confidence;
                                    break; // Only need the first detected barcode.
                                }
                            }

                            // Record the comparison results for the current symbology.
                            results.Add($"Symbology: {encode.TypeName}");
                            results.Add($"  Color Detected: {colorDetected}, Confidence: {colorConfidence}");
                            results.Add($"  Grayscale Detected: {grayDetected}, Confidence: {grayConfidence}");
                        }
                    }
                }
            }
        }

        // Output the accumulated summary to the console.
        foreach (var line in results)
        {
            Console.WriteLine(line);
        }
    }
}