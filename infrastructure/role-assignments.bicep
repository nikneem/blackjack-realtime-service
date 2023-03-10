param principalId string
param integrationResourceGroupName string

// resource storageAccountDataContributorRole 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
//   scope: resourceGroup()
//   name: '0a9a7e1f-b9d0-4cc4-a60d-0319b160aaa3'
// }
// module azureContainerApp 'br/BicepModules:authorization/roleassignments:0.1.10' = {
//   name: 'storageAccountDataReaderRoleAssignmentModule'
//   scope: resourceGroup()
//   params: {
//     principalId: principalId
//     roleDefinitionId: storageAccountDataContributorRole.id
//   }
// }

resource configurationDataReaderRole 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: resourceGroup()
  name: '516239f1-63e1-4d78-a4de-a74fb236a071'
}
module configurationReaderRoleAssignment 'br/BicepModules:authorization/roleassignments:0.1.10' = {
  name: 'configurationReaderRoleAssignmentModule'
  scope: resourceGroup(integrationResourceGroupName)
  params: {
    principalId: principalId
    roleDefinitionId: configurationDataReaderRole.id
  }
}

resource accessSecretsRole 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: resourceGroup()
  name: '4633458b-17de-408a-b874-0445c86b69e6'
}
module keyVaultSecretsAccessRoleAssignment 'br/BicepModules:authorization/roleassignments:0.1.10' = {
  name: 'keyVaultSecretsAccessRoleAssignmentModule'
  scope: resourceGroup(integrationResourceGroupName)
  params: {
    principalId: principalId
    roleDefinitionId: accessSecretsRole.id
  }
}
