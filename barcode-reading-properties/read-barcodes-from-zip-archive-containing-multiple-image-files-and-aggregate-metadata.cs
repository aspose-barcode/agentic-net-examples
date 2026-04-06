using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        var samples = new List<(string Text, BaseEncodeType Type)>
        {
            ("12345", EncodeTypes.Code128),
            ("ABCDEF", EncodeTypes.Code39),
            ("https://example.com", EncodeTypes.QR)
        };

        byte[] zipBytes;
        using (var zipStream = new MemoryStream())
        {
            using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                int index = 0;
                foreach (var (text, type) in samples)
                {
                    using (var generator = new BarcodeGenerator(type, text))
                    {
                        using (var imgStream = new MemoryStream())
                        {
                            generator.Save(imgStream, BarCodeImageFormat.Png);
                            imgStream.Position = 0;

                            var entry = zip.CreateEntry($"{index}.png");
                            using (var entryStream = entry.Open())
                            {
                                imgStream.CopyTo(entryStream);
                            }
                        }
                    }
                    index++;
                }
            }
            zipBytes = zipStream.ToArray();
        }

        var aggregated = new List<(string FileName, string CodeText, string CodeType, Rectangle Region)>();

        using (var zipReadStream = new MemoryStream(zipBytes))
        {
            using (var zip = new ZipArchive(zipReadStream, ZipArchiveMode.Read))
            {
                foreach (var entry in zip.Entries)
                {
                    using (var entryStream = entry.Open())
                    using (var imgStream = new MemoryStream())
                    {
                        entryStream.CopyTo(imgStream);
                        imgStream.Position = 0;

                        using (var reader = new BarCodeReader(imgStream, DecodeType.AllSupportedTypes))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                aggregated.Add((entry.Name, result.CodeText, result.CodeTypeName, result.Region.Rectangle));
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Aggregated barcode information:");
        foreach (var info in aggregated)
        {
            Console.WriteLine($"File: {info.FileName}");
            Console.WriteLine($"  Type: {info.CodeType}");
            Console.WriteLine($"  Text: {info.CodeText}");
            Console.WriteLine($"  Region: X={info.Region.X}, Y={info.Region.Y}, Width={info.Region.Width}, Height={info.Region.Height}");
        }
    }
}