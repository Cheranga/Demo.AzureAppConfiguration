param apiName string
param apiLocation string
param rgName string
param planName string
param apiEnvironment string
param apiConfigUrl string
param websiteTimeZone string = 'AUS Eastern Standard Time'

@secure()
param appInsightsKey string

var subscriptionId = subscription().subscriptionId
var fullWebAppUriForProductionSlot = '${apiName}.azurewebsites.net'
var fullWebAppUriForStagingSlot = '${apiName}.scm.azurewebsites.net'

resource apiName_resource 'Microsoft.Web/sites@2019-08-01' = {
  name: apiName
  location: apiLocation
  properties: {    
    serverFarmId: resourceId('Microsoft.Web/serverfarms', planName)    
    hostNameSslStates: [
      {
        name: fullWebAppUriForProductionSlot
        sslState: 'Disabled'
        virtualIP: null
        thumbprint: null
        toUpdate: null
        hostType: 'Standard'
      }
      {
        name: fullWebAppUriForStagingSlot
        sslState: 'Disabled'
        virtualIP: null
        thumbprint: null
        toUpdate: null
        hostType: 'Repository'
      }
    ]
    siteConfig: {
      minTlsVersion: '1.2'
      ftpsState: 'Disabled'      
    }
    httpsOnly: true
    clientCertEnabled: false
    clientCertExclusionPaths: ''
    clientAffinityEnabled: false
  }
  identity: {
    type: 'SystemAssigned'
  }
  tags: {
    'hidden-related:/subscriptions/${subscriptionId}/resourcegroups/${rgName}/providers/Microsoft.Web/serverfarms/${planName}': 'empty'
  }
}

resource apiName_appsettings 'Microsoft.Web/sites/config@2019-08-01' = {
  parent: apiName_resource
  name: 'appsettings'
  properties: {
    AzureAppConfigurationUrl: apiConfigUrl
    ASPNETCORE_ENVIRONMENT: apiEnvironment
    APPINSIGHTS_INSTRUMENTATIONKEY: appInsightsKey
    DIAGNOSTICS_AZUREBLOBCONTAINERSASURL: ''
    DIAGNOSTICS_AZUREBLOBRETENTIONINDAYS: '90'
    WEBSITE_TIME_ZONE: websiteTimeZone
  }
}

resource apiName_Staging 'Microsoft.Web/sites/slots@2019-08-01' = {
  parent: apiName_resource
  kind: 'app'
  name: 'Staging'
  location: apiLocation  
  properties: {
    enabled: true
    serverFarmId: '/subscriptions/${subscriptionId}/resourcegroups/${rgName}/providers/Microsoft.Web/serverfarms/${planName}'
    reserved: false
    scmSiteAlsoStopped: false
    hostingEnvironmentProfile: null
    clientAffinityEnabled: false
    clientCertEnabled: false
    hostNamesDisabled: false
    containerSize: 0
    dailyMemoryTimeQuota: 0
    cloningInfo: null
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource apiName_Staging_appsettings 'Microsoft.Web/sites/slots/config@2019-08-01' = {
  parent: apiName_Staging
  name: 'appsettings'
  properties: {
    AzureAppConfigurationUrl: apiConfigUrl
    ASPNETCORE_ENVIRONMENT: apiEnvironment
    APPINSIGHTS_INSTRUMENTATIONKEY: appInsightsKey
    DIAGNOSTICS_AZUREBLOBCONTAINERSASURL: ''
    DIAGNOSTICS_AZUREBLOBRETENTIONINDAYS: '90'
    WEBSITE_TIME_ZONE: websiteTimeZone
  }
}

output productionApiPrincipalId string = apiName_resource.identity.principalId
output stagingApiPrincipalId string = apiName_Staging.identity.principalId
output tenantId string = apiName_resource.identity.tenantId
