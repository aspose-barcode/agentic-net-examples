// Title: Generate and Decode Mailmark2D Barcode
// Description: Demonstrates creating a Mailmark2D barcode, saving it as a PNG image in memory, reading it back, and decoding its individual fields.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection, showcasing the use of ComplexBarcodeGenerator for barcode creation, BarCodeReader for image recognition, and ComplexCodetextReader for parsing Mailmark2D codetext. Developers working with postal and logistics solutions often need to generate Mailmark2D symbols, extract their data, and integrate it into tracking systems. The snippet illustrates typical workflows involving these key API classes.
/// Prompt: Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from the decoded result.
/// Tags: mailmark2d, barcode generation, barcode recognition, png, complexbarcodegenerator, barcodereader, complexcodetextreader, mailmark2dcodetext

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark2D barcode, reads it, and decodes its fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark2D barcode, reads it from a memory stream,
    /// and uses ComplexCodetextReader to decode the codetext into a Mailmark2DCodetext object.
    /// </summary>
    static void Main()
    {
        // Create a Mailmark2D codetext with required fields
        var mailmark2d = new Mailmark2DCodetext
        {
            VersionID = "1",               // single‑character string
            InformationTypeID = "0",       // single‑character string
            Class = "1",                   // single‑character string
            RTSFlag = "0",                 // single‑character string
            SupplyChainID = 384224,        // integer
            ItemID = 16563762,             // integer
            DestinationPostCodeAndDPS = "EF61AH8T " // valid postcode+DPs
        };

        // Generate the Mailmark2D barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Read the barcode from the generated image
                using (var reader = new BarCodeReader())
                {
                    reader.SetBarCodeImage(ms);
                    var results = reader.ReadBarCodes();

                    // Process each detected barcode result
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");

                        // Decode the codetext into a Mailmark2DCodetext object
                        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                        if (decoded != null)
                        {
                            Console.WriteLine("Decoded Mailmark2D fields:");
                            Console.WriteLine($"  VersionID: {decoded.VersionID}");
                            Console.WriteLine($"  InformationTypeID: {decoded.InformationTypeID}");
                            Console.WriteLine($"  Class: {decoded.Class}");
                            Console.WriteLine($"  RTSFlag: {decoded.RTSFlag}");
                            Console.WriteLine($"  SupplyChainID: {decoded.SupplyChainID}");
                            Console.WriteLine($"  ItemID: {decoded.ItemID}");
                            Console.WriteLine($"  DestinationPostCodeAndDPS: {decoded.DestinationPostCodeAndDPS}");
                        }
                        else
                        {
                            Console.WriteLine("Failed to decode Mailmark2D codetext.");
                        }
                    }
                }
            }
        }
    }
}