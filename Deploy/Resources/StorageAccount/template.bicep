param sgName string
param location string

resource storage_resource 'Microsoft.Storage/storageAccounts@2021-04-01'={
  name: sgName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'  
}

output storageAccountConnectionString string = 'DefaultEndpointsProtocol=https;AccountName=${sgName};AccountKey=${listKeys(resourceId(resourceGroup().name, 'Microsoft.Storage/storageAccounts', sgName), '2019-04-01').keys[0].value};EndpointSuffix=core.windows.net'
