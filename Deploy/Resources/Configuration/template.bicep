param azConfigName string
param location string
param apiEnvironment string
param keyVaultName string

@secure()
param storageConnectionString string

var featureFlagValue = {
  id: 'searchCustomerById'
  description: 'search customer by id'
  enabled: true
}

var tenantId = subscription().subscriptionId

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

// Key vault references
resource kvStorageConnectionString 'Microsoft.AppConfiguration/configurationStores/keyValues@2020-07-01-preview'={
  name: 'StorageTableConfiguration:ConnectionString'
  parent:azconfig_resource
  properties:{
    value:string({uri:dbConnectionStringSecret.properties.secretUri})
    contentType: 'application/vnd.microsoft.appconfig.keyvaultref+json;charset=utf-8'
  }
  dependsOn:[
    azconfig_resource
    dbConnectionStringSecret
  ]
}

resource keyVault 'Microsoft.KeyVault/vaults@2016-10-01' = {
  name: keyVaultName
  location: location
  properties: {
    enabledForDeployment: true
    enabledForTemplateDeployment: true
    enabledForDiskEncryption: true
    tenantId: tenantId
    accessPolicies: [
      {
        tenantId: tenantId
        objectId: azconfig_resource.identity.principalId
        permissions: {
          secrets: [
            'get'
            'list'
          ]
        }
      }      
    ]
    sku: {
      name: 'standard'
      family: 'A'
    }
  }
  dependsOn:[
    azconfig_resource
  ] 
}


resource dbConnectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2019-09-01' = {
  name: '${keyVaultName}/storageConnectionString'
  properties: {
    value: storageConnectionString
  }
  dependsOn:[
    keyVault
  ]
}

output dbConnectionStringUri string = dbConnectionStringSecret.properties.secretUri
output azConfigPrincipalId string = azconfig_resource.identity.principalId
output azConfigResourceId string = '/subscriptions/${subscription().subscriptionId}/resourceGroups/${resourceGroup().name}/providers/Microsoft.AppConfiguration/configurationStores/${azConfigName}'
