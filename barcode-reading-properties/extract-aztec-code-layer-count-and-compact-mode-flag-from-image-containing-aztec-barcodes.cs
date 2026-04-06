using System;
using System.Reflection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create an Aztec barcode with known parameters
        string imagePath = "aztec.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec))
        {
            generator.CodeText = "Sample";
            // Set compact mode and layers count
            generator.Parameters.Barcode.Aztec.AztecSymbolMode = AztecSymbolMode.Compact;
            generator.Parameters.Barcode.Aztec.LayersCount = 3;
            generator.Save(imagePath);
        }

        // Read the barcode and extract layer count and compact mode flag
        using (var reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Code type: {result.CodeTypeName}");
                Console.WriteLine($"Code text: {result.CodeText}");

                // Use reflection to get properties that may not be exposed directly
                var aztecExt = result.Extended.Aztec;
                Type extType = aztecExt.GetType();

                PropertyInfo layersProp = extType.GetProperty("LayersCount");
                PropertyInfo compactProp = extType.GetProperty("IsCompact");

                if (layersProp != null)
                {
                    object layersValue = layersProp.GetValue(aztecExt);
                    Console.WriteLine($"Layers count: {layersValue}");
                }
                else
                {
                    Console.WriteLine("Layers count not available.");
                }

                if (compactProp != null)
                {
                    object compactValue = compactProp.GetValue(aztecExt);
                    Console.WriteLine($"Compact mode: {compactValue}");
                }
                else
                {
                    Console.WriteLine("Compact mode flag not available.");
                }
            }
        }
    }
}