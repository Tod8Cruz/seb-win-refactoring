<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
    <xsl:output method="xml" indent="yes"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>
    <xsl:template match="wix:File[substring(@Source, string-length(@Source) - string-length('Monito.exe') + 1) = 'Monito.exe']">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
            <xsl:attribute name="Id">
                <xsl:text>MainExecutable</xsl:text>
            </xsl:attribute>
        </xsl:copy>
        <wix:File Id="ApplicationIconFile" Source="Resources\Application.ico" />
        <wix:RegistryKey Root="HKCR" Key="monito">
            <wix:RegistryValue Value="URL:Monito Protocol" Type="string" />
            <wix:RegistryValue Name="URL Protocol" Value="" Type="string" />
            <wix:RegistryValue Key="DefaultIcon" Value="[#ApplicationIconFile]" Type="string" />
            <wix:RegistryValue Key="shell\open\command" Value="&quot;[ApplicationDirectory]$(var.SafeExamBrowser.Runtime.TargetFileName)&quot; &quot;%1&quot;" Type="string" />
        </wix:RegistryKey>
        <wix:RegistryKey Root="HKCR" Key="smonito">
            <wix:RegistryValue Value="URL:Monito Secure Protocol" Type="string" />
            <wix:RegistryValue Name="URL Protocol" Value="" Type="string" />
            <wix:RegistryValue Key="DefaultIcon" Value="[#ApplicationIconFile]" Type="string" />
            <wix:RegistryValue Key="shell\open\command" Value="&quot;[ApplicationDirectory]$(var.SafeExamBrowser.Runtime.TargetFileName)&quot; &quot;%1&quot;" Type="string" />
        </wix:RegistryKey>
    </xsl:template>
</xsl:stylesheet>