// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Protocol.Services {
    using Opc.Ua;
    using Xunit;
    using Newtonsoft.Json.Linq;

    public class VariantEncoderUInt32Tests {

        [Fact]
        public void DecodeEncodeUInt32FromJValue() {
            var codec = new JsonVariantEncoder();
            var str = new JValue(123u);
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromJArray() {
            var codec = new JsonVariantEncoder();
            var str = new JArray(123u, 124u, 125u);
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromJValueTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = new JValue(123u);
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromJArrayTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = new JArray(123u, 124u, 125u);
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromString() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromString() {
            var codec = new JsonVariantEncoder();
            var str = "123, 124, 125";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromString2() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromString3() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromStringTypeIntegerIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeIntegerIsInt641() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(new Variant[] {
                new Variant(123L), new Variant(124L), new Variant(125L)
            });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeIntegerIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(new Variant[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromStringTypeNumberIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeNumberIsInt641() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(new Variant[] {
                new Variant(123L), new Variant(124L), new Variant(125L)
            });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeNumberIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(new Variant[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromStringTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }
        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123, 124, 125";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeNullIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromStringTypeNullIsNull() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = Variant.Null;
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
        }

        [Fact]
        public void DecodeEncodeUInt32FromQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "\"123\"";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromSinglyQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "  '123'";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "\"123\",'124',\"125\"";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromQuotedString2() {
            var codec = new JsonVariantEncoder();
            var str = " [\"123\",'124',\"125\"] ";
            var variant = codec.Decode(str, BuiltInType.UInt32, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonTokenTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = 123u
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonTokenTypeVariant1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = new uint[] { 123u, 124u, 125u }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonTokenTypeVariant2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = new uint[0]
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new uint[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = 123u
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = new uint[] { 123u, 124u, 125u }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonTokenTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = 123u
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonTokenTypeNull1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                TYPE = "UINT32",
                BODY = new uint[] { 123u, 124u, 125u }
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonTokenTypeNull2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt32",
                Body = new uint[0]
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new uint[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "uint32",
                Body = 123u
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                type = "UInt32",
                body = new uint[] { 123u, 124u, 125u }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonTokenTypeNullMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "UInt32",
                Value = 123u
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32FromVariantJsonStringTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "UInt32",
                Value = 123u
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(123u);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123u), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt32ArrayFromVariantJsonTokenTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                dataType = "UInt32",
                value = new uint[] { 123u, 124u, 125u }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new uint[] { 123u, 124u, 125u });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(123u, 124u, 125u), encoded);
        }
    }
}
