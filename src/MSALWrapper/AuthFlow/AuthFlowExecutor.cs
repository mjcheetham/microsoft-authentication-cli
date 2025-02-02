// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Authentication.MSALWrapper.AuthFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The auth flows class.
    /// </summary>
    public class AuthFlowExecutor : IAuthFlow
    {
        private readonly IEnumerable<IAuthFlow> authflows;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthFlowExecutor"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="authFlows">The list of auth flows.</param>
        public AuthFlowExecutor(ILogger logger, IEnumerable<IAuthFlow> authFlows)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.authflows = authFlows ?? throw new ArgumentNullException(nameof(authFlows));
        }

        /// <summary>
        /// Get a auth flow result.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<AuthFlowResult> GetTokenAsync()
        {
            AuthFlowResult result = new AuthFlowResult(null, new List<Exception>());

            if (this.authflows.Count() == 0)
            {
                this.logger.LogWarning("Warning: There are 0 auth modes to execute!");
            }

            foreach (var authFlow in this.authflows)
            {
                var authFlowName = authFlow.GetType().Name;
                this.logger.LogDebug($"Starting {authFlowName}...");

                var attempt = await authFlow.GetTokenAsync();
                if (attempt == null)
                {
                    var oopsMessage = $"Auth flow '{authFlow.GetType().Name}' returned a null AuthFlowResult.";
                    result.Errors.Add(new NullTokenResultException(oopsMessage));
                    this.logger.LogDebug(oopsMessage);
                }
                else
                {
                    result.AddErrors(attempt.Errors);

                    this.logger.LogDebug($"{authFlowName} success: {attempt.Success}.");
                    if (attempt.Success)
                    {
                        result.TokenResult = attempt.TokenResult;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
