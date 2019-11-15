// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Testing;
using Xunit;

namespace Microsoft.AspNetCore.Server.Kestrel.FunctionalTests
{
    public class GeneratedCodeTests
    {
        [ConditionalFact]
        [Flaky("https://github.com/aspnet/AspNetCore-Internal/issues/2223", FlakyOn.Helix.All)]
        public void GeneratedCodeIsUpToDate()
        {
            var httpHeadersGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "HttpHeaders.Generated.cs");
            var httpProtocolGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "HttpProtocol.Generated.cs");
            var httpUtilitiesGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "HttpUtilities.Generated.cs");
            var transportMultiplexedConnectionGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "TransportConnectionBase.Generated.cs");
            var transportConnectionGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "TransportConnection.Generated.cs");
            var transportStreamGeneratedPath = Path.Combine(AppContext.BaseDirectory,"shared", "GeneratedContent", "TransportStream.Generated.cs");

            var testHttpHeadersGeneratedPath = Path.GetTempFileName();
            var testHttpProtocolGeneratedPath = Path.GetTempFileName();
            var testHttpUtilitiesGeneratedPath = Path.GetTempFileName();
            var testTransportMultiplexedConnectionGeneratedPath = Path.GetTempFileName();
            var testTransportConnectionGeneratedPath = Path.GetTempFileName();
            var testTransportStreamGeneratedPath = Path.GetTempFileName();

            try
            {
                var currentHttpHeadersGenerated = File.ReadAllText(httpHeadersGeneratedPath);
                var currentHttpProtocolGenerated = File.ReadAllText(httpProtocolGeneratedPath);
                var currentHttpUtilitiesGenerated = File.ReadAllText(httpUtilitiesGeneratedPath);
                var currentTransportConnectionBaseGenerated = File.ReadAllText(transportMultiplexedConnectionGeneratedPath);
                var currentTransportConnectionGenerated = File.ReadAllText(transportConnectionGeneratedPath);
                var currentTransportStreamGenerated = File.ReadAllText(transportStreamGeneratedPath);

                CodeGenerator.Program.Run(testHttpHeadersGeneratedPath,
                    testHttpProtocolGeneratedPath,
                    testHttpUtilitiesGeneratedPath,
                    testTransportMultiplexedConnectionGeneratedPath,
                    testTransportConnectionGeneratedPath,
                    testTransportStreamGeneratedPath);

                var testHttpHeadersGenerated = File.ReadAllText(testHttpHeadersGeneratedPath);
                var testHttpProtocolGenerated = File.ReadAllText(testHttpProtocolGeneratedPath);
                var testHttpUtilitiesGenerated = File.ReadAllText(testHttpUtilitiesGeneratedPath);
                var testTransportMultiplxedConnectionGenerated = File.ReadAllText(testTransportMultiplexedConnectionGeneratedPath);
                var testTransportConnectionGenerated = File.ReadAllText(testTransportConnectionGeneratedPath);
                var testTransportStreamGenerated = File.ReadAllText(testTransportStreamGeneratedPath);

                Assert.Equal(currentHttpHeadersGenerated, testHttpHeadersGenerated, ignoreLineEndingDifferences: true);
                Assert.Equal(currentHttpProtocolGenerated, testHttpProtocolGenerated, ignoreLineEndingDifferences: true);
                Assert.Equal(currentHttpUtilitiesGenerated, testHttpUtilitiesGenerated, ignoreLineEndingDifferences: true);
                Assert.Equal(currentTransportConnectionBaseGenerated, testTransportMultiplxedConnectionGenerated, ignoreLineEndingDifferences: true);
                Assert.Equal(currentTransportConnectionGenerated, testTransportConnectionGenerated, ignoreLineEndingDifferences: true);
                Assert.Equal(currentTransportStreamGenerated, testTransportStreamGenerated, ignoreLineEndingDifferences: true);

            }
            finally
            {
                File.Delete(testHttpHeadersGeneratedPath);
                File.Delete(testHttpProtocolGeneratedPath);
                File.Delete(testHttpUtilitiesGeneratedPath);
                File.Delete(testTransportMultiplexedConnectionGeneratedPath);
                File.Delete(testTransportConnectionGeneratedPath);
                File.Delete(testTransportStreamGeneratedPath);
            }
        }
    }
}
