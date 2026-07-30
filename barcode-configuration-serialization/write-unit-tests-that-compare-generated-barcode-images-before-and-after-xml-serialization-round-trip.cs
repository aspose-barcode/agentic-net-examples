// Title: Barcode XML Serialization Round‑Trip Image Comparison
// Description: Demonstrates generating barcodes, exporting settings to XML, re‑importing, and verifying that the resulting images are identical.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, showcasing how to use BarcodeGenerator, ExportToXml, and ImportFromXml for persisting barcode configurations. Typical use cases include saving barcode settings, transferring them between services, and ensuring visual consistency after deserialization. Developers often need to validate that serialization does not alter the generated output.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, xml serialization, round-trip, image comparison, code128, qr, datamatrix, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcodes, serializes the generator settings to XML,
/// deserializes them back, and compares the original and round‑trip images for equality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over a set of barcode types, performs an XML
    /// round‑trip of the generator settings, and prints the comparison result.
    /// </summary>
    static void Main()
    {
        // Define a list of barcode symbologies and sample texts to test.
        var tests = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "Test123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Process each test case.
        foreach (var (type, text) in tests)
        {
            // ------------------------------------------------------------
            // Generate the original barcode image.
            // ------------------------------------------------------------
            byte[] originalImage;
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Set a deterministic parameter to ensure repeatable output.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                originalImage = GetImageBytes(generator);
            }

            // ------------------------------------------------------------
            // Export the generator settings to XML (in‑memory).
            // ------------------------------------------------------------
            byte[] xmlData;
            using (var generator = new BarcodeGenerator(type, text))
            {
                generator.Parameters.Barcode.XDimension.Point = 2f;
                using (var xmlStream = new MemoryStream())
                {
                    generator.ExportToXml(xmlStream);
                    xmlData = xmlStream.ToArray();
                }
            }

            // ------------------------------------------------------------
            // Import the generator settings from the XML data.
            // ------------------------------------------------------------
            BarcodeGenerator importedGenerator;
            using (var xmlStream = new MemoryStream(xmlData))
            {
                importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream);
            }

            // ------------------------------------------------------------
            // Generate the barcode image after the XML round‑trip.
            // ------------------------------------------------------------
            byte[] roundTripImage;
            using (importedGenerator)
            {
                roundTripImage = GetImageBytes(importedGenerator);
            }

            // ------------------------------------------------------------
            // Compare the two images byte‑by‑byte and output the result.
            // ------------------------------------------------------------
            bool imagesEqual = originalImage.SequenceEqual(roundTripImage);
            Console.WriteLine($"{type.TypeName} round‑trip test: {(imagesEqual ? "PASS" : "FAIL")}");
        }
    }

    /// <summary>
    /// Generates a barcode image using the provided <see cref="BarcodeGenerator"/>
    /// and returns the image data as a PNG byte array.
    /// </summary>
    /// <param name="generator">Configured barcode generator.</param>
    /// <returns>PNG image bytes.</returns>
    private static byte[] GetImageBytes(BarcodeGenerator generator)
    {
        using (var bitmap = generator.GenerateBarCodeImage())
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}