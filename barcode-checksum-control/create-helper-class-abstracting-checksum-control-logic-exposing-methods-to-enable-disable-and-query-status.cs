using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Helper class to manage checksum settings for a <see cref="BarcodeGenerator"/>.
/// </summary>
class ChecksumHelper
{
    /// <summary>
    /// Enables checksum for the given barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance.</param>
    public static void Enable(BarcodeGenerator generator)
    {
        // Validate argument
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Set checksum flag to Yes
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
    }

    /// <summary>
    /// Disables checksum for the given barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance.</param>
    public static void Disable(BarcodeGenerator generator)
    {
        // Validate argument
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Set checksum flag to No
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
    }

    /// <summary>
    /// Returns <c>true</c> if checksum is enabled; otherwise, <c>false</c>.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance.</param>
    /// <returns>Boolean indicating checksum status.</returns>
    public static bool GetStatus(BarcodeGenerator generator)
    {
        // Validate argument
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Compare current setting with Yes
        return generator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
    }
}

/// <summary>
/// Demonstrates enabling and disabling checksum on a barcode generator and saving the image.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // Create a Code39FullASCII barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
        {
            // Initially disable checksum
            ChecksumHelper.Disable(generator);
            Console.WriteLine("Checksum enabled? " + ChecksumHelper.GetStatus(generator));

            // Enable checksum
            ChecksumHelper.Enable(generator);
            Console.WriteLine("Checksum enabled? " + ChecksumHelper.GetStatus(generator));

            // Save the barcode image to a file
            generator.Save("barcode.png");
            Console.WriteLine("Barcode saved as barcode.png");
        }
    }
}