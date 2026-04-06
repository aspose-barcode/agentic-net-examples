using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PNG image containing the GS1 Composite Bar barcode.
        string imagePath = "gs1composite.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the reader for GS1 Composite Bar type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            // Read all barcodes from the image.
            var results = reader.ReadBarCodes();

            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            foreach (var result in results)
            {
                // Access extended parameters specific to GS1 Composite Bar.
                var gs1Ext = result.Extended.GS1CompositeBar;

                // Determine how many components (1D and 2D) are present.
                int componentCount = 0;
                if (!string.IsNullOrEmpty(gs1Ext.OneDCodeText))
                    componentCount++;
                if (!string.IsNullOrEmpty(gs1Ext.TwoDCodeText))
                    componentCount++;

                Console.WriteLine($"Component count: {componentCount}");

                // Combine the 1D and 2D code texts to extract Application Identifiers (AIs).
                string combinedCodeText = (gs1Ext.OneDCodeText ?? string.Empty) + (gs1Ext.TwoDCodeText ?? string.Empty);

                // GS1 AIs are enclosed in parentheses, e.g., (01), (21), etc.
                var aiMatches = Regex.Matches(combinedCodeText, @"\(\d{2,4}\)")
                                    .Cast<Match>()
                                    .Select(m => m.Value)
                                    .Distinct()
                                    .ToList();

                Console.WriteLine("Application Identifiers found:");
                foreach (var ai in aiMatches)
                {
                    Console.WriteLine(ai);
                }
            }
        }
    }
}