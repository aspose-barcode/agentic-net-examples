using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string xmlFilePath = "config_snapshots.xml";

        if (!File.Exists(xmlFilePath))
        {
            using (FileStream fs = File.Create(xmlFilePath))
            {
            }
        }

        for (int i = 1; i <= 3; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Parameters.Barcode.XDimension.Point = 0.5f;

                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    bool success = generator.ExportToXml(stream);
                    if (!success)
                    {
                        Console.WriteLine($"Failed to export configuration snapshot {i}.");
                    }
                }
            }
        }

        Console.WriteLine("Configuration snapshots have been concatenated to " + xmlFilePath);
    }
}