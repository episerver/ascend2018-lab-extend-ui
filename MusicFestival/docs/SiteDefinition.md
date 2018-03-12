# Site Definition Api

The Site Definition Api exposes data about configured Episerver sites, with information about associated Content Roots and Languages enabled on the Start Page.

## Configuration Settings ##

The Site Definition Api can be enabled/disabled via the `SiteDefinitionApiEnabled` property on `ContentApiOptions`. When the Site Definition Api is disabled, requests made to the Api will result in a `404 Not Found `  response.

By default, The Site Definition Api only returns information about the current site, based upon the domain of the request. When the `MultiSiteFilteringEnabled` property of `ContentApiOptions` is set to `false`, information about all available configured sites in Episerver will be returned. 


## Example Request / Response ##


***Site Definition Request***

Returns information about the current site, or about all available sites when multi-site filtering is enabled

```javascript
    axios.get('/api/episerver/site/', {
        headers: {
            'Accept': 'application/json'
        }
    })
    .then(function (response) {
        console.log(response);
    })
    .catch(function (error) {
        console.log(error);
    });
```

***Response***

```json

[
    {
        "Name": "EPiServer.ContentApi.MusicFestival.Site",
        "Id": "443859c8-c7f4-4bab-865c-1a68f61a9828",
        "ContentRoots": {
            "ContentAssetsRoot": {
                "Id": 4,
                "WorkId": 0,
                "GuidValue": "99d57529-61f2-47c0-80c0-f91eca6af1ac",
                "ProviderName": null
            },
            "GlobalAssetsRoot": {
                "Id": 3,
                "WorkId": 0,
                "GuidValue": "e56f85d0-e833-4e02-976a-2d11fe4d598c",
                "ProviderName": null
            },
            "RootPage": {
                "Id": 1,
                "WorkId": 0,
                "GuidValue": "43f936c9-9b23-4ea3-97b2-61c538ad07c9",
                "ProviderName": null
            },
            "WasteBasket": {
                "Id": 2,
                "WorkId": 0,
                "GuidValue": "2f40ba47-f4fc-47ae-a244-0b909d4cf988",
                "ProviderName": null
            },
            "StartPage": {
                "Id": 5,
                "WorkId": 0,
                "GuidValue": "4079d7d5-e629-4a54-bb5f-3e7a87ed3092",
                "ProviderName": null
            }
        },
        "Languages": [
            {
                "IsMasterLanguage": true,
                "DisplayName": "English",
                "Name": "en"
            },
            {
                "IsMasterLanguage": false,
                "DisplayName": "Swedish",
                "Name": "sv"
            }
        ]
    },
    {
        "Name": "Demo Site",
        "Id": "c25cbde2-937e-40af-84a3-2c4c0fa3f71a",
        "ContentRoots": {
            "ContentAssetsRoot": {
                "Id": 4,
                "WorkId": 0,
                "GuidValue": "99d57529-61f2-47c0-80c0-f91eca6af1ac",
                "ProviderName": null
            },
            "GlobalAssetsRoot": {
                "Id": 3,
                "WorkId": 0,
                "GuidValue": "e56f85d0-e833-4e02-976a-2d11fe4d598c",
                "ProviderName": null
            },
            "RootPage": {
                "Id": 1,
                "WorkId": 0,
                "GuidValue": "43f936c9-9b23-4ea3-97b2-61c538ad07c9",
                "ProviderName": null
            },
            "WasteBasket": {
                "Id": 2,
                "WorkId": 0,
                "GuidValue": "2f40ba47-f4fc-47ae-a244-0b909d4cf988",
                "ProviderName": null
            },
            "StartPage": {
                "Id": 2082,
                "WorkId": 0,
                "GuidValue": "2259fcdc-ad07-4856-8c1b-3719449e549e",
                "ProviderName": null
            }
        },
        "Languages": [
            {
                "IsMasterLanguage": true,
                "DisplayName": "English",
                "Name": "en"
            }
        ]
    }
]
```