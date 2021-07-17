param buildNumber string
param location string
param planCapacity int
param planName string
param planSku string

param appInsightsName string


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
