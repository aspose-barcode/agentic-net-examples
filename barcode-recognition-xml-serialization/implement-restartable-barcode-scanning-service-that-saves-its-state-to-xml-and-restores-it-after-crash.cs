// Title: Restartable barcode scanning service with XML state persistence
// Description: Demonstrates generating, reading, and persisting progress of barcode processing so the service can resume after a crash.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: barcode symbology, generation, recognition, xml persistence, restartable service

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates barcodes, reads them back, and persists processing state to allow restart after a failure.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode processing loop and manages state persistence.
    /// </summary>
    static void Main()
    {
        const string stateFile = "state.xml";

        // Load previously processed indices from the state file (if it exists).
        var processed = new HashSet<int>();
        if (File.Exists(stateFile))
        {
            try
            {
                var doc = XDocument.Load(stateFile);
                // Retrieve each stored index element and add it to the processed set.
                foreach (var elem in doc.Root?.Element("ProcessedIndices")?.Elements("Index") ?? Enumerable.Empty<XElement>())
                {
                    if (int.TryParse(elem.Value, out int idx))
                        processed.Add(idx);
                }
            }
            catch
            {
                // If the state file is corrupted, discard its contents and start fresh.
                processed.Clear();
            }
        }

        // Sample list of barcode texts to be generated and processed.
        var codes = new List<string>
        {
            "ABC123",
            "XYZ789",
            "123456",
            "HELLO",
            "WORLD"
        };

        // Iterate over each barcode text, skipping those already processed.
        for (int i = 0; i < codes.Count; i++)
        {
            if (processed.Contains(i))
                continue; // Skip index already handled in a previous run.

            string imagePath = $"barcode_{i}.png";

            // Generate a barcode image for the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                generator.Save(imagePath);
            }

            // Read the generated barcode image and output its details.
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Index {i}: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }

            // Mark this index as processed and persist the updated state to XML.
            processed.Add(i);
            var stateDoc = new XDocument(
                new XElement("State",
                    new XElement("ProcessedIndices",
                        processed.Select(idx => new XElement("Index", idx))
                    )
                )
            );
            stateDoc.Save(stateFile);
        }

        // All barcode items have been processed successfully.
    }
}