// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Authentication.MSALWrapper.AuthFlow
{
    /// <summary>
    /// Extension method to the <see cref="TokenResult"/> class.
    /// </summary>
    internal static class TokenResultExtensions
    {
        /// <summary>
        /// Sets the AuthType property to the given authType if the token result is not null.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="authType">The auth type.</param>
        internal static void SetAuthenticationType(this TokenResult result, AuthType authType)
        {
            if (result != null)
            {
                result.AuthType = authType;
            }
        }
    }
}
