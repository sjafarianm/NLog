// 
// Copyright (c) 2004-2021 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

#if !NETSTANDARD1_3

namespace NLog.LayoutRenderers
{
    using System.Text;
    using NLog.Config;
    using NLog.Internal.Fakeables;

    /// <summary>
    /// The name of the current process.
    /// </summary>
    [LayoutRenderer("processname")]
    [AppDomainFixedOutput]
    [ThreadAgnostic]
    public class ProcessNameLayoutRenderer : LayoutRenderer
    {
        private readonly string _processFilePath;
        private readonly string _processBaseName;

        /// <summary>
        /// Gets or sets a value indicating whether to write the full path to the process executable.
        /// </summary>
        /// <docgen category='Rendering Options' order='10' />
        public bool FullName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessNameLayoutRenderer" /> class.
        /// </summary>
        public ProcessNameLayoutRenderer()
            :this(LogFactory.DefaultAppEnvironment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessNameLayoutRenderer" /> class.
        /// </summary>
        internal ProcessNameLayoutRenderer(IAppEnvironment appEnvironment)
        {
            _processFilePath = appEnvironment.CurrentProcessFilePath;
            _processBaseName = appEnvironment.CurrentProcessBaseName;
        }

        /// <summary>
        /// Renders the current process name (optionally with a full path).
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var output = FullName ? _processFilePath : _processBaseName;
            builder.Append(output);
        }
    }
}

#endif