---
name: swiss-qr-code
description: C# examples for Swiss QR Code using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - Swiss QR Code

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Swiss QR Code** category.
This folder contains standalone C# examples for Swiss QR Code operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Required Namespaces

- `using System;`
- `using System.IO;`
- `using Aspose.BarCode.ComplexBarcode;`
- `using Aspose.BarCode.Generation;`
- `using Aspose.BarCode.BarCodeRecognition;`

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [access-creditor-name-iban-amount-and-currency-properties-from-decoded-swissqrcodetext-instance.cs](./access-creditor-name-iban-amount-and-currency-properties-from-decoded-swissqrcodetext-instance.cs) | `SwissQRCodetext` | Access creditor name, IBAN, amount, and currency properties from the decoded SwissQRCodete... |
| [benchmark-time-required-to-decode-swiss-qr-code-images-of-varying-resolutions-using-barcodereader.cs](./benchmark-time-required-to-decode-swiss-qr-code-images-of-varying-resolutions-using-barcodereader.cs) | `SwissQRCodetext`, `QrParameters`, `BarCodeReader` | Benchmark the time required to decode Swiss QR Code images of varying resolutions using Ba... |
| [compare-swiss-qr-code-image-dimensions-and-file-size-using-different-margin-and-module-size-configurations.cs](./compare-swiss-qr-code-image-dimensions-and-file-size-using-different-margin-and-module-size-configurations.cs) | `SwissQRCodetext`, `QrParameters` | Compare Swiss QR Code image dimensions and file size using different margin and module siz... |
| [configure-complexbarcodegenerator-to-output-barcode-image-with-transparent-background-for-ui-component-overlay.cs](./configure-complexbarcodegenerator-to-output-barcode-image-with-transparent-background-for-ui-component-overlay.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Configure ComplexBarcodeGenerator to output a barcode image with transparent background fo... |
| [create-batch-process-to-generate-swiss-qr-code-images-for-invoices-listed-in-csv-file.cs](./create-batch-process-to-generate-swiss-qr-code-images-for-invoices-listed-in-csv-file.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file. |
| [create-console-app-that-reads-qr-code-images-from-folder-and-writes-payment-details-to-json.cs](./create-console-app-that-reads-qr-code-images-from-folder-and-writes-payment-details-to-json.cs) | `QrParameters` | Create a console app that reads QR code images from a folder and writes payment details to... |
| [create-net-method-that-accepts-parameters-and-returns-byte-array-of-swiss-qr-code-png.cs](./create-net-method-that-accepts-parameters-and-returns-byte-array-of-swiss-qr-code-png.cs) | `SwissQRCodetext`, `QrParameters` | Create a .NET method that accepts parameters and returns a byte array of the Swiss QR Code... |
| [customize-barcode-foreground-and-background-colors-via-complexbarcodegenerator-properties-before-generating-image.cs](./customize-barcode-foreground-and-background-colors-via-complexbarcodegenerator-properties-before-generating-image.cs) | `ComplexBarcodeGenerator`, `BarcodeColorParameters`, `BarcodeGenerator` | Customize barcode foreground and background colors via ComplexBarcodeGenerator properties ... |
| [deserialize-xml-of-swissqrcodetext-back-into-object-to-regenerate-qr-code-barcode.cs](./deserialize-xml-of-swissqrcodetext-back-into-object-to-regenerate-qr-code-barcode.cs) |  |  |
| [export-generated-swiss-qr-code-as-jpeg-image-with-adjustable-quality-settings-for-web-usage.cs](./export-generated-swiss-qr-code-as-jpeg-image-with-adjustable-quality-settings-for-web-usage.cs) | `SwissQRCodetext`, `QrParameters`, `QualitySettings`, `BarcodeGenerator` | Export the generated Swiss QR Code as a JPEG image with adjustable quality settings for we... |
| [generate-swiss-qr-code-image-from-payment-details-using-complexbarcodegenerator-and-swissqrcodetext.cs](./generate-swiss-qr-code-image-from-payment-details-using-complexbarcodegenerator-and-swissqrcodetext.cs) | `ComplexBarcodeGenerator`, `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and Swis... |
| [implement-error-handling-for-missing-mandatory-fields-when-constructing-swissqrcodetext-to-prevent-invalid-barcode-gener.cs](./implement-error-handling-for-missing-mandatory-fields-when-constructing-swissqrcodetext-to-prevent-invalid-barcode-gener.cs) | `SwissQRCodetext`, `BarcodeGenerator` | Implement error handling for missing mandatory fields when constructing SwissQRCodetext to... |
| [implement-logging-of-barcode-generation-parameters-and-outcomes-using-net-built-in-logging-framework-for-audit-trails.cs](./implement-logging-of-barcode-generation-parameters-and-outcomes-using-net-built-in-logging-framework-for-audit-trails.cs) | `BarcodeGenerator` | Implement logging of barcode generation parameters and outcomes using .NET built‑in loggin... |
| [implement-parallel-generation-of-swiss-qr-code-barcodes-for-multiple-payment-records-to-boost-performance.cs](./implement-parallel-generation-of-swiss-qr-code-barcodes-for-multiple-payment-records-to-boost-performance.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Implement parallel generation of Swiss QR Code barcodes for multiple payment records to bo... |
| [integrate-barcode-generation-into-background-service-that-processes-payment-requests-from-message-queue.cs](./integrate-barcode-generation-into-background-service-that-processes-payment-requests-from-message-queue.cs) | `BarcodeGenerator` | Integrate barcode generation into a background service that processes payment requests fro... |
| [integrate-swiss-qr-code-generation-into-aspnet-mvc-controller-action-returning-barcode-image-as-http-response.cs](./integrate-swiss-qr-code-generation-into-aspnet-mvc-controller-action-returning-barcode-image-as-http-response.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the bar... |
| [load-saved-qr-code-png-image-into-barcodereader-and-set-decodetype-to-qr-for-recognition.cs](./load-saved-qr-code-png-image-into-barcodereader-and-set-decodetype-to-qr-for-recognition.cs) | `QrParameters`, `DecodeType`, `BarCodeReader` | Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition... |
| [provide-configuration-file-to-map-custom-field-names-to-swissqrcodetext-properties-for-dynamic-barcode-generation.cs](./provide-configuration-file-to-map-custom-field-names-to-swissqrcodetext-properties-for-dynamic-barcode-generation.cs) | `SwissQRCodetext`, `BarcodeGenerator` | Provide a configuration file to map custom field names to SwissQRCodetext properties for d... |
| [read-raw-encoded-text-from-swiss-qr-code-image-and-gracefully-handle-possible-decoding-exceptions.cs](./read-raw-encoded-text-from-swiss-qr-code-image-and-gracefully-handle-possible-decoding-exceptions.cs) | `SwissQRCodetext`, `QrParameters` | Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding e... |
| [save-generated-swiss-qr-code-to-png-file-with-custom-dimensions-and-margins.cs](./save-generated-swiss-qr-code-to-png-file-with-custom-dimensions-and-margins.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Save the generated Swiss QR Code to a PNG file with custom dimensions and margins. |
| [serialize-swissqrcodetext-object-to-xml-for-archival-storage-of-payment-information.cs](./serialize-swissqrcodetext-object-to-xml-for-archival-storage-of-payment-information.cs) | `SwissQRCodetext` | Serialize the SwissQRCodetext object to XML for archival storage of payment information. |
| [set-specific-qr-error-correction-level-for-swiss-qr-code-generation-to-ensure-readability-under-distortion.cs](./set-specific-qr-error-correction-level-for-swiss-qr-code-generation-to-ensure-readability-under-distortion.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Set a specific QR error correction level for Swiss QR Code generation to ensure readabilit... |
| [use-complexbarcodegenerator-to-embed-logo-at-center-of-swiss-qr-code-without-affecting-scannability.cs](./use-complexbarcodegenerator-to-embed-logo-at-center-of-swiss-qr-code-without-affecting-scannability.cs) | `ComplexBarcodeGenerator`, `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Use ComplexBarcodeGenerator to embed a logo at the center of the Swiss QR Code without aff... |
| [use-complexcodetextreadertrydecodeswissqr-to-parse-raw-text-into-swissqrcodetext-object-for-extraction.cs](./use-complexcodetextreadertrydecodeswissqr-to-parse-raw-text-into-swissqrcodetext-object-for-extraction.cs) | `ComplexCodetextReader`, `SwissQRCodetext` | Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object... |
| [use-swissqrbill-class-to-generate-pdf-qr-bill-document-embedding-generated-swiss-qr-code-image.cs](./use-swissqrbill-class-to-generate-pdf-qr-bill-document-embedding-generated-swiss-qr-code-image.cs) | `SwissQRCodetext`, `SwissQRBill`, `QrParameters`, `BarcodeGenerator` | Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR ... |
| [validate-decoded-payment-information-against-iso-20022-constraints-using-custom-net-business-rules.cs](./validate-decoded-payment-information-against-iso-20022-constraints-using-custom-net-business-rules.cs) |  | Validate decoded payment information against ISO 20022 constraints using custom .NET busin... |
| [validate-that-generated-swiss-qr-code-complies-with-swiss-implementation-guidelines-by-checking-required-data-fields.cs](./validate-that-generated-swiss-qr-code-complies-with-swiss-implementation-guidelines-by-checking-required-data-fields.cs) | `SwissQRCodetext`, `QrParameters`, `BarcodeGenerator` | Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by... |
| [write-unit-tests-to-verify-complexbarcodegenerator-produces-correct-qr-code-data-for-given-payment-fields.cs](./write-unit-tests-to-verify-complexbarcodegenerator-produces-correct-qr-code-data-for-given-payment-fields.cs) | `ComplexBarcodeGenerator`, `QrParameters`, `Unit`, `BarcodeGenerator` | Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given... |

## Category Statistics
- Total examples: 28
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `ComplexBarcodeGenerator`
- `SwissQRCodetext`
- `SwissQRBill`
- `Address`
- `ComplexCodetextReader`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-06-28 | Examples: 28
<!-- AUTOGENERATED:END -->
