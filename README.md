# Microsoft Authentication CLI

![Tests](https://shields.io/github/workflow/status/AzureAD/microsoft-authentication-cli/Build%20and%20Test/main?style=flat-square)
![Release](https://shields.io/github/v/release/AzureAD/microsoft-authentication-cli?display_name=tag&include_prereleases&sort=semver&style=flat-square)
![License](https://shields.io/badge/license-MIT-purple?style=flat-square)

---

`AzureAuth` is a CLI wrapper for performing AAD Authentication. It makes use of [MSAL](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) for authentication and [MSAL Extensions](https://github.com/AzureAD/microsoft-authentication-extensions-for-dotnet) for caching.

The CLI is designed for authenticating and returning an access token for public client AAD applications. This acts like a credential provider for Azure Devops and any other [public client app](https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-client-applications).

# Platform Support

| Operating System | Auth Broker Integration | Web Auth Flow | Device Code Flow | Token Caching | Multi-Account Support |
|------------------|-----------------------|-------------------------|---------------|------------------------------|---------------|
| Windows          | ✅ | ✅ | ✅ | ✅ | ⚠️ `--domain` account filtering |
| OSX (MacOS)      | ⚠️ via Web Browser | ✅ | ✅ | ✅ | ⚠️ `--domain` account filtering |
| Ubuntu (linux) <br/>‼️Releases coming soon | ⚠️ via Edge | ✅ | ✅ | ✅ | ⚠️ `--domain` account filtering |
<br/>

# Installation

## Windows

On Windows we provide a PowerShell bootstrap script, which will download and extract the application to
`%LOCALAPPDATA%\Programs\AzureAuth` and automatically add the `azureauth` binary to your `$PATH`. You can set an
alternative installation location via the `$AZUREAUTH_INSTALL_DIRECTORY` environment variable. We don't currently
provide a means of downloading the latest release, so you **must** specify your desired version via the
`$AZUREAUTH_VERSION` environment variable.

To install the application, run

```powershell
# 0.3.1 is an example. See https://github.com/AzureAD/microsoft-authentication-cli/releases for the latest.
$env:AZUREAUTH_VERSION = '0.3.1'
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
iex "& { $(irm https://raw.githubusercontent.com/AzureAD/microsoft-authentication-cli/main/install/install.ps1) } -Verbose"
```

Or, if you want a method more resilient to failure than `Invoke-Expression`, run

```powershell
# 0.3.1 is an example. See https://github.com/AzureAD/microsoft-authentication-cli/releases for the latest.
$env:AZUREAUTH_VERSION = '0.3.1'
$script = "${env:TEMP}\install.ps1"
$url = 'https://raw.githubusercontent.com/AzureAD/microsoft-authentication-cli/main/install/install.ps1'
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest $url -OutFile $script; if ($?) { &$script }; if ($?) { rm $script }
```

**Note**: The script does not signal currently running processes to update their environments, so you'll need to
relaunch applications before the `$PATH` changes take effect.

## macOS

On macOS we provide a shell bootstrap script, which will download and extract the application to `$HOME/.azureauth`
and automatically add the `azureauth` binary to your `$PATH`. You can set an alternative installation location via the
`$AZUREAUTH_INSTALL_DIRECTORY` environment variable. We don't currently provide a means of downloading the latest
release, so you **must** specify your desired version via the `$AZUREAUTH_VERSION` environment variable.

To install the application, run

```bash
# 0.3.1 is an example. See https://github.com/AzureAD/microsoft-authentication-cli/releases for the latest.
export AZUREAUTH_VERSION='0.3.1'
curl -sL https://raw.githubusercontent.com/AzureAD/microsoft-authentication-cli/main/install/install.sh | sh
```

**Note**: The script currently only updates the `$PATH` in `~/.bashrc` and `~/.zshrc`. It does not update the environment
of currently running processes, so you'll need to relaunch applications (or source your shell profile) before the `$PATH`
changes take effect.

# Using AzureAuth

Instructions on using AzureAuth CLI in your applications are available [here](docs/usage.md).

# Data Collection

The software may collect information about you and your use of the software and send it to Microsoft. Microsoft may use
this information to provide services and improve our products and services. You may turn off the telemetry as described
in the repository. There are also some features in the software that may enable you and Microsoft to collect data from
users of your applications. If you use these features, you must comply with applicable law, including providing
appropriate notices to users of your applications together with a copy of Microsoft’s privacy statement. Our privacy
statement is located at https://go.microsoft.com/fwlink/?LinkID=824704. You can learn more about data collection and
use in the help documentation and our privacy statement. Your use of the software operates as your consent to these
practices.

## Telemetry Is Off By Default

No telemetry will be collected unless you set the `AZUREAUTH_APPLICATION_INSIGHTS_INGESTION_TOKEN` environment variable
to a valid [Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)
ingestion token.

# Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft
trademarks or logos is subject to and must follow [Microsoft’s Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft
sponsorship. Any use of third-party trademarks or logos are subject to those third-party’s policies.
