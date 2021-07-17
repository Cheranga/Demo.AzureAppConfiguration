@description('The name of the app service plan.')
param planName string
param sku string
param capacity int

@description('The location where the app service plan resides.')
param planLocation string

resource asp_resource 'Microsoft.Web/serverfarms@2021-01-15'={
  name: planName
  kind:'app'
  location: planLocation
  sku:{
    capacity:capacity
    name:sku
  }  
}
