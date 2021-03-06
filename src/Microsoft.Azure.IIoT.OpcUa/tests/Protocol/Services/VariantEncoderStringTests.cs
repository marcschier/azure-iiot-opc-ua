// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Protocol.Services {
    using Opc.Ua;
    using Xunit;
    using Newtonsoft.Json.Linq;

    public class VariantEncoderStringTests {

        [Fact]
        public void DecodeEncodeStringFromJValue() {
            var codec = new JsonVariantEncoder();
            var str = new JValue("");
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromJArray() {
            var codec = new JsonVariantEncoder();
            var str = new JArray("", "", "");
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromJValueTypeNullIsString() {
            var codec = new JsonVariantEncoder();
            var str = new JValue("");
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromJArrayTypeNullIsString() {
            var codec = new JsonVariantEncoder();
            var str = new JArray("", "", "");
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromString() {
            var codec = new JsonVariantEncoder();
            var str = "123";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("123");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue("123"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromString2() {
            var codec = new JsonVariantEncoder();
            var str = "123, 124, 125";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("123, 124, 125");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromString3() {
            var codec = new JsonVariantEncoder();
            var str = "[test, test, test]";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(str);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(str, encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromString2() {
            var codec = new JsonVariantEncoder();
            var str = "[123, 124, 125]";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[] { "123", "124", "125" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("123", "124", "125"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromString4() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromStringTypeNullIsString() {
            var codec = new JsonVariantEncoder();
            var str = "test";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant("test");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue("test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromStringTypeNullIsString() {
            var codec = new JsonVariantEncoder();
            var str = "test, test, test";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "test", "test", "test" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("test", "test", "test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromStringTypeNullIsString2() {
            var codec = new JsonVariantEncoder();
            var str = "[\"test\", \"test\", \"test\"]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "test", "test", "test" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("test", "test", "test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromStringTypeNullIsString3() {
            var codec = new JsonVariantEncoder();
            var str = "[test, test, test]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "[test", "test", "test]" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("[test", "test", "test]"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromStringTypeNullIsNull() {
            var codec = new JsonVariantEncoder();
            var str = "[]";
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = Variant.Null;
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
        }

        [Fact]
        public void DecodeEncodeStringFromQuotedString1() {
            var codec = new JsonVariantEncoder();
            var str = "\"test\"";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("test");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal("test", encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromQuotedString2() {
            var codec = new JsonVariantEncoder();
            var str = "\"\\\"test\\\"\"";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("\"test\"");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal("\"test\"", encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromSinglyQuotedString() {
            var codec = new JsonVariantEncoder();
            var str = "  'test'";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant("test");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue("test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromQuotedString1() {
            var codec = new JsonVariantEncoder();
            var str = "\"test\",'test',\"test\"";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[] { "test", "test", "test" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("test", "test", "test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromQuotedString2() {
            var codec = new JsonVariantEncoder();
            var str = " [\"test\",'test',\"test\"] ";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[] { "test", "test", "test" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("test", "test", "test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromQuotedString3() {
            var codec = new JsonVariantEncoder();
            var str = " [\"\\\"test\\\"\",'\\\"test\\\"',\"\\\"test\\\"\"] ";
            var variant = codec.Decode(str, BuiltInType.String, null);
            var expected = new Variant(new string[] { "test", "test", "test" });
            // TODO: var expected = new Variant(new string[] { "\"test\"", "\"test\"", "\"test\"" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            // TODO: Assert.Equal(new JArray("\"test\"", "\"test\"", "\"test\""), encoded);
            Assert.Equal(new JArray("test", "test", "test"), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonTokenTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = ""
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonTokenTypeVariant1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = new string[] { "", "", "" }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("", "", ""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonTokenTypeVariant2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = new string[0]
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new string[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = ""
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonStringTypeVariant() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = new string[] { "", "", "" }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("", "", ""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonTokenTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = ""
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonTokenTypeNull1() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                TYPE = "STRING",
                BODY = new string[] { "", "", "" }
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("", "", ""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonTokenTypeNull2() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "String",
                Body = new string[0]
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[0]);
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray(), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                Type = "string",
                Body = ""
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonStringTypeNull() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                type = "String",
                body = new string[] { "", "", "" }
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("", "", ""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonTokenTypeNullMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "String",
                Value = ""
            });
            var variant = codec.Decode(str, BuiltInType.Null, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringFromVariantJsonStringTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                DataType = "String",
                Value = ""
            }).ToString();
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant("");
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JValue(""), encoded);
        }

        [Fact]
        public void DecodeEncodeStringArrayFromVariantJsonTokenTypeVariantMsftEncoding() {
            var codec = new JsonVariantEncoder();
            var str = JToken.FromObject(new {
                dataType = "String",
                value = new string[] { "", "", "" }
            });
            var variant = codec.Decode(str, BuiltInType.Variant, null);
            var expected = new Variant(new string[] { "", "", "" });
            var encoded = codec.Encode(variant);
            Assert.Equal(expected, variant);
            Assert.Equal(new JArray("", "", ""), encoded);
        }
    }
}
