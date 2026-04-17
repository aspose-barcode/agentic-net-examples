using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        string symbologyName = "Code128";
        string codeText = "1234567890";
        bool forceChecksumVisibility = false;

        // Parse command‑line arguments if provided
        if (args.Length > 0)
            symbologyName = args[0];
        if (args.Length > 1)
            codeText = args[1];
        if (args.Length > 2)
            bool.TryParse(args[2], out forceChecksumVisibility);

        // Resolve symbology name to a BaseEncodeType
        if (!EncodeTypes.TryParse(symbologyName, out BaseEncodeType encodeType))
        {
            Console.WriteLine($"Unsupported symbology '{symbologyName}'. Using default Code128.");
            encodeType = EncodeTypes.Code128;
        }

        // Create the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum generation for symbologies where it is optional
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Force checksum text to be shown regardless of defaults
            if (forceChecksumVisibility)
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image
            string outputFile = "output.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode saved to '{outputFile}'.");
            Console.WriteLine($"Symbology: {encodeType.GetType().Name}, CodeText: {codeText}");
            Console.WriteLine($"Checksum visibility forced: {forceChecksumVisibility}");
        }
    }
}