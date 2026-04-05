using System;
using Aspose.BarCode.Generation;

public static class ChecksumHelper
{
    // Enables checksum calculation for the provided barcode generator.
    public static void EnableChecksum(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
    }

    // Disables checksum calculation for the provided barcode generator.
    public static void DisableChecksum(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
    }

    // Returns true if checksum is explicitly enabled (Yes), otherwise false.
    public static bool IsChecksumEnabled(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        return generator.Parameters.Barcode.IsChecksumEnabled == Aspose.BarCode.Generation.EnableChecksum.Yes;
    }
}

class Program
{
    static void Main()
    {
        // Example: generate a barcode with checksum enabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
        {
            ChecksumHelper.EnableChecksum(generator);
            Console.WriteLine("Checksum enabled: " + ChecksumHelper.IsChecksumEnabled(generator));
            generator.Save("barcode_enabled.png");
        }

        // Example: generate a barcode with checksum disabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
        {
            ChecksumHelper.DisableChecksum(generator);
            Console.WriteLine("Checksum enabled: " + ChecksumHelper.IsChecksumEnabled(generator));
            generator.Save("barcode_disabled.png");
        }
    }
}