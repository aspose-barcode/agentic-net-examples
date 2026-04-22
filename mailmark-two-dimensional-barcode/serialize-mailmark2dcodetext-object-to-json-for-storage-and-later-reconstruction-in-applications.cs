using System;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create a Mailmark2DCodetext instance and set required single‑character properties
        var mailmark2D = new Mailmark2DCodetext
        {
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            RTSFlag = "0"
        };

        // Serialize the object to JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(mailmark2D, jsonOptions);
        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);

        // Deserialize the JSON back to a Mailmark2DCodetext object
        var deserialized = JsonSerializer.Deserialize<Mailmark2DCodetext>(json);
        Console.WriteLine("\nDeserialized object values:");
        Console.WriteLine($"VersionID: {deserialized?.VersionID}");
        Console.WriteLine($"InformationTypeID: {deserialized?.InformationTypeID}");
        Console.WriteLine($"Class: {deserialized?.Class}");
        Console.WriteLine($"RTSFlag: {deserialized?.RTSFlag}");
    }
}