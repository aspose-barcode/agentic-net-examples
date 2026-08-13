// Title: Extract fields from Mailmark2D barcode codetext
// Description: Demonstrates constructing a Mailmark2D codetext, decoding it with Aspose.BarCode, and extracting individual fields such as routing and service codes.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode category, showcasing the Mailmark2D symbology. It uses the Mailmark2DCodetext class to build a codetext string, ComplexCodetextReader to decode it, and then accesses the parsed properties. Developers working with postal automation, logistics, or any scenario that requires reading Mailmark2D barcodes will find this pattern useful for extracting routing, service, and custom data from decoded objects.
/// Prompt: Extract individual fields such as routing and service code from the decoded Mailmark2DCodetext.
/// Tags: mailmark2d, barcode, decoding, extraction, aspose.barcode, complexbarcode, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that builds a Mailmark2D codetext, decodes it, and prints each field.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Constructs a Mailmark2D codetext, decodes it, and displays the individual components.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a Mailmark2DCodetext instance with known sample values.
        // ------------------------------------------------------------
        var mailmark = new Mailmark2DCodetext
        {
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 1234567,
            ItemID = 7654321,
            DestinationPostCodeAndDPS = "SW1A1AA00",
            ReturnToSenderPostCode = "SW1A1AA",
            UPUCountryID = "GBR",
            CustomerContent = "SampleContent",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // ------------------------------------------------------------
        // 2. Generate the codetext string that would be encoded in the barcode.
        // ------------------------------------------------------------
        string constructedCodetext = mailmark.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Decode the generated codetext back into a Mailmark2DCodetext object.
        // ------------------------------------------------------------
        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(constructedCodetext);
        if (decoded == null)
        {
            Console.WriteLine("Failed to decode Mailmark2D codetext.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Extract and display each individual field from the decoded object.
        // ------------------------------------------------------------
        Console.WriteLine($"VersionID: {decoded.VersionID}");
        Console.WriteLine($"InformationTypeID: {decoded.InformationTypeID}");
        Console.WriteLine($"Class: {decoded.Class}");
        Console.WriteLine($"RTSFlag: {decoded.RTSFlag}");
        Console.WriteLine($"SupplyChainID: {decoded.SupplyChainID}");
        Console.WriteLine($"ItemID: {decoded.ItemID}");
        Console.WriteLine($"DestinationPostCodeAndDPS: {decoded.DestinationPostCodeAndDPS}");
        Console.WriteLine($"ReturnToSenderPostCode: {decoded.ReturnToSenderPostCode}");
        Console.WriteLine($"UPUCountryID: {decoded.UPUCountryID}");
        Console.WriteLine($"CustomerContent: {decoded.CustomerContent}");
        Console.WriteLine($"CustomerContentEncodeMode: {decoded.CustomerContentEncodeMode}");
    }
}