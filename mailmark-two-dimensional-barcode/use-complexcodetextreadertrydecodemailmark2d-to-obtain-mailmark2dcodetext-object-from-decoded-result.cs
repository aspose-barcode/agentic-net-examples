// Title: Decode Mailmark 2D codetext using ComplexCodetextReader
// Description: Demonstrates creating a Mailmark2DCodetext, encoding it to a string, and decoding it back using ComplexCodetextReader.TryDecodeMailmark2D.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category. It showcases the use of Aspose.BarCode.ComplexBarcode classes such as Mailmark2DCodetext and ComplexCodetextReader for generating and parsing Mailmark 2D codetext. Developers working with postal barcode standards often need to construct codetext, encode it into barcodes, and later decode it for validation or data extraction.
// Prompt: Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from the decoded result.
// Tags: mailmark, 2d, barcode, decoding, complexcodetextreader, aspnet, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark2DCodetext, encodes it, and then decodes it back
/// using <see cref="ComplexCodetextReader.TryDecodeMailmark2D"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds a Mailmark2DCodetext, constructs its codetext string,
    /// attempts to decode it, and prints the resulting property values.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a Mailmark2DCodetext instance with sample data.
        // ------------------------------------------------------------
        var mailmark2d = new Mailmark2DCodetext
        {
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 1234567,
            ItemID = 7654321,
            DestinationPostCodeAndDPS = "SW1A1AA00",
            ReturnToSenderPostCode = "SW1A1AA",
            UPUCountryID = "GB",
            CustomerContent = "Sample",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // ------------------------------------------------------------
        // 2. Construct the codetext string that would be encoded in a barcode.
        // ------------------------------------------------------------
        string encoded = mailmark2d.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Decode the constructed codetext back into a Mailmark2DCodetext object.
        // ------------------------------------------------------------
        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(encoded);

        // ------------------------------------------------------------
        // 4. Output the decoded values or an error message.
        // ------------------------------------------------------------
        if (decoded != null)
        {
            Console.WriteLine("Decoded Mailmark2D:");
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
        }
        else
        {
            Console.WriteLine("Failed to decode Mailmark2D codetext.");
        }
    }
}