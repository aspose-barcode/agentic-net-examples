using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output directories
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "InputSpecs");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Ensure the input directory exists; if not, create it and add a sample XML spec
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
            string sampleXmlPath = Path.Combine(inputDir, "sample.xml");
            // Minimal XML that Aspose.BarCode can import
            string sampleXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>12345</CodeText>
  <Barcode>
    <XDimension Unit=""Millimeters"">0.5</XDimension>
    <BarHeight Unit=""Millimeters"">10</BarHeight>
  </Barcode>
</BarcodeGenerator>";
            File.WriteAllText(sampleXmlPath, sampleXml);
        }

        // Process each XML file in the input directory
        foreach (string xmlFile in Directory.GetFiles(inputDir, "*.xml"))
        {
            // Import barcode settings from XML
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                // Apply unit conversion: convert XDimension and BarHeight from millimeters to points
                // 1 millimeter = 2.83465 points
                const float mmToPoint = 2.83465f;

                // Convert XDimension if it has a millimeter value
                float xDimMm = generator.Parameters.Barcode.XDimension.Millimeters;
                generator.Parameters.Barcode.XDimension.Point = xDimMm * mmToPoint;

                // Convert BarHeight if it has a millimeter value
                float barHeightMm = generator.Parameters.Barcode.BarHeight.Millimeters;
                generator.Parameters.Barcode.BarHeight.Point = barHeightMm * mmToPoint;

                // Save the generated barcode as PNG
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
                string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".png");
                generator.Save(outputPath);
            }
        }

        Console.WriteLine("Barcode generation completed. Check the OutputBarcodes folder.");
    }
}