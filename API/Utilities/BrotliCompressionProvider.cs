using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace API.Utilities
{
    public class BrotliCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "br";
        public bool SupportsFlush => true;
        public Stream CreateStream(Stream outputStream) => new BrotliStream(outputStream, CompressionMode.Compress);
    }
}
