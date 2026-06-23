using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates merging barcode information from multiple XML state files
/// and writing a summary text file.
/// </summary>
class Program
{
    /// <summary>
    /// Simple model to hold barcode information extracted from XML state files.
    /// </summary>
    class BarcodeInfo
    {
        public string SourceFile { get; set; }
        public string Type { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Creates sample XML files, merges their
    /// barcode data, and writes a summary file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a folder with sample XML state files (in a real scenario
        // these would already exist)
        // --------------------------------------------------------------------
        string stateFolder = Path.Combine(Directory.GetCurrentDirectory(), "StateFiles");
        if (!Directory.Exists(stateFolder))
        {
            Directory.CreateDirectory(stateFolder);
        }

        // --------------------------------------------------------------------
        // Create a few sample XML files for demonstration purposes
        // --------------------------------------------------------------------
        CreateSampleStateFile(Path.Combine(stateFolder, "state1.xml"), new[]
        {
            new BarcodeInfo { Type = "Code128", CodeText = "ABC123" },
            new BarcodeInfo { Type = "QR", CodeText = "https://example.com" }
        });

        CreateSampleStateFile(Path.Combine(stateFolder, "state2.xml"), new[]
        {
            new BarcodeInfo { Type = "EAN13", CodeText = "5901234123457" }
        });

        CreateSampleStateFile(Path.Combine(stateFolder, "state3.xml"), new[]
        {
            new BarcodeInfo { Type = "Code39", CodeText = "CODE39" },
            new BarcodeInfo { Type = "DataMatrix", CodeText = "DM12345" }
        });

        // --------------------------------------------------------------------
        // Merge all XML state files into a single collection
        // --------------------------------------------------------------------
        List<BarcodeInfo> mergedBarcodes = new List<BarcodeInfo>();
        foreach (string xmlPath in Directory.GetFiles(stateFolder, "*.xml"))
        {
            // Verify the file exists before attempting to load it
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Warning: File not found - {xmlPath}");
                continue;
            }

            XDocument doc;
            try
            {
                // Load the XML document; handle any parsing errors gracefully
                doc = XDocument.Load(xmlPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load XML file '{xmlPath}': {ex.Message}");
                continue;
            }

            // Iterate over each <Barcode> element and extract its attributes
            foreach (XElement barcodeElem in doc.Root?.Elements("Barcode") ?? new List<XElement>())
            {
                string type = (string)barcodeElem.Attribute("Type") ?? "Unknown";
                string codeText = (string)barcodeElem.Attribute("CodeText") ?? string.Empty;

                mergedBarcodes.Add(new BarcodeInfo
                {
                    SourceFile = Path.GetFileName(xmlPath),
                    Type = type,
                    CodeText = codeText
                });
            }
        }

        // --------------------------------------------------------------------
        // Output a summary of merged barcode entries to a text file
        // --------------------------------------------------------------------
        string summaryPath = Path.Combine(Directory.GetCurrentDirectory(), "MergedBarcodes.txt");
        using (var writer = new StreamWriter(summaryPath, false))
        {
            writer.WriteLine("SourceFile\tBarcodeType\tCodeText");
            foreach (var info in mergedBarcodes)
            {
                writer.WriteLine($"{info.SourceFile}\t{info.Type}\t{info.CodeText}");
            }
        }

        Console.WriteLine($"Merged {mergedBarcodes.Count} barcode entries into '{summaryPath}'.");
    }

    /// <summary>
    /// Helper method to create a simple XML state file with the given barcode entries.
    /// </summary>
    /// <param name="filePath">Full path where the XML file will be saved.</param>
    /// <param name="barcodes">Array of barcode information to include in the file.</param>
    static void CreateSampleStateFile(string filePath, BarcodeInfo[] barcodes)
    {
        // Build the root <Barcodes> element
        var root = new XElement("Barcodes");
        foreach (var b in barcodes)
        {
            // Create a <Barcode> element with Type and CodeText attributes
            var elem = new XElement("Barcode");
            elem.SetAttributeValue("Type", b.Type);
            elem.SetAttributeValue("CodeText", b.CodeText);
            root.Add(elem);
        }

        // Save the constructed XML document to the specified file
        var doc = new XDocument(root);
        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            doc.Save(stream);
        }
    }
}