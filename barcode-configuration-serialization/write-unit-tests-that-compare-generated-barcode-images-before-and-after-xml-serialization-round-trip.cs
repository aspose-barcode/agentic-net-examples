// Title: Barcode XML Serialization Round‑Trip Image Comparison
// Description: Generates barcodes, exports their configuration to XML, re‑imports it, regenerates the images, and verifies that the original and round‑tripped images are byte‑identical.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It demonstrates using BarcodeGenerator, ExportToXml, and ImportFromXml to persist barcode settings, a common scenario for developers who need to store or transfer barcode configurations across systems while ensuring visual fidelity.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, symbology, xml, serialization, image comparison, aspose.barcode, generation, testing

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates round‑trip XML serialization of barcode configurations and validates image equality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs a set of barcode round‑trip tests and reports results.
    /// </summary>
    static void Main()
    {
        // Define test cases: each tuple contains a symbology name (matching EncodeTypes) and sample code text.
        var testCases = new List<(string Symbology, string CodeText)>
        {
            ("Code128", "ABC123"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DMTest")
        };

        var failedTests = new List<string>();

        // Execute each test case and collect any failures.
        foreach (var (symbology, codeText) in testCases)
        {
            try
            {
                if (!RunRoundTripTest(symbology, codeText))
                {
                    failedTests.Add($"{symbology} ({codeText})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during test {symbology}: {ex.Message}");
                failedTests.Add($"{symbology} ({codeText})");
            }
        }

        // Summarize results.
        if (failedTests.Count == 0)
        {
            Console.WriteLine("ALL TESTS PASSED");
        }
        else
        {
            Console.WriteLine($"FAILED: {failedTests.Count} test(s) failed.");
            foreach (var f in failedTests)
            {
                Console.WriteLine($" - {f}");
            }
        }
    }

    // Returns true if the image before and after XML round‑trip are identical.
    static bool RunRoundTripTest(string symbologyName, string codeText)
    {
        // Resolve the EncodeTypes member that corresponds to the requested symbology.
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);

        // Generate the original barcode and capture its PNG bytes.
        byte[] originalImage;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting a property (optional).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                originalImage = ms.ToArray();
            }

            // Export the generator configuration to XML (in‑memory).
            string xml;
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0;
                using (var sr = new StreamReader(xmlStream, leaveOpen: true))
                {
                    xml = sr.ReadToEnd();
                }
            }

            // Import the configuration from XML and generate a second barcode.
            byte[] roundTripImage;
            using (var xmlInput = new MemoryStream())
            {
                using (var sw = new StreamWriter(xmlInput, leaveOpen: true))
                {
                    sw.Write(xml);
                    sw.Flush();
                    xmlInput.Position = 0;
                }

                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlInput))
                {
                    using (var ms2 = new MemoryStream())
                    {
                        importedGenerator.Save(ms2, BarCodeImageFormat.Png);
                        roundTripImage = ms2.ToArray();
                    }
                }
            }

            // Compare the two byte arrays for exact equality.
            return ImagesAreEqual(originalImage, roundTripImage);
        }
    }

    // Resolve a BaseEncodeType from the EncodeTypes class using reflection.
    static BaseEncodeType ResolveEncodeType(string name)
    {
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            throw new ArgumentException($"EncodeTypes does not contain a member named '{name}'.");
        }
        return (BaseEncodeType)field.GetValue(null);
    }

    // Simple byte‑wise comparison of two images.
    static bool ImagesAreEqual(byte[] img1, byte[] img2)
    {
        if (img1.Length != img2.Length)
            return false;
        for (int i = 0; i < img1.Length; i++)
        {
            if (img1[i] != img2[i])
                return false;
        }
        return true;
    }
}