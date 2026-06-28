using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates decoding of a Mailmark 2D codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a sample encoded Mailmark 2D codetext.
        // Replace this placeholder with an actual encoded value when available.
        string encodedCodetext = "SampleEncodedCodetext";

        // Attempt to decode the provided codetext into a Mailmark2DCodetext object.
        Mailmark2DCodetext mailmark2d = ComplexCodetextReader.TryDecodeMailmark2D(encodedCodetext);

        // Check if decoding was successful.
        if (mailmark2d != null)
        {
            // Output each decoded property to the console.
            Console.WriteLine("Decoded Mailmark2D codetext:");
            Console.WriteLine($"VersionID: {mailmark2d.VersionID}");
            Console.WriteLine($"InformationTypeID: {mailmark2d.InformationTypeID}");
            Console.WriteLine($"Class: {mailmark2d.Class}");
            Console.WriteLine($"RTSFlag: {mailmark2d.RTSFlag}");
            Console.WriteLine($"DestinationPostCodeAndDPS: {mailmark2d.DestinationPostCodeAndDPS}");
        }
        else
        {
            // Inform the user that decoding failed.
            Console.WriteLine("Failed to decode Mailmark2D codetext.");
        }
    }
}