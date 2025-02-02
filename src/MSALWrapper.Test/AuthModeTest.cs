// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Authentication.MSALWrapper.Test
{
    using FluentAssertions;

    using Microsoft.Authentication.MSALWrapper;

    using NUnit.Framework;

    internal class AuthModeTest
    {
#if PlatformWindows
        [Test]
        public void AllIsAll()
        {
            (AuthMode.Broker | AuthMode.Web | AuthMode.DeviceCode).Should().Be(AuthMode.All);
        }

        [Test]
        public void WindowsDefaultModes()
        {
            var subject = AuthMode.Default;
            subject.IsBroker().Should().BeTrue();
            subject.IsWeb().Should().BeTrue();
            subject.IsDeviceCode().Should().BeFalse();
        }

        [TestCase(AuthMode.All, true)]
        [TestCase(AuthMode.Broker, true)]
        [TestCase(AuthMode.Web, false)]
        [TestCase(AuthMode.DeviceCode, false)]
        public void BrokerIsExpected(AuthMode subject, bool expected)
        {
            subject.IsBroker().Should().Be(expected);
        }

        [TestCase(AuthMode.All, true)]
        [TestCase(AuthMode.Broker, false)]
        [TestCase(AuthMode.Web, true)]
        [TestCase(AuthMode.DeviceCode, false)]
        public void WebIsExpected(AuthMode subject, bool expected)
        {
            subject.IsWeb().Should().Be(expected);
        }

        [TestCase(AuthMode.All, true)]
        [TestCase(AuthMode.Broker, false)]
        [TestCase(AuthMode.Web, false)]
        [TestCase(AuthMode.DeviceCode, true)]
        public void DeviceCodeIsExpected(AuthMode subject, bool expected)
        {
            subject.IsDeviceCode().Should().Be(expected);
        }

        [Test]
        public void OtherCombos()
        {
            var subject = AuthMode.DeviceCode | AuthMode.Web;
            subject.IsDeviceCode().Should().BeTrue();
            subject.IsWeb().Should().BeTrue();

            subject = AuthMode.Broker | AuthMode.Web;
            subject.IsBroker().Should().BeTrue();
            subject.IsWeb().Should().BeTrue();
        }

        [Test]
        public void WebOrDeviceCodeIsNotbroker()
        {
            var subject = AuthMode.Web | AuthMode.DeviceCode;

            subject.IsBroker().Should().BeFalse();
        }
#else
        [Test]
        public void AllIsAll()
        {
            (AuthMode.Web | AuthMode.DeviceCode).Should().Be(AuthMode.All);
        }

        [TestCase(AuthMode.All, false)]
        [TestCase(AuthMode.Web, false)]
        [TestCase(AuthMode.DeviceCode, false)]
        public void BrokerIsExpected(AuthMode subject, bool expected)
        {
            subject.IsBroker().Should().Be(expected);
        }

        [Test]
        public void NonWindowsDefaultModes()
        {
            var subject = AuthMode.Default;
            subject.IsBroker().Should().BeFalse();
            subject.IsWeb().Should().BeTrue();
            subject.IsDeviceCode().Should().BeFalse();
        }
#endif
    }
}
