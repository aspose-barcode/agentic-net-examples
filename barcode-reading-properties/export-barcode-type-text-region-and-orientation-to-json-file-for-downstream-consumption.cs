using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file paths
        const string barcodePath = "barcode.png";
        const string jsonPath = "barcodeInfo.json";

        // Generate a barcode image and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(barcodePath);
        }

        // List to hold extracted barcode information
        var extractedData = new List<object>();

        // Read the barcode from the saved image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var rect = result.Region.Rectangle;
                var data = new
                {
                    Type = result.CodeTypeName,
                    Text = result.CodeText,
                    Region = new
                    {
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height
                    },
                    Orientation = result.Region.Angle
                };
                extractedData.Add(data);
            }
        }

        // Serialize the extracted information to JSON and write to a file
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(extractedData, jsonOptions);
        File.WriteAllText(jsonPath, json);
    }
}