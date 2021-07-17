param azConfigName string
param location string

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

resource appconfigurations 'Microsoft.AppConfiguration/configurationStores/keyValues@2021-03-01-preview'={
  name: 'message'
  properties:{
    value:'the message'
  }
  parent:azconfig_resource
  dependsOn:[
    azconfig_resource
  ]
}
