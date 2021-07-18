param location string
param keyVaultName string
param tenantId string
param appConfigPrincipalId string

@secure()
param storageConnectionString string


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

output dbConnectionStringUri string = dbConnectionStringSecret.properties.secretUri
