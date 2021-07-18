param location string
param keyVaultName string
param tenantId string
param appConfigPrincipalId string

@secure()
param storageConnectionString string

@secure()
param sendOnlyServiceBusConnectionString string

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
        objectId: appConfigPrincipalId
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

resource sendOnlyServiceBusConnectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2019-09-01' = {
  name: '${keyVaultName}/sendOnlyServiceBusConnectionString'
  properties: {
    value: sendOnlyServiceBusConnectionString
  }
  dependsOn:[
    keyVault
  ]
}

output dbConnectionStringUri string = dbConnectionStringSecret.properties.secretUri
output sendOnlySecretUrl string = sendOnlyServiceBusConnectionStringSecret.properties.secretUri



