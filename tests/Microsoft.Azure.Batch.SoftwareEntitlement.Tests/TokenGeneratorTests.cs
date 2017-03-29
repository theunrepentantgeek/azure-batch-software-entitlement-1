﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Microsoft.Azure.Batch.SoftwareEntitlement.Tests
{
    public class TokenGeneratorTests
    {
        // Key used to sign tokens
        private readonly SymmetricSecurityKey _signingKey;
        public TokenGeneratorTests()
        {
            var plainTextSecurityKey = "This is my shared, not so secret, secret!";
            _signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(plainTextSecurityKey));
        }

        public class Constructor : TokenGeneratorTests
        {
            [Fact]
            public void GivenNullKey_ThrowsArgumentNullException()
            {
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => new TokenGenerator(null));
                exception.ParamName.Should().Be("signingKey");
            }

            [Fact]
            public void GivenKey_InitializesProperty()
            {
                var generator = new TokenGenerator(_signingKey);
                generator.SigningKey.Should().Be(_signingKey);
            }
        }

        public class Generate : TokenGeneratorTests
        {
            private readonly TokenGenerator _generator;

            public Generate()
            {
                _generator = new TokenGenerator(_signingKey);
            }

            [Fact]
            public void GivenNullEntitlement_ThrowsArgumentNullException()
            {
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _generator.Generate(null));
                exception.ParamName.Should().Be("entitlements");
            }
        }
    }
}