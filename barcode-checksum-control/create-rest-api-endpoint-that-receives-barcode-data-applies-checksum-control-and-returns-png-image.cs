// Title: Generate Barcode PNG with Checksum via Simulated REST Endpoint
// Description: Demonstrates how to generate a barcode image with checksum control using Aspose.BarCode and return it as a PNG, simulating a REST API request.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image output formats. Typical use cases include creating barcode images on-the-fly for web services, validating data integrity with checksum options, and delivering PNG streams to clients. Developers often need to map symbology names to encode types, enable checksum, and return the image in a format suitable for HTTP responses.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: barcode generation checksum png rest api aspose.barcode aspose.drawing

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simulates a REST API endpoint that generates a barcode PNG with optional checksum control.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that processes a simulated request, generates the barcode, and outputs the PNG image.
    /// </summary>
    static void Main()
    {
        // ---------- Simulated request payload ----------
        // In a real REST API these values would come from the HTTP request body.
        string symbologyName = "Code39Extended"; // e.g., "Code39Extended", "Code128", "SwissPostParcel"
        string data = "123456";
        bool enableChecksum = true;

        // ---------- Resolve symbology name to an EncodeType ----------
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
        if (encodeType == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // ---------- Generate barcode with checksum control ----------
        using (var generator = new BarcodeGenerator(encodeType, data))
        {
            // Enable or disable checksum based on the request flag.
            generator.Parameters.Barcode.IsChecksumEnabled = enableChecksum ? EnableChecksum.Yes : EnableChecksum.No;
            generator.Parameters.Barcode.ChecksumAlwaysShow = enableChecksum;

            // ---------- Save barcode to a memory stream ----------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reads.

                // ---------- Write PNG to a temporary file (simulating file response) ----------
                string outputPath = Path.Combine(Path.GetTempPath(), "barcode_output.png");
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode image saved to: {outputPath}");

                // ---------- Optionally output Base64 string of the PNG ----------
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Base64 PNG:");
                Console.WriteLine(base64);
            }
        }
    }

    /// <summary>
    /// Maps a symbology name (string) to the corresponding <see cref="BaseEncodeType"/> using reflection.
    /// Returns null if the name does not match any known encode type.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology.</param>
    /// <returns>The matching <see cref="BaseEncodeType"/> or null.</returns>
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        // Look for a public static field on EncodeTypes that matches the provided name.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            return null;

        // Return the field value cast to BaseEncodeType.
        return field.GetValue(null) as BaseEncodeType;
    }
}