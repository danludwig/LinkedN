using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for converting response streams into other representations.
    /// </summary>
    internal static class ResourceConverter
    {
        internal static TResource ConvertTo<TResource>(this Stream stream, string url) where TResource : ResourceModel
        {
            if (stream == null) throw new ArgumentNullException("stream");

            // first, serialize the stream to a string
            var raw = new StreamReader(stream).ReadToEnd();

            // create a new stream from the string
            stream = raw.ToStream();

            var serializer = new DataContractJsonSerializer(typeof(TResource));
            var resource = serializer.ReadObject<TResource>(stream);
            resource.Resource = raw;
            resource.Uri = url;
            
            return resource;
        }

        private static TResource ReadObject<TResource>(this DataContractJsonSerializer serializer, Stream stream) where TResource : class
        {
            return serializer.ReadObject(stream) as TResource;
        }

        private static Stream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
