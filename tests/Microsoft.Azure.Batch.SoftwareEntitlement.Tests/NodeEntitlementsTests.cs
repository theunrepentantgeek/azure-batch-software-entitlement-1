﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Microsoft.Azure.Batch.SoftwareEntitlement.Tests
{
    public class NodeEntitlementsTests
    {
        // An empty software entitlement to use for testing
        private readonly NodeEntitlements _emptyEntitlement = new NodeEntitlements();

        // A Times span representing NZDT
        private readonly TimeSpan _nzdt = new TimeSpan(+13, 0, 0);

        // An instant to use as the start for testing
        private readonly DateTimeOffset _start;

        // An instant to use as the finish for testing
        private readonly DateTimeOffset _finish;

        public NodeEntitlementsTests()
        {
            _start = new DateTimeOffset(2016, 2, 29, 16, 14, 12, _nzdt);
            _finish = new DateTimeOffset(2016, 3, 31, 16, 14, 12, _nzdt);
        }

        public class WithVirtualMachineIdMethod : NodeEntitlementsTests
        {
            [Fact]
            public void GivenNull_ThrowsArgumentNullException()
            {
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _emptyEntitlement.WithVirtualMachineId(null));
                exception.ParamName.Should().Be("virtualMachineId");
            }

            [Fact]
            public void GivenVirtualMachineId_ConfiguresProperty()
            {
                var vmid = "Sample";
                _emptyEntitlement.WithVirtualMachineId(vmid)
                    .VirtualMachineId.Should().Be(vmid);
            }
        }

        public class FromInstantMethod : NodeEntitlementsTests
        {
            [Fact]
            public void GivenStart_ConfiguresProperty()
            {
                _emptyEntitlement.FromInstant(_start)
                    .NotBefore.Should().Be(_start);
            }
        }

        public class UntilInstantMethod : NodeEntitlementsTests
        {
            [Fact]
            public void GivenFinish_ConfiguresProperty()
            {
                _emptyEntitlement.UntilInstant(_finish)
                    .NotAfter.Should().Be(_finish);
            }
        }
    }
}