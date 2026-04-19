using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string imagePath = "aztec.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            bool anyFound = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Code Text: {result.CodeText}");

                var aztecExt = result.Extended?.Aztec;
                if (aztecExt != null)
                {
                    var type = aztecExt.GetType();

                    // Layers count (if available)
                    int layersCount = -1;
                    var layersProp = type.GetProperty("LayersCount");
                    if (layersProp != null && layersProp.PropertyType == typeof(int))
                    {
                        layersCount = (int)layersProp.GetValue(aztecExt);
                    }
                    Console.WriteLine($"Layers Count: {(layersCount >= 0 ? layersCount.ToString() : "N/A")}");

                    // Symbol mode (if available)
                    string modeName = "Unknown";
                    bool isCompact = false;
                    var modeProp = type.GetProperty("SymbolMode");
                    if (modeProp != null)
                    {
                        object modeValue = modeProp.GetValue(aztecExt);
                        if (modeValue != null)
                        {
                            modeName = modeValue.ToString();
                            isCompact = modeName.Equals("Compact", StringComparison.OrdinalIgnoreCase);
                        }
                    }

                    Console.WriteLine($"Symbol Mode: {modeName}");
                    Console.WriteLine($"Is Compact Mode: {isCompact}");
                }
                else
                {
                    Console.WriteLine("No Aztec extended parameters available.");
                }

                Console.WriteLine(new string('-', 40));
            }

            if (!anyFound)
            {
                Console.WriteLine("No Aztec barcodes were detected in the image.");
            }
        }
    }
}