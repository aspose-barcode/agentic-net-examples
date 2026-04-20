using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    // Model matching the JSON structure for Mailmark2DCodetext
    private class Mailmark2DModel
    {
        public string Class { get; set; }
        public string CustomerContent { get; set; }
        public string DestinationPostCodeAndDPS { get; set; }
        public string InformationTypeID { get; set; }
        public int ItemID { get; set; }
        public string ReturnToSenderPostCode { get; set; }
        public string RTSFlag { get; set; }
        public int SupplyChainID { get; set; }
        public string UPUCountryID { get; set; }
        public string VersionID { get; set; }
        public int DataMatrixType { get; set; } // enum underlying int
        public string CustomerContentEncodeMode { get; set; }
    }

    static void Main()
    {
        // Sample JSON representing a Mailmark2DCodetext instance
        string json = @"{
            ""Class"": ""1"",
            ""CustomerContent"": ""SampleContent"",
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""InformationTypeID"": ""0"",
            ""ItemID"": 16563762,
            ""ReturnToSenderPostCode"": ""SW1A1AA"",
            ""RTSFlag"": ""0"",
            ""SupplyChainID"": 384224,
            ""UPUCountryID"": ""GB"",
            ""VersionID"": ""1"",
            ""DataMatrixType"": 0,
            ""CustomerContentEncodeMode"": ""C40""
        }";

        // Deserialize JSON into the model
        Mailmark2DModel model = JsonSerializer.Deserialize<Mailmark2DModel>(json);
        if (model == null)
        {
            Console.WriteLine("Failed to deserialize JSON.");
            return;
        }

        // Populate Mailmark2DCodetext instance
        var mailmark = new Mailmark2DCodetext
        {
            Class = model.Class,
            CustomerContent = model.CustomerContent,
            DestinationPostCodeAndDPS = model.DestinationPostCodeAndDPS,
            InformationTypeID = model.InformationTypeID,
            ItemID = model.ItemID,
            ReturnToSenderPostCode = model.ReturnToSenderPostCode,
            RTSFlag = model.RTSFlag,
            SupplyChainID = model.SupplyChainID,
            UPUCountryID = model.UPUCountryID,
            VersionID = model.VersionID,
            DataMatrixType = (Mailmark2DType)model.DataMatrixType
            // CustomerContentEncodeMode can be set if needed via enum EncodeMode, omitted for brevity
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Ensure a visible barcode height
            generator.Parameters.Barcode.BarHeight.Point = 10f;
            // Optional: set resolution
            generator.Parameters.Resolution = 300;

            // Save to a PNG file
            string outputPath = "mailmark2d.png";
            using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                generator.Save(stream, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}