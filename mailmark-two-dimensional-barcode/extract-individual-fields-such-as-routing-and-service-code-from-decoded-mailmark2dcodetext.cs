// Title: Extract fields from Mailmark2D barcode codetext
// Description: Demonstrates creating a Mailmark2D codetext, generating a barcode image, decoding the codetext, and extracting individual fields such as routing and service code.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of Aspose.BarCode.ComplexBarcode classes (Mailmark2DCodetext, ComplexBarcodeGenerator, ComplexCodetextReader) for generating and recognizing Mailmark2D barcodes. Typical scenarios include postal automation, logistics tracking, and mail sorting where developers need to encode, decode, and manipulate individual data elements within a Mailmark2D symbol.
// Prompt: Extract individual fields such as routing and service code from the decoded Mailmark2DCodetext.
// Tags: mailmark2d, barcode, extraction, complexbarcode, generation, recognition, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Mailmark2D barcode, decodes it, and extracts its individual data fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the full lifecycle: construct codetext, generate barcode, decode, and display fields.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a Mailmark2D codetext object and populate its fields.
        // ------------------------------------------------------------
        var mailmark2d = new Mailmark2DCodetext
        {
            InformationTypeID = "0",                     // Domestic Sorted & Unsorted (routing)
            VersionID = "1",
            Class = "1",                                 // Example class (service code)
            RTSFlag = "0",
            DestinationPostCodeAndDPS = "EC1A1BB",       // Sample postcode + DPS
            SupplyChainID = 1234567,
            ItemID = 7654321,
            UPUCountryID = "GBR"
        };

        // ------------------------------------------------------------
        // 2. Construct the raw codetext string from the populated object.
        // ------------------------------------------------------------
        string constructedCodetext = mailmark2d.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. (Optional) Generate a barcode image to demonstrate full lifecycle.
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // The MemoryStream now contains PNG data; it is not used further in this example.
            }
        }

        // ------------------------------------------------------------
        // 4. Decode the constructed codetext back into a Mailmark2DCodetext object.
        // ------------------------------------------------------------
        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(constructedCodetext);
        if (decoded == null)
        {
            Console.WriteLine("Failed to decode Mailmark2D codetext.");
            return;
        }

        // ------------------------------------------------------------
        // 5. Extract and display individual fields from the decoded object.
        // ------------------------------------------------------------
        Console.WriteLine("Decoded Mailmark2D fields:");
        Console.WriteLine($"Information Type ID (Routing): {decoded.InformationTypeID}");
        Console.WriteLine($"Version ID: {decoded.VersionID}");
        Console.WriteLine($"Class (Service Code): {decoded.Class}");
        Console.WriteLine($"RTS Flag: {decoded.RTSFlag}");
        Console.WriteLine($"Destination Postcode + DPS: {decoded.DestinationPostCodeAndDPS}");
        Console.WriteLine($"Supply Chain ID: {decoded.SupplyChainID}");
        Console.WriteLine($"Item ID: {decoded.ItemID}");
        Console.WriteLine($"UPU Country ID: {decoded.UPUCountryID}");
    }
}