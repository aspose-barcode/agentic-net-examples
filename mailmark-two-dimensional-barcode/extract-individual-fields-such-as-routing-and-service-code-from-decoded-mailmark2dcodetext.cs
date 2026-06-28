using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates encoding and decoding of a Mailmark2D codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a sample Mailmark2D object, encodes it,
    /// decodes the resulting string, and prints all fields to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a sample Mailmark2DCodetext object with known values.
        // --------------------------------------------------------------------
        var mailmark2d = new Mailmark2DCodetext
        {
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",                     // Service code
            RTSFlag = "0",
            SupplyChainID = 384224,          // Routing code
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T "
        };

        // --------------------------------------------------------------------
        // 2. Construct the encoded codetext string from the object.
        // --------------------------------------------------------------------
        string encodedCodetext = mailmark2d.GetConstructedCodetext();

        // --------------------------------------------------------------------
        // 3. Decode the codetext back to a Mailmark2DCodetext object.
        // --------------------------------------------------------------------
        Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(encodedCodetext);

        // --------------------------------------------------------------------
        // 4. Verify decoding succeeded; if not, report the failure and exit.
        // --------------------------------------------------------------------
        if (decoded == null)
        {
            Console.WriteLine("Failed to decode Mailmark2D codetext.");
            return;
        }

        // --------------------------------------------------------------------
        // 5. Extract and display individual fields from the decoded object.
        // --------------------------------------------------------------------
        Console.WriteLine("Decoded Mailmark2D fields:");
        Console.WriteLine($"VersionID: {decoded.VersionID}");
        Console.WriteLine($"InformationTypeID: {decoded.InformationTypeID}");
        Console.WriteLine($"Class (Service Code): {decoded.Class}");
        Console.WriteLine($"RTSFlag: {decoded.RTSFlag}");
        Console.WriteLine($"SupplyChainID (Routing Code): {decoded.SupplyChainID}");
        Console.WriteLine($"ItemID: {decoded.ItemID}");
        Console.WriteLine($"DestinationPostCodeAndDPS: {decoded.DestinationPostCodeAndDPS}");
        Console.WriteLine($"ReturnToSenderPostCode: {decoded.ReturnToSenderPostCode}");
        Console.WriteLine($"UPUCountryID: {decoded.UPUCountryID}");
        Console.WriteLine($"CustomerContent: {decoded.CustomerContent}");
        Console.WriteLine($"CustomerContentEncodeMode: {decoded.CustomerContentEncodeMode}");
        Console.WriteLine($"DataMatrixType: {decoded.DataMatrixType}");
    }
}