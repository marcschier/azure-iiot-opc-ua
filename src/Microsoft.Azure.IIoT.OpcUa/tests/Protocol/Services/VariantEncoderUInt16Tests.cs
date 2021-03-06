// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Protocol.Services {
    using Opc.Ua;
    using Xunit;
    using Newtonsoft.Json.Linq;

    public class VariantEncoderUInt16Tests {

        [Fact]
        public void DecodeEncodeUInt16FromJValue() {
            var codec = new JsonVariantEncoder();
            var str = new JValue(123);
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromJArray() {
            var codec = new JsonVariantEncoder();
            var str = new JArray((ushort)123, (ushort)124, (ushort)125);
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromJValueTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = new JValue(123);
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromJArrayTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = new JArray((ushort)123, (ushort)124, (ushort)125);
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromString() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromString() {
            var codec = new JsonVariantEncoder();
            var str = "123, 124, 125";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromString2() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromString3() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromStringTypeIntegerIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeIntegerIsInt641() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(new Variant[] {
                new Variant(123L), new Variant(124L), new Variant(125L)
            });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeIntegerIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Integer, null);
            var expected = new Variant(new Variant[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromStringTypeNumberIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeNumberIsInt641() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(new Variant[] {
                new Variant(123L), new Variant(124L), new Variant(125L)
            });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeNumberIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Number, null);
            var expected = new Variant(new Variant[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromStringTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(123L);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }
        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeNullIsInt64() {
            var codec = new JsonVariantEncoder();
            var str = "123, 124, 125";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeNullIsInt642() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new long[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromStringTypeNullIsNull() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = Variant.Null;
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
        }

        [Fact]
        public void DecodeEncodeUInt16FromQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "\"123\"";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromSinglyQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "  '123'";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "\"123\",'124',\"125\"";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromQuotedString2() {
            var codec = new JsonVariantEncoder();
            var str = " [\"123\",'124',\"125\"] ";
            var variant = codec.Decode(str, BuiltInType.UInt16, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonTokenTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = 123
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonTokenTypeVariant1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = new ushort[] { 123, 124, 125 }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonTokenTypeVariant2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = new ushort[0]
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new ushort[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = 123
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = new ushort[] { 123, 124, 125 }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonTokenTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = (ushort)123
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonTokenTypeNull1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                TYPE = "UINT16",
                BODY = new ushort[] { 123, 124, 125 }
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonTokenTypeNull2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "UInt16",
                Body = new ushort[0]
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new ushort[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "uint16",
                Body = (ushort)123
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                type = "UInt16",
                body = new ushort[] { 123, 124, 125 }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonTokenTypeNullMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "UInt16",
                Value = 123
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16FromVariantJsonStringTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "UInt16",
                Value = (ushort)123
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant((ushort)123);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(123), encoded);
        }

        [Fact]
        public void DecodeEncodeUInt16ArrayFromVariantJsonTokenTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                dataType = "UInt16",
                value = new ushort[] { 123, 124, 125 }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new ushort[] { 123, 124, 125 });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray((ushort)123, (ushort)124, (ushort)125), encoded);
        }
    }
}
