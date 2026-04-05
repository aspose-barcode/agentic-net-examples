using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a temporary XML file with barcode settings
        string xmlPath = "barcodeSettings.xml";
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>Sample123</CodeText>
  <Parameters>
    <Barcode>
      <XDimension>
        <Point>2.5</Point>
      </XDimension>
      <BarHeight>
        <Point>40</Point>
      </BarHeight>
    </Barcode>
    <Resolution>150</Resolution>
  </Parameters>
</BarcodeGenerator>";
        File.WriteAllText(xmlPath, xmlContent);

        // Load the settings from the XML file
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Populate (display) selected properties
            Console.WriteLine($"Encode Type: {generator.BarcodeType}");
            Console.WriteLine($"Code Text: {generator.CodeText}");
            Console.WriteLine($"XDimension (pt): {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"BarHeight (pt): {generator.Parameters.Barcode.BarHeight.Point}");
            Console.WriteLine($"Resolution (dpi): {generator.Parameters.Resolution}");

            // Generate and save the barcode image using the loaded settings
            generator.Save("generated.png");
        }

        // Optional cleanup of the temporary XML file
        // File.Delete(xmlPath);
    }
}