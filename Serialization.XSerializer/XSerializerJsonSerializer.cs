using Cactus.Blade.Guard;
using Cactus.Blade.Serialization;
using System;
using System.IO;
using XSerializer;

namespace Serialization.XSerializer
{
    /// <summary>
    /// A JSON implementation of the <see cref="ISerializer"/> interface using <see cref="JsonSerializer"/>/>.
    /// </summary>
    public class XSerializerJsonSerializer : ISerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XSerializerJsonSerializer"/> class.
        /// </summary>
        /// <param name="name">The name of the serializer, used to when selecting which serializer to use.</param>
        /// <param name="options">Options for customizing the JsonSerializer.</param>
        public XSerializerJsonSerializer(string name = "default", JsonSerializerConfiguration options = null)
        {
            Name = name ?? "default";
            Options = options;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="JsonSerializerConfiguration"/> options.
        /// </summary>
        public JsonSerializerConfiguration Options { get; }

        /// <inheritdoc />
        public void SerializeToStream(Stream stream, object item, Type type)
        {
            Guard.Against.Null(stream, nameof(stream));
            Guard.Against.Null(item, nameof(item));
            Guard.Against.Null(type, nameof(type));

            var serializer = JsonSerializer.Create(type, Options);

            serializer.Serialize(stream, item);
        }

        /// <inheritdoc />
        public object DeserializeFromStream(Stream stream, Type type)
        {
            Guard.Against.Null(stream, nameof(stream));
            Guard.Against.Null(type, nameof(type));

            var serializer = JsonSerializer.Create(type, Options);

            return serializer.Deserialize(stream);
        }

        /// <inheritdoc />
        public string SerializeToString(object item, Type type)
        {
            Guard.Against.Null(item, nameof(item));
            Guard.Against.Null(type, nameof(type));

            var serializer = JsonSerializer.Create(type, Options);

            return serializer.Serialize(item);
        }

        /// <inheritdoc />
        public object DeserializeFromString(string data, Type type)
        {
            Guard.Against.Null(data, nameof(data));
            Guard.Against.Null(type, nameof(type));

            var serializer = JsonSerializer.Create(type, Options);

            return serializer.Deserialize(data);
        }
    }
}
