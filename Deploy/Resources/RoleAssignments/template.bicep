// param roleDefinitionID string
// param principalId string
// param scopeId string

// var roleAssignmentName_var = guid(principalId, roleDefinitionID, resourceGroup().id)

// resource roleAssignmentName 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
//   name: roleAssignmentName_var
//   properties: {
//     roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitionID)
//     principalId: principalId
//     scope: scopeId
//   }
// }


// param configStoreName string
// param storageConnectionStringUri string

// var storageConnectionStringRef ={
//   uri:storageConnectionStringUri
// }


// resource keyVaultStorageConnection 'Microsoft.AppConfiguration/configurationStores/keyValues@2020-07-01-preview' = {
//   name: 'keyValues/StorageTableConfiguration:ConnectionString'
//   parent: resourceId('Microsoft.','dsffg')
//   properties: {
//     value: string(storageConnectionStringRef)
//     contentType: 'application/vnd.microsoft.appconfig.keyvaultref+json;charset=utf-8'
//   }
// }



