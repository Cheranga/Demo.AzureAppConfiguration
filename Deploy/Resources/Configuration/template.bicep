param azConfigName string
param location string
param apiEnvironment string

var featureFlagValue = {
  id: 'searchCustomerById'
  description: 'search customer by id'
  enabled: true
}

resource azconfig_resource 'Microsoft.AppConfiguration/configurationStores@2021-03-01-preview'={
  name: azConfigName
  location: location
  sku: {
    name: 'Standard'
  }
  identity:{
    type:'SystemAssigned'    
  }  
}

// Configurations
resource appconfigurations 'Microsoft.AppConfiguration/configurationStores/keyValues@2021-03-01-preview'={
  name: 'message$${apiEnvironment}'
  properties:{
    value:'the message in dev'    
  }
  parent:azconfig_resource
  dependsOn:[
    azconfig_resource
  ]
}

// Feature flags
resource appFeatures 'Microsoft.AppConfiguration/configurationStores/keyValues@2021-03-01-preview'={
  name: '.appconfig.featureflag~2f${featureFlagValue.id}$${apiEnvironment}'
  properties:{
    value:string(featureFlagValue)
    contentType:'application/vnd.microsoft.appconfig.ff+json;charset=utf-8'
  }
  parent:azconfig_resource
  dependsOn:[
    azconfig_resource
  ]
}

output azConfigPrincipalId string = azconfig_resource.identity.principalId
output azConfigResourceId string = '/subscriptions/${subscription().subscriptionId}/resourceGroups/${resourceGroup().name}/providers/Microsoft.AppConfiguration/configurationStores/${azConfigName}'
