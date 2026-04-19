using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Conversion factor from millimeters to points (1 mm = 2.83465 pt)
    const float MmToPt = 2.83465f;

    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesInput");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesOutput");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample XML if none exist (so the example runs end‑to‑end)
        string[] existingXml = Directory.GetFiles(inputFolder, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "Sample1.xml");
            File.WriteAllText(sampleXmlPath,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>Sample123</CodeText>
  <Parameters>
    <ImageWidth>
      <Millimeters>50</Millimeters>
    </ImageWidth>
    <ImageHeight>
      <Millimeters>20</Millimeters>
    </ImageHeight>
    <Barcode>
      <XDimension>
        <Millimeters>0.5</Millimeters>
      </XDimension>
      <BarHeight>
        <Millimeters>10</Millimeters>
      </BarHeight>
      <Padding>
        <Left>
          <Millimeters>2</Millimeters>
        </Left>
        <Top>
          <Millimeters>2</Millimeters>
        </Top>
        <Right>
          <Millimeters>2</Millimeters>
        </Right>
        <Bottom>
          <Millimeters>2</Millimeters>
        </Bottom>
      </Padding>
    </Barcode>
  </Parameters>
</BarcodeGenerator>");
        }

        // Process each XML file
        foreach (string xmlFile in Directory.GetFiles(inputFolder, "*.xml"))
        {
            try
            {
                // Load generator from XML
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    // Apply unit conversions (mm -> pt) for supported properties
                    ConvertMillimetersToPoints(generator.Parameters.ImageWidth);
                    ConvertMillimetersToPoints(generator.Parameters.ImageHeight);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.XDimension);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.BarHeight);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.Padding.Left);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.Padding.Top);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.Padding.Right);
                    ConvertMillimetersToPoints(generator.Parameters.Barcode.Padding.Bottom);

                    // Determine output file name
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                    // Save barcode as PNG
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlFile}': {ex.Message}");
            }
        }
    }

    // Helper to convert Millimeters to Points if Millimeters value is set (>0)
    static void ConvertMillimetersToPoints(Aspose.BarCode.Generation.Unit unit)
    {
        if (unit == null) return;
        // The Unit class exposes Millimeters and Point members.
        // If Millimeters is set (non‑zero), compute the equivalent points.
        if (unit.Millimeters > 0f)
        {
            unit.Point = unit.Millimeters * MmToPt;
        }
    }
}