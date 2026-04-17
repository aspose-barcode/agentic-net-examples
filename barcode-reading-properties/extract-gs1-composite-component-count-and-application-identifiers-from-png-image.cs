using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine image path (first argument or default)
        string imagePath = args.Length > 0 ? args[0] : "gs1composite.png";

        // Validate file existence
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Read GS1 Composite barcodes from the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No GS1 Composite barcode detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                // Extended parameters contain separate 1D and 2D parts
                var ext = result.Extended.GS1CompositeBar;

                string oneD = ext.OneDCodeText ?? string.Empty;
                string twoD = ext.TwoDCodeText ?? string.Empty;

                Console.WriteLine("=== Detected GS1 Composite Barcode ===");
                Console.WriteLine($"1D Component Text: {oneD}");
                Console.WriteLine($"2D Component Text: {twoD}");

                // Combine both parts for AI extraction
                string combined = $"{oneD}{twoD}";

                // Find all Application Identifiers (AIs) in the format (nn) or (nnn) etc.
                var matches = Regex.Matches(combined, @"\(\d{2,4}\)");
                int aiCount = matches.Count;

                Console.WriteLine($"Total Application Identifiers (AIs) found: {aiCount}");
                if (aiCount > 0)
                {
                    Console.WriteLine("List of AIs:");
                    foreach (Match m in matches)
                    {
                        Console.WriteLine($"  {m.Value}");
                    }
                }

                // Component count: GS1 Composite always has two components (1D + 2D)
                Console.WriteLine("Component Count: 2 (1D linear + 2D)");
                Console.WriteLine();
            }
        }
    }
}