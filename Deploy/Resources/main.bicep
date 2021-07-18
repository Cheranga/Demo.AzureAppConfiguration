// Common
param rgName string
param buildNumber string
param location string

// App service plan
param planCapacity int
param planName string
param planSku string

// App insights
param appInsightsName string

// Storage account
param sgName string

// Azure app configuration
param azConfigName string

// Key vault
param keyVaultName string


// Customers API
param apiEnvironment string
param apiName string
var apiConfigUrl = 'https://${azConfigName}.azconfig.io'

// Role assignment
param azureConfigDataReader string


module aspModule 'AppServicePlan/template.bicep' ={
  name: 'asp-${buildNumber}'
  params: {
    capacity: planCapacity
    planLocation: location
    planName: planName
    sku: planSku
  }  
}

module appInsightsModule 'AppInsights/template.bicep'={
  name: 'appins-${buildNumber}'
  params: {
    appInsightsName: appInsightsName 
    location: location
  }  
}

module storageAccountModule 'StorageAccount/template.bicep'={
  name: 'storage-${buildNumber}'
  params: {
    location: location
    sgName: sgName    
  }  
}

module apiModule 'API/template.bicep'={
  name: 'customersapi-${buildNumber}'
  params: {
    apiConfigUrl: apiConfigUrl
    apiEnvironment: apiEnvironment
    apiLocation: location
    apiName: apiName
    appInsightsKey: appInsightsModule.outputs.appInsightsKey
    planName: planName
    rgName: rgName
  }
  dependsOn:[
    aspModule
    azAppConfigurationModule
  ]  
}

module akvModule 'KeyVault/template.bicep'={
  name: 'akv-${buildNumber}'
  params: {
    appConfigPrincipalId: azAppConfigurationModule.outputs.azConfigPrincipalId
    keyVaultName: keyVaultName
    location: location
    storageConnectionString: storageAccountModule.outputs.storageAccountConnectionString
    tenantId: subscription().tenantId
  }
  dependsOn:[
    azAppConfigurationModule
  ]  
}

module azAppConfigurationModule 'Configuration/template.bicep'={
  name: 'azAppConfig-${buildNumber}'
  params: {
    azConfigName: azConfigName
    location: location
    apiEnvironment:apiEnvironment
  }  
}

// module roleAssignmentModule 'RoleAssignments/template.bicep'={
//   name: 'rbac-${buildNumber}'
//   params: {
//     principalId: apiModule.outputs.productionApiPrincipalId
//     roleDefinitionID: azureConfigDataReader
//     scopeId: resourceGroup().id
//   }
//   dependsOn:[
//     apiModule
//     azAppConfigurationModule
//   ]
// }
