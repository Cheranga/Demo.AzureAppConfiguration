// Common
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

module azAppConfigurationModule 'Configuration/template.bicep'={
  name: 'azAppConfig-${buildNumber}'
  params: {
    azConfigName: azConfigName
    location: location
  }  
}
