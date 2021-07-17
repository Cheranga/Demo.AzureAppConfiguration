param buildNumber string
param location string
param planCapacity int
param planName string
param planSku string


module aspModule 'AppServicePlan/template.bicep' ={
  name: 'asp-${buildNumber}'
  params: {
    capacity: planCapacity
    planLocation: location
    planName: planName
    sku: planSku
  }  
}
