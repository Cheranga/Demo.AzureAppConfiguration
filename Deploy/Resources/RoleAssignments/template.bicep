param roleDefinitionID string
param principalId string
param scopeId string

var roleAssignmentName_var = guid(principalId, roleDefinitionID, resourceGroup().id)

resource roleAssignmentName 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: roleAssignmentName_var
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitionID)
    principalId: principalId
    scope: scopeId
  }
}
