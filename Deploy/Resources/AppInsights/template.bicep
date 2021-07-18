param appInsightsName string
param location string

resource appInsights_resource 'Microsoft.Insights/components@2020-02-02'={
  name: appInsightsName
  location: location
  kind: 'web'
  properties:{
    Application_Type:'web'
    Request_Source:'rest'
    Flow_Type:'Bluefield'
  }
}

output appInsightsKey string = appInsights_resource.properties.InstrumentationKey
