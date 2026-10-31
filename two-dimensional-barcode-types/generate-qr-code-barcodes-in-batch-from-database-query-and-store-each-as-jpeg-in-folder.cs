using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulated database records: each tuple contains (Id, CodeText)
        List<(int Id, string CodeText)> records = new List<(int, string)>
        {
            (1, "https://example.com/item/1"),
            (2, "https://example.com/item/2"),
            (3, "https://example.com/item/3"),
            (4, "https://example.com/item/4"),
            (5, "https://example.com/item/5")
        };

        // In a real scenario, replace the above list with a DB query, e.g.:
        // using (SqlConnection conn = new SqlConnection(connectionString))
        // {
        //     conn.Open();
        //     using (SqlCommand cmd = new SqlCommand("SELECT Id, CodeText FROM Barcodes", conn))
        //     using (SqlDataReader reader = cmd.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             records.Add((reader.GetInt32(0), reader.GetString(1)));
        //         }
        //     }
        // }

        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving QR codes to: {outputFolder}");

        foreach (var record in records)
        {
            string fileName = $"qr_{record.Id}.jpg";
            string filePath = Path.Combine(outputFolder, fileName);

            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, record.CodeText))
                {
                    // Optional: adjust appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Resolution = 300f;
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                }

                Console.WriteLine($"Generated QR code for Id {record.Id} -> {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating QR for Id {record.Id}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}