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

#if !NETSTANDARD1_3 && !NETSTANDARD1_5

namespace NLog.LayoutRenderers
{
    using System;
    using System.IO;
    using System.Text;
    using NLog.Config;
    using NLog.Internal;

    /// <summary>
    /// System special folder path (includes My Documents, My Music, Program Files, Desktop, and more).
    /// </summary>
    [LayoutRenderer("specialfolder")]
    [AppDomainFixedOutput]
    [ThreadAgnostic]
    public class SpecialFolderLayoutRenderer : LayoutRenderer
    {
        /// <summary>
        /// Gets or sets the system special folder to use.
        /// </summary>
        /// <remarks>
        /// Full list of options is available at <a href="https://docs.microsoft.com/en-us/dotnet/api/system.environment.specialfolder">MSDN</a>.
        /// The most common ones are:
        /// <ul>
        /// <li><b>ApplicationData</b> - roaming application data for current user.</li>
        /// <li><b>CommonApplicationData</b> - application data for all users.</li>
        /// <li><b>MyDocuments</b> - My Documents</li>
        /// <li><b>DesktopDirectory</b> - Desktop directory</li>
        /// <li><b>LocalApplicationData</b> - non roaming application data</li>
        /// <li><b>Personal</b> - user profile directory</li>
        /// <li><b>System</b> - System directory</li>
        /// </ul>
        /// </remarks>
        /// <docgen category='Rendering Options' order='10' />
        [DefaultParameter]
        public Environment.SpecialFolder Folder { get; set; }

        /// <summary>
        /// Gets or sets the name of the file to be Path.Combine()'d with the directory name.
        /// </summary>
        /// <docgen category='Advanced Options' order='10' />
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the name of the directory to be Path.Combine()'d with the directory name.
        /// </summary>
        /// <docgen category='Advanced Options' order='10' />
        public string Dir { get; set; }

        /// <summary>
        /// Renders the directory where NLog is located and appends it to the specified <see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            string basePath = Environment.GetFolderPath(Folder);
            var path = PathHelpers.CombinePaths(basePath, Dir, File);
            builder.Append(path);
        }
    }
}

#endif