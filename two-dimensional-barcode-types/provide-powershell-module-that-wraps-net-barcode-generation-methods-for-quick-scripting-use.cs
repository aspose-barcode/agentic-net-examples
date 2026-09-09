// Title: Generate PowerShell Module for Aspose.BarCode
// Description: Demonstrates how to programmatically create a PowerShell .psm1 module that wraps Aspose.BarCode .NET generation methods, enabling quick scripting of barcode creation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to automate barcode production in scripts or CI pipelines; wrapping the .NET API in a PowerShell module provides a convenient, reusable interface for such scenarios. The snippet illustrates building module content, handling symbology resolution, and exporting a New-Barcode function.
// Prompt: Provide a PowerShell module that wraps .NET barcode generation methods for quick scripting use.
// Tags: barcode, symbology, generation, powershell, module, aspose, .net, png, encode-types

using System;
using System.IO;
using System.Text;

using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Creates a PowerShell module that encapsulates Aspose.BarCode generation functionality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds and writes the PowerShell module file.
    /// </summary>
    static void Main()
    {
        // Define the PowerShell module file name and full path
        string moduleFileName = "BarcodeModule.psm1";
        string modulePath = Path.Combine(Directory.GetCurrentDirectory(), moduleFileName);

        // Use a StringBuilder to compose the .psm1 script content
        StringBuilder sb = new StringBuilder();

        // Module header comments for PowerShell users
        sb.AppendLine("# PowerShell module wrapping Aspose.BarCode generation");
        sb.AppendLine("# Requires Aspose.BarCode for .NET assembly to be available in the PowerShell session");
        sb.AppendLine();

        // Helper function: resolves a symbology name (string) to the corresponding EncodeTypes enum value
        sb.AppendLine("function Resolve-EncodeType");
        sb.AppendLine("{");
        sb.AppendLine("    param([string]$SymbologyName)");
        sb.AppendLine("    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($SymbologyName)");
        sb.AppendLine("    if ($null -eq $field) {");
        sb.AppendLine("        Write-Error \"Unknown symbology: $SymbologyName\"");
        sb.AppendLine("        return $null");
        sb.AppendLine("    }");
        sb.AppendLine("    return $field.GetValue($null)");
        sb.AppendLine("}");
        sb.AppendLine();

        // Main function exposed to PowerShell: creates a barcode image file
        sb.AppendLine("function New-Barcode");
        sb.AppendLine("{");
        sb.AppendLine("    [CmdletBinding()]");
        sb.AppendLine("    param(");
        sb.AppendLine("        [Parameter(Mandatory=$true)][string]$Symbology,");
        sb.AppendLine("        [Parameter(Mandatory=$true)][string]$CodeText,");
        sb.AppendLine("        [Parameter(Mandatory=$true)][string]$OutputPath");
        sb.AppendLine("    )");
        sb.AppendLine();
        sb.AppendLine("    $encodeType = Resolve-EncodeType -SymbologyName $Symbology");
        sb.AppendLine("    if ($null -eq $encodeType) { return }");
        sb.AppendLine();
        sb.AppendLine("    try");
        sb.AppendLine("    {");
        sb.AppendLine("        $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $CodeText)");
        sb.AppendLine("        # Set foreground (barcode) and background colors");
        sb.AppendLine("        $generator.Parameters.Barcode.BarColor = [Aspose.Drawing.Color]::Black");
        sb.AppendLine("        $generator.Parameters.BackColor = [Aspose.Drawing.Color]::White");
        sb.AppendLine();
        sb.AppendLine("        # Save the generated barcode as a PNG file");
        sb.AppendLine("        $generator.Save($OutputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)");
        sb.AppendLine("        Write-Host \"Barcode saved to $OutputPath\"");
        sb.AppendLine("    }");
        sb.AppendLine("    catch");
        sb.AppendLine("    {");
        sb.AppendLine("        Write-Error $_.Exception.Message");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();

        // Export the New-Barcode function so it is available when the module is imported
        sb.AppendLine("Export-ModuleMember -Function New-Barcode");

        // Write the composed script to the .psm1 file on disk
        try
        {
            File.WriteAllText(modulePath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"PowerShell module created at: {modulePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create module file: {ex.Message}");
        }
    }
}