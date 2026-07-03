---
name: barcode-reading-properties
description: C# examples for Barcode Reading Properties using Aspose.BarCode for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Barcode Reading Properties

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Barcode Reading Properties** category.
This folder contains standalone C# examples for Barcode Reading Properties operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Required Namespaces

- `using System;`
- `using System.IO;`
- `using Aspose.BarCode.BarCodeRecognition;`
- `using Aspose.BarCode.Generation;`

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [access-pdf417-extended-parameters-to-check-if-barcode-is-linked-to-another-segment.cs](./access-pdf417-extended-parameters-to-check-if-barcode-is-linked-to-another-segment.cs) | `Pdf417Parameters` | Access PDF417 extended parameters to check if the barcode is linked to another segment. |
| [adjust-dpi-settings-when-loading-images-to-ensure-accurate-barcode-region-detection.cs](./adjust-dpi-settings-when-loading-images-to-ensure-accurate-barcode-region-detection.cs) |  | Adjust DPI settings when loading images to ensure accurate barcode region detection. |
| [batch-process-folder-of-images-to-extract-barcode-metadata-and-write-results-to-csv.cs](./batch-process-folder-of-images-to-extract-barcode-metadata-and-write-results-to-csv.cs) |  | Batch process a folder of images to extract barcode metadata and write results to CSV. |
| [capture-barcode-region-as-rectangle-object-and-convert-coordinates-to-absolute-pixel-values.cs](./capture-barcode-region-as-rectangle-object-and-convert-coordinates-to-absolute-pixel-values.cs) |  | Capture barcode region as a rectangle object and convert coordinates to absolute pixel val... |
| [check-1d-barcode-checksum-status-for-code128-barcodes-detected-in-bmp-file.cs](./check-1d-barcode-checksum-status-for-code128-barcodes-detected-in-bmp-file.cs) |  | Check 1D barcode checksum status for Code128 barcodes detected in a BMP file. |
| [check-pdf417-isreaderinitialization-flag-to-determine-if-barcode-contains-initialization-instructions-for-scanner.cs](./check-pdf417-isreaderinitialization-flag-to-determine-if-barcode-contains-initialization-instructions-for-scanner.cs) | `Pdf417Parameters`, `BarCodeReader` | Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization i... |
| [configure-barcodereader-to-enable-tryharder-mode-for-detecting-low-contrast-barcodes-in-challenging-lighting.cs](./configure-barcodereader-to-enable-tryharder-mode-for-detecting-low-contrast-barcodes-in-challenging-lighting.cs) | `BarCodeReader` | Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in ch... |
| [configure-barcodereader-to-read-only-2d-barcodes-and-ignore-1d-symbologies-for-faster-processing.cs](./configure-barcodereader-to-read-only-2d-barcodes-and-ignore-1d-symbologies-for-faster-processing.cs) | `DecodeType`, `BarCodeReader` | Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster proc... |
| [detect-barcodes-in-rotated-images-and-verify-orientation-angle-matches-expected-rotation.cs](./detect-barcodes-in-rotated-images-and-verify-orientation-angle-matches-expected-rotation.cs) | `BarCodeReader` | Detect barcodes in rotated images and verify orientation angle matches expected rotation. |
| [determine-barcode-orientation-angle-for-each-detected-barcode-in-bmp-image.cs](./determine-barcode-orientation-angle-for-each-detected-barcode-in-bmp-image.cs) | `BarCodeReader` | Determine barcode orientation angle for each detected barcode in a BMP image. |
| [dispose-barcodereader-instance-properly-within-using-block-to-release-unmanaged-resources.cs](./dispose-barcodereader-instance-properly-within-using-block-to-release-unmanaged-resources.cs) | `BarCodeReader` | Dispose BarCodeReader instance properly within a using block to release unmanaged resource... |
| [download-image-from-aws-s3-bucket-and-read-pdf417-linked-state-metadata.cs](./download-image-from-aws-s3-bucket-and-read-pdf417-linked-state-metadata.cs) | `Pdf417Parameters` | Download image from AWS S3 bucket and read PDF417 linked state metadata. |
| [enable-autorotate-option-to-automatically-correct-barcode-orientation-before-reading-each-processed-image.cs](./enable-autorotate-option-to-automatically-correct-barcode-orientation-before-reading-each-processed-image.cs) |  | Enable autoRotate option to automatically correct barcode orientation before reading each ... |
| [enable-checksum-validation-for-code128-barcodes-and-report-any-verification-failures-during-processing.cs](./enable-checksum-validation-for-code128-barcodes-and-report-any-verification-failures-during-processing.cs) | `EnableChecksum`, `ChecksumValidation` | Enable checksum validation for Code128 barcodes and report any verification failures durin... |
| [export-barcode-type-text-region-and-orientation-to-json-file-for-downstream-consumption.cs](./export-barcode-type-text-region-and-orientation-to-json-file-for-downstream-consumption.cs) |  | Export barcode type, text, region, and orientation to a JSON file for downstream consumpti... |
| [extract-aztec-code-layer-count-and-compact-mode-flag-from-image-containing-aztec-barcodes.cs](./extract-aztec-code-layer-count-and-compact-mode-flag-from-image-containing-aztec-barcodes.cs) | `AztecParameters` | Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcod... |
| [extract-barcode-metadata-from-live-camera-feed-and-display-results-in-real-time.cs](./extract-barcode-metadata-from-live-camera-feed-and-display-results-in-real-time.cs) |  | Extract barcode metadata from live camera feed and display results in real time. |
| [extract-barcode-placement-region-coordinates-from-png-file-and-store-them-in-database.cs](./extract-barcode-placement-region-coordinates-from-png-file-and-store-them-in-database.cs) |  | Extract barcode placement region coordinates from a PNG file and store them in a database. |
| [extract-gs1-composite-component-count-and-application-identifiers-from-png-image.cs](./extract-gs1-composite-component-count-and-application-identifiers-from-png-image.cs) | `GS1CodeTextBuilder`, `GS1CompositeBarParameters` | Extract GS1 Composite component count and application identifiers from a PNG image. |
| [extract-pdf417-structured-append-sequence-number-and-total-count-from-multi-segment-pdf417-codes.cs](./extract-pdf417-structured-append-sequence-number-and-total-count-from-multi-segment-pdf417-codes.cs) | `Pdf417Parameters` | Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417... |
| [fetch-image-from-azure-blob-storage-and-extract-barcode-type-and-code-text.cs](./fetch-image-from-azure-blob-storage-and-extract-barcode-type-and-code-text.cs) |  | Fetch image from Azure Blob storage and extract barcode type and code text. |
| [handle-password-protected-image-files-by-supplying-credentials-before-barcode-detection-in-secure-pipelines.cs](./handle-password-protected-image-files-by-supplying-credentials-before-barcode-detection-in-secure-pipelines.cs) |  | Handle password‑protected image files by supplying credentials before barcode detection in... |
| [identify-micro-pdf417-code128-emulation-flag-and-handle-accordingly-in-processing-logic.cs](./identify-micro-pdf417-code128-emulation-flag-and-handle-accordingly-in-processing-logic.cs) | `Pdf417Parameters` | Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic. |
| [identify-qr-code-structured-append-metadata-using-qrextendedparameters-for-multi-segment-qr-codes-in-images.cs](./identify-qr-code-structured-append-metadata-using-qrextendedparameters-for-multi-segment-qr-codes-in-images.cs) | `QrParameters` | Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment Q... |
| [implement-async-barcode-reading-using-barcodereaderreadbarcodesasync-for-responsive-ui-in-desktop-applications.cs](./implement-async-barcode-reading-using-barcodereaderreadbarcodesasync-for-responsive-ui-in-desktop-applications.cs) | `BarCodeReader` | Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in... |
| [instantiate-barcodereader-with-image-file-path-and-read-all-detected-barcode-types.cs](./instantiate-barcodereader-with-image-file-path-and-read-all-detected-barcode-types.cs) | `BarCodeReader` | Instantiate BarCodeReader with an image file path and read all detected barcode types. |
| [iterate-over-barcoderesult-collection-to-log-each-barcode-s-type-text-and-region.cs](./iterate-over-barcoderesult-collection-to-log-each-barcode-s-type-text-and-region.cs) | `BarCodeResult` | Iterate over BarCodeResult collection to log each barcode's type, text, and region. |
| [limit-barcodereader-to-specific-symbologies-such-as-qr-code-and-pdf417-for-performance.cs](./limit-barcodereader-to-specific-symbologies-such-as-qr-code-and-pdf417-for-performance.cs) | `QrParameters`, `Pdf417Parameters`, `DecodeType`, `BarCodeReader` | Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance. |
| [load-image-data-from-memory-stream-and-extract-barcode-placement-region-without-saving-to-disk.cs](./load-image-data-from-memory-stream-and-extract-barcode-placement-region-without-saving-to-disk.cs) |  | Load image data from a memory stream and extract barcode placement region without saving t... |
| [obtain-dotcode-version-information-and-error-correction-level-from-scanned-dotcode-barcode.cs](./obtain-dotcode-version-information-and-error-correction-level-from-scanned-dotcode-barcode.cs) |  | Obtain DotCode version information and error correction level from a scanned DotCode barco... |
| [process-encrypted-pdf-files-by-providing-password-to-barcodereader-and-extracting-barcode-data.cs](./process-encrypted-pdf-files-by-providing-password-to-barcodereader-and-extracting-barcode-data.cs) | `BarCodeReader` | Process encrypted PDF files by providing password to BarCodeReader and extracting barcode ... |
| [read-barcode-code-text-and-symbology-type-from-jpeg-image-using-barcodereader.cs](./read-barcode-code-text-and-symbology-type-from-jpeg-image-using-barcodereader.cs) | `DecodeType`, `BarCodeReader` | Read barcode code text and symbology type from a JPEG image using BarCodeReader. |
| [read-barcode-data-from-base64-encoded-image-string-and-decode-embedded-information.cs](./read-barcode-data-from-base64-encoded-image-string-and-decode-embedded-information.cs) | `BarCodeImageFormat`, `BarCodeReader` | Read barcode data from a base64‑encoded image string and decode the embedded information. |
| [read-barcode-information-from-byte-array-representing-image-and-output-json-metadata.cs](./read-barcode-information-from-byte-array-representing-image-and-output-json-metadata.cs) | `BarCodeReader` | Read barcode information from a byte array representing an image and output JSON metadata. |
| [read-barcodes-from-multi-page-tiff-file-and-capture-orientation-for-each-page.cs](./read-barcodes-from-multi-page-tiff-file-and-capture-orientation-for-each-page.cs) | `BarCodeReader` | Read barcodes from a multi‑page TIFF file and capture orientation for each page. |
| [read-barcodes-from-video-frame-captured-by-webcam-and-log-orientation-angles.cs](./read-barcodes-from-video-frame-captured-by-webcam-and-log-orientation-angles.cs) | `BarCodeReader` | Read barcodes from a video frame captured by a webcam and log orientation angles. |
| [read-barcodes-from-zip-archive-containing-multiple-image-files-and-aggregate-metadata.cs](./read-barcodes-from-zip-archive-containing-multiple-image-files-and-aggregate-metadata.cs) | `BarCodeReader` | Read barcodes from a zip archive containing multiple image files and aggregate metadata. |
| [read-databar-expanded-data-fields-and-numeric-values-from-jpeg-image.cs](./read-databar-expanded-data-fields-and-numeric-values-from-jpeg-image.cs) |  | Read DataBar expanded data fields and numeric values from a JPEG image. |
| [read-datamatrix-symbol-size-and-encoding-mode-from-tiff-image-with-datamatrix-barcodes.cs](./read-datamatrix-symbol-size-and-encoding-mode-from-tiff-image-with-datamatrix-barcodes.cs) | `DataMatrixParameters`, `BarCodeReader` | Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes. |
| [read-qr-code-structured-append-parity-data-and-validate-against-expected-values-for-each-segment.cs](./read-qr-code-structured-append-parity-data-and-validate-against-expected-values-for-each-segment.cs) | `QrParameters` | Read QR Code structured‑append parity data and validate against expected values for each s... |
| [read-qr-code-version-and-error-correction-level-from-each-detected-qr-barcode.cs](./read-qr-code-version-and-error-correction-level-from-each-detected-qr-barcode.cs) | `QrParameters`, `BarCodeReader` | Read QR Code version and error correction level from each detected QR barcode. |
| [retrieve-maxicode-mode-and-postal-code-data-from-pdf-containing-maxicode-symbols.cs](./retrieve-maxicode-mode-and-postal-code-data-from-pdf-containing-maxicode-symbols.cs) |  | Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols. |
| [retrieve-pdf417-macro-fields-such-as-file-id-and-segment-id-from-scanned-document.cs](./retrieve-pdf417-macro-fields-such-as-file-id-and-segment-id-from-scanned-document.cs) | `Pdf417Parameters` | Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document. |
| [scale-down-high-resolution-images-before-barcode-reading-to-improve-performance-on-limited-hardware.cs](./scale-down-high-resolution-images-before-barcode-reading-to-improve-performance-on-limited-hardware.cs) |  | Scale down high‑resolution images before barcode reading to improve performance on limited... |
| [set-barcodereader-to-ignore-white-space-when-decoding-code39-barcodes-in-scanned-images.cs](./set-barcodereader-to-ignore-white-space-when-decoding-code39-barcodes-in-scanned-images.cs) | `BarCodeReader` | Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images. |
| [set-maximum-number-of-barcodes-per-image-to-three-to-limit-processing-overhead.cs](./set-maximum-number-of-barcodes-per-image-to-three-to-limit-processing-overhead.cs) |  | Set maximum number of barcodes per image to three to limit processing overhead. |
| [store-barcode-region-polygon-points-in-spatial-database-for-later-geometric-analysis.cs](./store-barcode-region-polygon-points-in-spatial-database-for-later-geometric-analysis.cs) |  | Store barcode region polygon points in a spatial database for later geometric analysis. |
| [use-barcodereader-on-pdf-stream-to-decode-barcodes-embedded-on-each-page.cs](./use-barcodereader-on-pdf-stream-to-decode-barcodes-embedded-on-each-page.cs) | `BarCodeReader` | Use BarCodeReader on a PDF stream to decode barcodes embedded on each page. |
| [use-custom-region-of-interest-to-limit-barcode-detection-to-specific-area-of-image.cs](./use-custom-region-of-interest-to-limit-barcode-detection-to-specific-area-of-image.cs) |  | Use custom region of interest to limit barcode detection to a specific area of an image. |
| [use-parallel-processing-to-read-barcodes-from-multiple-images-concurrently-and-aggregate-results.cs](./use-parallel-processing-to-read-barcodes-from-multiple-images-concurrently-and-aggregate-results.cs) | `BarCodeReader` | Use parallel processing to read barcodes from multiple images concurrently and aggregate r... |

## Category Statistics
- Total examples: 50
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `BarCodeReader`
- `BarCodeResult`
- `DecodeType`
- `QualitySettings`
- `BarcodeSettings`
- `MultipleScanMode`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-07-03 | Examples: 50
<!-- AUTOGENERATED:END -->
