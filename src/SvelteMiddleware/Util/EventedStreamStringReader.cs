// Copyright (c) .NET Foundation Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Original Source: https://github.com/aspnet/JavaScriptServices

using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("SvelteMiddleware.Tests")]
namespace SvelteMiddleware
{
    /// <summary>
    /// Captures the completed-line notifications from a <see cref="EventedStreamReader"/>,
    /// combining the data into a single <see cref="string"/>.
    /// </summary>
    internal class EventedStreamStringReader : IDisposable
    {
        private readonly EventedStreamReader _eventedStreamReader;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private bool _isDisposed;

        public EventedStreamStringReader(EventedStreamReader eventedStreamReader)
        {
            _eventedStreamReader = eventedStreamReader ?? throw new ArgumentNullException(nameof(eventedStreamReader));
            _eventedStreamReader.OnReceivedLine += OnReceivedLine;
        }

        public string ReadAsString() => _stringBuilder.ToString();

        private void OnReceivedLine(string line) => _stringBuilder.AppendLine(line);

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _eventedStreamReader.OnReceivedLine -= OnReceivedLine;
                _isDisposed = true;
            }
        }
    }
}
