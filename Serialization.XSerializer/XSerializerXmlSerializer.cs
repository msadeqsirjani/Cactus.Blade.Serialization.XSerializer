using Cactus.Blade.Guard;
using Cactus.Blade.Serialization;
using System;
using System.IO;
using XSerializer;

namespace Serialization.XSerializer
{
    /// <summary>
    /// An XML implementation of the <see cref="ISerializer"/> interface using <see cref="System.Xml.Serialization.XmlSerializer"/>/>.
    /// </summary>
    public class XSerializerXmlSerializer : ISerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XSerializerXmlSerializer"/> class.
        /// </summary>
        /// <param name="name">The name of the serializer, used to when selecting which serializer to use.</param>
        /// <param name="options">Options for customizing the XmlSerializer.</param>
        public XSerializerXmlSerializer(string name = "default", XmlSerializationOptions options = null)
        {
            Name = name ?? "default";
            Options = options;
        }

        /// <inheritdoc />  
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="XmlSerializationOptions"/> options.
        /// </summary>
        public XmlSerializationOptions Options { get; }

        /// <inheritdoc />
        public void SerializeToStream(Stream stream, object item, Type type)
        {
            Guard.Against.Null(stream, nameof(stream));
            Guard.Against.Null(item, nameof(item));
            Guard.Against.Null(type, nameof(type));

            var serializer = Options.IsNull()
                ? XmlSerializer.Create(type)
                : XmlSerializer.Create(type, Options);

            serializer.Serialize(stream, item);
        }

        /// <inheritdoc />
        public object DeserializeFromStream(Stream stream, Type type)
        {
            Guard.Against.Null(stream, nameof(stream));
            Guard.Against.Null(type, nameof(type));

            var serializer = Options.IsNull()
                ? XmlSerializer.Create(type)
                : XmlSerializer.Create(type, Options);

            return serializer.Deserialize(stream);
        }

        /// <inheritdoc />
        public string SerializeToString(object item, Type type)
        {
            Guard.Against.Null(item, nameof(item));
            Guard.Against.Null(type, nameof(type));

            var serializer = Options.IsNull()
                ? XmlSerializer.Create(type)
                : XmlSerializer.Create(type, Options);

            return serializer.Serialize(item);
        }

        /// <inheritdoc />
        public object DeserializeFromString(string data, Type type)
        {
            Guard.Against.Null(data, nameof(data));
            Guard.Against.Null(type, nameof(type));

            var serializer = Options.IsNull()
                ? XmlSerializer.Create(type)
                : XmlSerializer.Create(type, Options);

            return serializer.Deserialize(data);
        }
    }
}
