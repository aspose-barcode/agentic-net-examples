using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace Mailmark2DExample
{
    class Program
    {
        static void Main()
        {
            // Create a Mailmark2D codetext instance and set required fields
            Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext();
            mailmark2D.VersionID = "1";               // Single‑character version identifier
            mailmark2D.InformationTypeID = "0";       // Information type (routing/service code)
            mailmark2D.Class = "1";                   // Class of the item
            mailmark2D.RTSFlag = "0";                 // Return‑to‑sender flag
            mailmark2D.SupplyChainID = 1234567;       // Example supply chain ID (max 7 digits)
            mailmark2D.ItemID = 12345678;             // Example item ID (max 8 digits)
            mailmark2D.DestinationPostCodeAndDPS = "EF61AH8T "; // Known‑valid postcode + DPS

            // Construct the encoded codetext string
            string encodedCodetext = mailmark2D.GetConstructedCodetext();

            // Decode the constructed codetext back to a Mailmark2DCodetext object
            Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(encodedCodetext);

            if (decoded == null)
            {
                Console.WriteLine("Failed to decode the Mailmark2D codetext.");
                return;
            }

            // Extract and display individual fields
            Console.WriteLine("Decoded Mailmark2D fields:");
            Console.WriteLine($"VersionID            : {decoded.VersionID}");
            Console.WriteLine($"InformationTypeID    : {decoded.InformationTypeID}   // Routing / Service code");
            Console.WriteLine($"Class                : {decoded.Class}");
            Console.WriteLine($"RTSFlag              : {decoded.RTSFlag}");
            Console.WriteLine($"SupplyChainID        : {decoded.SupplyChainID}");
            Console.WriteLine($"ItemID               : {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodeAndDPS : {decoded.DestinationPostCodeAndDPS}");
            Console.WriteLine($"ReturnToSenderPostCode     : {decoded.ReturnToSenderPostCode}");
            Console.WriteLine($"CustomerContent            : {decoded.CustomerContent}");
            Console.WriteLine($"UPUCountryID               : {decoded.UPUCountryID}");
        }
    }
}