﻿using System;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DefaultDerAsnDecoderTests
    {
        [Test]
        public void Decode_ShouldDecodeAllKnownDefaultTypes()
        {
            // TODO Try to decode all known types
        }

        [Test]
        public void RegisterGenericType_PassNullAsTypeConstructor_ShouldThrowArgumentNullException()
        {
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 0, null));
            Assert.That(ex.ParamName, Is.EqualTo("typeConstructor"));
        }

        [Test]
        public void RegisterGenericType_RegisterSameTypeTwice_ReplaceSetToFalse_ShouldThrowInvalidOperationException()
        {
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null);
            var ex = Assert.Throws<InvalidOperationException>(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 0xFFF, (_, __, ___) => null));
            Assert.That(ex.Message, Is.EqualTo("Type with encoding type Constructed and tag number 4095 already registered"));
        }

        [Test]
        public void RegisterGenericType_RegisterSameTypeTwice_ReplaceSetToTrue_ShouldNotThrowException()
        {
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null);
            Assert.DoesNotThrow(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null, true));
        }

        [Test]
        public void RegisterType_PassNullAsIdentifier_ShouldThrowArgumentNullException()
        {
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterType(null, (_, __, ___) => null));
            Assert.That(ex.ParamName, Is.EqualTo("identifier"));
        }

        [Test]
        public void RegisterType_PassNullAsTypeConstructor_ShouldThrowArgumentNullException()
        {
            var identifier = DerAsnIdentifiers.Universal.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterType(identifier, null));
            Assert.That(ex.ParamName, Is.EqualTo("typeConstructor"));
        }

        [Test]
        public void RegisterType_RegisterSameTypeTwice_ReplaceSetToFalse_ShouldThrowInvalidOperationException()
        {
            var identifier = DerAsnIdentifiers.Universal.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterType(identifier, (_, __, ___) => null);
            var ex = Assert.Throws<InvalidOperationException>(() => decoder.RegisterType(identifier, (_, __, ___) => null));
            Assert.That(ex.Message, Is.EqualTo("Type with class Universal, encoding type Primitive and tag number 1 already registered"));
        }

        [Test]
        public void RegisterType_RegisterSameTypeTwice_ReplaceSetToTrue_ShouldNotThrowException()
        {
            var identifier = DerAsnIdentifiers.Universal.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterType(identifier, (_, __, ___) => null);
            Assert.DoesNotThrow(() => decoder.RegisterType(identifier, (_, __, ___) => null, true));
        }
    }
}
