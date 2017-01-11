function Remove-EPiCommonFromWebConfig {

	param ([xml]$webConfig)

	$modified = $false

	$commonSectionDeclaration = $webConfig.SelectSingleNode("/configuration/configSections/section[@name='episerver.common']")
	if ($commonSectionDeclaration -ne $null) {
		Write-Host "Removing episerver.common config section declaration."
		$commonSectionDeclaration.ParentNode.RemoveChild($commonSectionDeclaration) | Out-Null
		$modified = $true
	}

	$commonSection = $webConfig.SelectSingleNode("/configuration/episerver.common")
	if ($commonSection -ne $null) {
		Write-Host "Removing episerver.common config section."
		$commonSection.ParentNode.RemoveChild($commonSection) | Out-Null
		$modified = $true
	}

	$nhibernateSectionDeclaration = $webConfig.SelectSingleNode("/configuration/configSections/section[@name='nhibernate']")
	if ($nhibernateSectionDeclaration -ne $null) {
		Write-Host "Removing nhibernate config section declaration."
		$nhibernateSectionDeclaration.ParentNode.RemoveChild($nhibernateSectionDeclaration) | Out-Null
		$modified = $true
	}

	$nhibernateSection = $webConfig.SelectSingleNode("/configuration/nhibernate")
	if ($nhibernateSection -ne $null) {
		Write-Host "Removing nhibernate config section."
		$nhibernateSection.ParentNode.RemoveChild($nhibernateSection) | Out-Null
		$modified = $true
	}

	$hibernateConfigSectionDeclaration = $webConfig.SelectSingleNode("/configuration/configSections/section[@name='hibernate-configuration']")
	if ($hibernateConfigSectionDeclaration -ne $null) {
		Write-Host "Removing hibernate-configuration config section declaration."
		$hibernateConfigSectionDeclaration.ParentNode.RemoveChild($hibernateConfigSectionDeclaration) | Out-Null
		$modified = $true
	}

	$hibernateConfigSection = $webConfig.SelectSingleNode("/configuration/*[local-name()='hibernate-configuration']")
	if ($hibernateConfigSection -ne $null) {
		Write-Host "Removing hibernate-configuration config section."
		$hibernateConfigSection.ParentNode.RemoveChild($hibernateConfigSection) | Out-Null
		$modified = $true
	}

	$ns = @{x = 'urn:schemas-microsoft-com:asm.v1'}
	$bindingRedirects = $webConfig | Select-Xml -Namespace $ns `
		"/configuration/runtime/x:assemblyBinding/x:dependentAssembly[x:assemblyIdentity[starts-with(@name,'EPiServer.Common.')]]"
	if (($bindingRedirects | Measure-Object).Count -gt 0) {
		Write-Host 'Removing assembly binding redirects:'
		$bindingRedirects| Foreach-Object {
			$assemblyName = $_ | Select-Xml "x:assemblyIdentity/@name" -Namespace $ns
			Write-Host "Removing assembly binding redirect for $assemblyName"
			$_.Node.ParentNode.RemoveChild($_.Node) | Out-Null
		}
		$modified = $true
	}

	$roleProviders = $webConfig | Select-Xml "/configuration/system.web/roleManager/providers/add[starts-with(@type, 'EPiServer.Common.')]"
	if (($roleProviders | Measure-Object).Count -gt 0) {
		Write-Host 'Removing EPiServer Common role provider(s):'
		$roleProviders | Foreach-Object {
			$providerName = $_ | Select-Xml "@name"
			Write-Host "Removing role provider $providerName"
			$_.Node.ParentNode.RemoveChild($_.Node) | Out-Null
		}
		$modified = $true
	}

	$membershipProviders = $webConfig | Select-Xml "/configuration/system.web/membership/providers/add[starts-with(@type, 'EPiServer.Common.')]"
	if (($membershipProviders | Measure-Object).Count -gt 0) {
		Write-Host 'Removing EPiServer Common membership provider(s):'
		$membershipProviders | Foreach-Object {
			$providerName = $_ | Select-Xml "@name"
			Write-Host "Removing membership provider $providerName"
			$_.Node.ParentNode.RemoveChild($_.Node) | Out-Null
		}
		$modified = $true
	}

	$httpModules = $webConfig | Select-Xml "/configuration/system.webServer/modules/add[starts-with(@type, 'EPiServer.Common.')]"
	if (($httpModules | Measure-Object).Count -gt 0) {
		Write-Host 'Removing EPiServer Common http module(s):'
		$httpModules | Foreach-Object {
			$moduleName = $_ | Select-Xml "@name"
			Write-Host "Removing http module $moduleName"
			$_.Node.ParentNode.RemoveChild($_.Node) | Out-Null
		}
		$modified = $true
	}

	$commonShellModule = $webConfig.SelectSingleNode("/configuration/episerver.shell/protectedModules/add[@name='EPiServerCommon']")
	if ($commonShellModule -ne $null) {
		Write-Host "Removing EPiServer Common protected shell module."
		$commonShellModule.ParentNode.RemoveChild($commonShellModule) | Out-Null
		$modified = $true
	}

	$commonLocation = $webConfig.SelectSingleNode("/configuration/location[@path='EPiServerCommon']")
	if ($commonLocation -ne $null) {
		Write-Host "Removing EPiServer Common location element."
		$commonLocation.ParentNode.RemoveChild($commonLocation) | Out-Null
		$modified = $true
	}
	
	return $modified
}
# SIG # Begin signature block
# MIIZDwYJKoZIhvcNAQcCoIIZADCCGPwCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUmo999W6DZ0nypwPXBxL/0UrF
# QSSgghP/MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B
# AQUFADCBizELMAkGA1UEBhMCWkExFTATBgNVBAgTDFdlc3Rlcm4gQ2FwZTEUMBIG
# A1UEBxMLRHVyYmFudmlsbGUxDzANBgNVBAoTBlRoYXd0ZTEdMBsGA1UECxMUVGhh
# d3RlIENlcnRpZmljYXRpb24xHzAdBgNVBAMTFlRoYXd0ZSBUaW1lc3RhbXBpbmcg
# Q0EwHhcNMTIxMjIxMDAwMDAwWhcNMjAxMjMwMjM1OTU5WjBeMQswCQYDVQQGEwJV
# UzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFu
# dGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMjCCASIwDQYJKoZIhvcN
# AQEBBQADggEPADCCAQoCggEBALGss0lUS5ccEgrYJXmRIlcqb9y4JsRDc2vCvy5Q
# WvsUwnaOQwElQ7Sh4kX06Ld7w3TMIte0lAAC903tv7S3RCRrzV9FO9FEzkMScxeC
# i2m0K8uZHqxyGyZNcR+xMd37UWECU6aq9UksBXhFpS+JzueZ5/6M4lc/PcaS3Er4
# ezPkeQr78HWIQZz/xQNRmarXbJ+TaYdlKYOFwmAUxMjJOxTawIHwHw103pIiq8r3
# +3R8J+b3Sht/p8OeLa6K6qbmqicWfWH3mHERvOJQoUvlXfrlDqcsn6plINPYlujI
# fKVOSET/GeJEB5IL12iEgF1qeGRFzWBGflTBE3zFefHJwXECAwEAAaOB+jCB9zAd
# BgNVHQ4EFgQUX5r1blzMzHSa1N197z/b7EyALt0wMgYIKwYBBQUHAQEEJjAkMCIG
# CCsGAQUFBzABhhZodHRwOi8vb2NzcC50aGF3dGUuY29tMBIGA1UdEwEB/wQIMAYB
# Af8CAQAwPwYDVR0fBDgwNjA0oDKgMIYuaHR0cDovL2NybC50aGF3dGUuY29tL1Ro
# YXd0ZVRpbWVzdGFtcGluZ0NBLmNybDATBgNVHSUEDDAKBggrBgEFBQcDCDAOBgNV
# HQ8BAf8EBAMCAQYwKAYDVR0RBCEwH6QdMBsxGTAXBgNVBAMTEFRpbWVTdGFtcC0y
# MDQ4LTEwDQYJKoZIhvcNAQEFBQADgYEAAwmbj3nvf1kwqu9otfrjCR27T4IGXTdf
# plKfFo3qHJIJRG71betYfDDo+WmNI3MLEm9Hqa45EfgqsZuwGsOO61mWAK3ODE2y
# 0DGmCFwqevzieh1XTKhlGOl5QGIllm7HxzdqgyEIjkHq3dlXPx13SYcqFgZepjhq
# IhKjURmDfrYwggSjMIIDi6ADAgECAhAOz/Q4yP6/NW4E2GqYGxpQMA0GCSqGSIb3
# DQEBBQUAMF4xCzAJBgNVBAYTAlVTMR0wGwYDVQQKExRTeW1hbnRlYyBDb3Jwb3Jh
# dGlvbjEwMC4GA1UEAxMnU3ltYW50ZWMgVGltZSBTdGFtcGluZyBTZXJ2aWNlcyBD
# QSAtIEcyMB4XDTEyMTAxODAwMDAwMFoXDTIwMTIyOTIzNTk1OVowYjELMAkGA1UE
# BhMCVVMxHTAbBgNVBAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMTQwMgYDVQQDEytT
# eW1hbnRlYyBUaW1lIFN0YW1waW5nIFNlcnZpY2VzIFNpZ25lciAtIEc0MIIBIjAN
# BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAomMLOUS4uyOnREm7Dv+h8GEKU5Ow
# mNutLA9KxW7/hjxTVQ8VzgQ/K/2plpbZvmF5C1vJTIZ25eBDSyKV7sIrQ8Gf2Gi0
# jkBP7oU4uRHFI/JkWPAVMm9OV6GuiKQC1yoezUvh3WPVF4kyW7BemVqonShQDhfu
# ltthO0VRHc8SVguSR/yrrvZmPUescHLnkudfzRC5xINklBm9JYDh6NIipdC6Anqh
# d5NbZcPuF3S8QYYq3AhMjJKMkS2ed0QfaNaodHfbDlsyi1aLM73ZY8hJnTrFxeoz
# C9Lxoxv0i77Zs1eLO94Ep3oisiSuLsdwxb5OgyYI+wu9qU+ZCOEQKHKqzQIDAQAB
# o4IBVzCCAVMwDAYDVR0TAQH/BAIwADAWBgNVHSUBAf8EDDAKBggrBgEFBQcDCDAO
# BgNVHQ8BAf8EBAMCB4AwcwYIKwYBBQUHAQEEZzBlMCoGCCsGAQUFBzABhh5odHRw
# Oi8vdHMtb2NzcC53cy5zeW1hbnRlYy5jb20wNwYIKwYBBQUHMAKGK2h0dHA6Ly90
# cy1haWEud3Muc3ltYW50ZWMuY29tL3Rzcy1jYS1nMi5jZXIwPAYDVR0fBDUwMzAx
# oC+gLYYraHR0cDovL3RzLWNybC53cy5zeW1hbnRlYy5jb20vdHNzLWNhLWcyLmNy
# bDAoBgNVHREEITAfpB0wGzEZMBcGA1UEAxMQVGltZVN0YW1wLTIwNDgtMjAdBgNV
# HQ4EFgQURsZpow5KFB7VTNpSYxc/Xja8DeYwHwYDVR0jBBgwFoAUX5r1blzMzHSa
# 1N197z/b7EyALt0wDQYJKoZIhvcNAQEFBQADggEBAHg7tJEqAEzwj2IwN3ijhCcH
# bxiy3iXcoNSUA6qGTiWfmkADHN3O43nLIWgG2rYytG2/9CwmYzPkSWRtDebDZw73
# BaQ1bHyJFsbpst+y6d0gxnEPzZV03LZc3r03H0N45ni1zSgEIKOq8UvEiCmRDoDR
# EfzdXHZuT14ORUZBbg2w6jiasTraCXEQ/Bx5tIB7rGn0/Zy2DBYr8X9bCT2bW+IW
# yhOBbQAuOA2oKY8s4bL0WqkBrxWcLC9JG9siu8P+eJRRw4axgohd8D20UaF5Mysu
# e7ncIAkTcetqGVvP6KUwVyyJST+5z3/Jvz4iaGNTmr1pdKzFHTx/kuDDvBzYBHUw
# ggVUMIIEPKADAgECAhBqBz1Yk9Ce+JomHWkTBhgAMA0GCSqGSIb3DQEBBQUAMIG0
# MQswCQYDVQQGEwJVUzEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xHzAdBgNVBAsT
# FlZlcmlTaWduIFRydXN0IE5ldHdvcmsxOzA5BgNVBAsTMlRlcm1zIG9mIHVzZSBh
# dCBodHRwczovL3d3dy52ZXJpc2lnbi5jb20vcnBhIChjKTEwMS4wLAYDVQQDEyVW
# ZXJpU2lnbiBDbGFzcyAzIENvZGUgU2lnbmluZyAyMDEwIENBMB4XDTEzMDIwNTAw
# MDAwMFoXDTE2MDQwNTIzNTk1OVowgZcxCzAJBgNVBAYTAlNFMQowCAYDVQQIEwEt
# MQ4wDAYDVQQHEwVLSVNUQTEVMBMGA1UEChQMRVBpU2VydmVyIEFCMT4wPAYDVQQL
# EzVEaWdpdGFsIElEIENsYXNzIDMgLSBNaWNyb3NvZnQgU29mdHdhcmUgVmFsaWRh
# dGlvbiB2MjEVMBMGA1UEAxQMRVBpU2VydmVyIEFCMIIBIjANBgkqhkiG9w0BAQEF
# AAOCAQ8AMIIBCgKCAQEAo6coNqVVn2Rk4HBEl0kc/HO+PttBuDrEx/9fKLONe3yT
# SFWk6dg7/Lv1l+uSTwt4GbWkk3HU4tRRd2gPJ3AK14AysycQRE9T0H5mhcJntXnz
# 6i6rOOQjEqmjipcu1iO1BPl8OSEK3h37kjjhtPCei2KpViH2icmVfVgtevF988qh
# n7V/B66QtQGjl44gBAI3JBgUDUhCCFO+d9+tY6gYx9SR9+OwYWNusRpEG4wHlzpo
# mK4xIcrk6CBfktyEjDRs7ZCNOdL3mWvPKVeZjj3+f+XPrARmEOkBsCCDuRPG2bRK
# /3gLrAfVP5L73EHHNgLqS2uzPppChulRvIKjnUyOVwIDAQABo4IBezCCAXcwCQYD
# VR0TBAIwADAOBgNVHQ8BAf8EBAMCB4AwQAYDVR0fBDkwNzA1oDOgMYYvaHR0cDov
# L2NzYzMtMjAxMC1jcmwudmVyaXNpZ24uY29tL0NTQzMtMjAxMC5jcmwwRAYDVR0g
# BD0wOzA5BgtghkgBhvhFAQcXAzAqMCgGCCsGAQUFBwIBFhxodHRwczovL3d3dy52
# ZXJpc2lnbi5jb20vcnBhMBMGA1UdJQQMMAoGCCsGAQUFBwMDMHEGCCsGAQUFBwEB
# BGUwYzAkBggrBgEFBQcwAYYYaHR0cDovL29jc3AudmVyaXNpZ24uY29tMDsGCCsG
# AQUFBzAChi9odHRwOi8vY3NjMy0yMDEwLWFpYS52ZXJpc2lnbi5jb20vQ1NDMy0y
# MDEwLmNlcjAfBgNVHSMEGDAWgBTPmanqeyb0S8mOj9fwBSbv49KnnTARBglghkgB
# hvhCAQEEBAMCBBAwFgYKKwYBBAGCNwIBGwQIMAYBAQABAf8wDQYJKoZIhvcNAQEF
# BQADggEBAIk7DJSHwVYLgVDJzo1GSsMElW0XhAkl167CGHP0q18xgRCEZsb1M93u
# Z6uSnJWbtnGrxHSjbOxvWPUQSChMq7h+aZdw/emdFpZ5g3tbKcTZN/1l8pREvPG7
# vO/UUmXSG20xezxcuzM2bRgIYxmFIHNn6XXVORkWVGujm/zo/dYVDPjH1udFZ5nj
# IenD/YeO2ZjvnZssAoyTZuDhpf3qUtEff2Kc+PXVYoMsk8Q4TO74ps6DbpqddLDg
# k73Xbyr++tvmhZIL8XSzB9j1thEijIwYFn6k2TMls4pVQ8s/37oJcvwZ/KPICLUY
# +A8+Kx6iywx7QN1mfwEekzsKiYA7LHswggYKMIIE8qADAgECAhBSAOWqJVb8Gobt
# lsnUSzPHMA0GCSqGSIb3DQEBBQUAMIHKMQswCQYDVQQGEwJVUzEXMBUGA1UEChMO
# VmVyaVNpZ24sIEluYy4xHzAdBgNVBAsTFlZlcmlTaWduIFRydXN0IE5ldHdvcmsx
# OjA4BgNVBAsTMShjKSAyMDA2IFZlcmlTaWduLCBJbmMuIC0gRm9yIGF1dGhvcml6
# ZWQgdXNlIG9ubHkxRTBDBgNVBAMTPFZlcmlTaWduIENsYXNzIDMgUHVibGljIFBy
# aW1hcnkgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkgLSBHNTAeFw0xMDAyMDgwMDAw
# MDBaFw0yMDAyMDcyMzU5NTlaMIG0MQswCQYDVQQGEwJVUzEXMBUGA1UEChMOVmVy
# aVNpZ24sIEluYy4xHzAdBgNVBAsTFlZlcmlTaWduIFRydXN0IE5ldHdvcmsxOzA5
# BgNVBAsTMlRlcm1zIG9mIHVzZSBhdCBodHRwczovL3d3dy52ZXJpc2lnbi5jb20v
# cnBhIChjKTEwMS4wLAYDVQQDEyVWZXJpU2lnbiBDbGFzcyAzIENvZGUgU2lnbmlu
# ZyAyMDEwIENBMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA9SNLXqXX
# irsy6dRX9+/kxyZ+rRmY/qidfZT2NmsQ13WBMH8EaH/LK3UezR0IjN9plKc3o5x7
# gOCZ4e43TV/OOxTuhtTQ9Sc1vCULOKeMY50Xowilq7D7zWpigkzVIdob2fHjhDuK
# Kk+FW5ABT8mndhB/JwN8vq5+fcHd+QW8G0icaefApDw8QQA+35blxeSUcdZVAccA
# JkpAPLWhJqkMp22AjpAle8+/PxzrL5b65Yd3xrVWsno7VDBTG99iNP8e0fRakyiF
# 5UwXTn5b/aSTmX/fze+kde/vFfZH5/gZctguNBqmtKdMfr27Tww9V/Ew1qY2jtaA
# dtcZLqXNfjQtiQIDAQABo4IB/jCCAfowEgYDVR0TAQH/BAgwBgEB/wIBADBwBgNV
# HSAEaTBnMGUGC2CGSAGG+EUBBxcDMFYwKAYIKwYBBQUHAgEWHGh0dHBzOi8vd3d3
# LnZlcmlzaWduLmNvbS9jcHMwKgYIKwYBBQUHAgIwHhocaHR0cHM6Ly93d3cudmVy
# aXNpZ24uY29tL3JwYTAOBgNVHQ8BAf8EBAMCAQYwbQYIKwYBBQUHAQwEYTBfoV2g
# WzBZMFcwVRYJaW1hZ2UvZ2lmMCEwHzAHBgUrDgMCGgQUj+XTGoasjY5rw8+AatRI
# GCx7GS4wJRYjaHR0cDovL2xvZ28udmVyaXNpZ24uY29tL3ZzbG9nby5naWYwNAYD
# VR0fBC0wKzApoCegJYYjaHR0cDovL2NybC52ZXJpc2lnbi5jb20vcGNhMy1nNS5j
# cmwwNAYIKwYBBQUHAQEEKDAmMCQGCCsGAQUFBzABhhhodHRwOi8vb2NzcC52ZXJp
# c2lnbi5jb20wHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMDMCgGA1UdEQQh
# MB+kHTAbMRkwFwYDVQQDExBWZXJpU2lnbk1QS0ktMi04MB0GA1UdDgQWBBTPmanq
# eyb0S8mOj9fwBSbv49KnnTAfBgNVHSMEGDAWgBR/02Wnwt3su/AwCfNDOfoCrzMx
# MzANBgkqhkiG9w0BAQUFAAOCAQEAViLmNKTEYctIuQGtVqhkD9mMkcS7zAzlrXqg
# In/fRzhKLWzRf3EafOxwqbHwT+QPDFP6FV7+dJhJJIWBJhyRFEewTGOMu6E01MZF
# 6A2FJnMD0KmMZG3ccZLmRQVgFVlROfxYFGv+1KTteWsIDEFy5zciBgm+I+k/RJoe
# 6WGdzLGQXPw90o2sQj1lNtS0PUAoj5sQzyMmzEsgy5AfXYxMNMo82OU31m+lIL00
# 6ybZrg3nxZr3obQhkTNvhuhYuyV8dA5Y/nUbYz/OMXybjxuWnsVTdoRbnK2R+qzt
# k7pdyCFTwoJTY68SDVCHERs9VFKWiiycPZIaCJoFLseTpUiR0zGCBHowggR2AgEB
# MIHJMIG0MQswCQYDVQQGEwJVUzEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xHzAd
# BgNVBAsTFlZlcmlTaWduIFRydXN0IE5ldHdvcmsxOzA5BgNVBAsTMlRlcm1zIG9m
# IHVzZSBhdCBodHRwczovL3d3dy52ZXJpc2lnbi5jb20vcnBhIChjKTEwMS4wLAYD
# VQQDEyVWZXJpU2lnbiBDbGFzcyAzIENvZGUgU2lnbmluZyAyMDEwIENBAhBqBz1Y
# k9Ce+JomHWkTBhgAMAkGBSsOAwIaBQCgeDAYBgorBgEEAYI3AgEMMQowCKACgACh
# AoAAMBkGCSqGSIb3DQEJAzEMBgorBgEEAYI3AgEEMBwGCisGAQQBgjcCAQsxDjAM
# BgorBgEEAYI3AgEVMCMGCSqGSIb3DQEJBDEWBBQV5UZdsXw64qUVtjgVAjpxuI1y
# LzANBgkqhkiG9w0BAQEFAASCAQCRvuWEsS01lcEvWWV3W9BdE/eFtwFObwSD7hgB
# KqLB4gsokLIGHX78l8w8Q3yUKjA4aLNsJbdm2+vTGd2QY8ktkzd5I5MLuIjvd5Z1
# 1bpFKMPN7nYdJKePMfqDbCxplpTUeoJv1Tu4sudKO5UL0b1uuUO7OdXKBwUS1Z3F
# Gxwck+GogqDMihUnR50/l+CbqeWuq4ghfrE9HYXAQ/lNRsExcgnCfgYQLB2FC2Fi
# TLjx0NuhstuBOKXVBOIldmU3GpUxBbyqi+kiy2EBpeqmm78ckc7VT49x0NaouBut
# +iHXvrNm0da1/jJfpMV94A1waKgv2v6NSfgCALNoaY6Y3nwGoYICCzCCAgcGCSqG
# SIb3DQEJBjGCAfgwggH0AgEBMHIwXjELMAkGA1UEBhMCVVMxHTAbBgNVBAoTFFN5
# bWFudGVjIENvcnBvcmF0aW9uMTAwLgYDVQQDEydTeW1hbnRlYyBUaW1lIFN0YW1w
# aW5nIFNlcnZpY2VzIENBIC0gRzICEA7P9DjI/r81bgTYapgbGlAwCQYFKw4DAhoF
# AKBdMBgGCSqGSIb3DQEJAzELBgkqhkiG9w0BBwEwHAYJKoZIhvcNAQkFMQ8XDTE1
# MDQxNzA4MzgxNFowIwYJKoZIhvcNAQkEMRYEFAqzLOnhMc2FML/0OfmmgOWqqlHV
# MA0GCSqGSIb3DQEBAQUABIIBAC5WvYxuYolBWg1C6hfjW2cO6ntgA7eOaYKnXjIZ
# v0EpbC4RiEqXoZqNG+Msw0tOSo5FSo9TVloGNo7nTPTNhZvGEAdVaAgrdi3+b/Pc
# X4A9LxjIqwuZg3ZIgp/7g0JH9Z48COGiITH6JrDqojMBsw5K5R0reYvdAJ4mmxW6
# HoWzJGv5qs6VE5tEionjRGFfEDiazCgkyGpty+aYV6YolWNB0EQfZlEQtI7KB0I5
# 6Y3UX41xYw0L+Fu/hIpmaRyZdVrDwSCH5w4j9mR8lVEvUuDYiOAcDlb4Xk+qRivT
# AV22YHF73nRmhPLAjVKmM5wTvOOXAdLIABf7P5YPmDnjFD4=
# SIG # End signature block