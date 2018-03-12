# Content Api

The Content Api exposes familiar operations from `IContentLoader`, such as Get, GetItems, GetChildren, and GetAncestors, with support for all content versions, personalization, proper access control, and a feature for loading referenced content in the same request.

## Localization ##

Content can be retrieved in one language within the same request to the Content Api endpoint. In order to indicate the desired language, attach the `Accept-Language` header to your request. If you do not provide a header value, it will default to the context of the request, based on the URL and configured sites in Episerver.

```
Accept-Language: en
Accept-Language: sv
Accept-Language: en-US
```

## Access Control ##

The Content Api returns content based on what the current user has access to view.

Users can authenticate via Cookie Authentication or Content API Authorization using OAuth, and either method can be used to call the API from an authenticated context.

## Expand (Returning Referenced Content) ##

Use the `expand` parameter on all Content Api operations to return data from reference properties in the same request.

```
?expand=MyContentReference
?expand=MyContentReference,MyContentArea
?expand=*
```

The following property types can be expanded: Content Area, Content Reference, Content Reference List, Link Collection

When reference properties are expanded, the full object for the referenced IContent instances will be returned in the response on the "ExpandedValue" property of that property's object.

Properties on objects that are returned in the ExpandedValue cannot be expanded further in the same request.

## Example Requests / Responses ##

All example request use [Axios](https://github.com/axios/axios) for generating requests in Javascript.

Add Axios (and a polyfill for UrlSearchParams for older browsers) via CDN for easy testing:

```html
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/url-search-params/0.10.0/url-search-params.js"></script>
```

### Get Content By Reference ###

Returns a content model based on the provided complex reference in the format of contentID[_workID[_providerName]]

****Request****

```javascript
    axios.get('/api/episerver/content/7', {
            headers: {
                'Accept': 'application/json',
                'Accept-Language': 'en'
            }
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
```

****Response****

```json

{
	"ContentLink": {
		"Id": 7,
		"WorkId": 0,
		"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
		"ProviderName": null
	},
	"Name": "Music Festival App",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "AppRootPage"],
	"ParentLink": {
		"Id": 5,
		"WorkId": 0,
		"GuidValue": "5894fd79-046d-4823-8086-fceffa5c60cb",
		"ProviderName": null
	},
	"RouteSegment": "music-festival-app",
	"Url": null,
	"Changed": "2017-12-01T19:44:16Z",
	"Created": "2017-12-01T19:16:41Z",
	"StartPublish": "2017-12-01T19:17:03Z",
	"StopPublish": null,
	"Saved": "2017-12-01T19:44:16Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"MenuItems": {
		"Value": [{
			"ContentLink": {
				"Id": 9,
				"WorkId": 0,
				"GuidValue": "aecb9c55-d73c-4dd4-a67b-2cfc335180a9",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	},
	"FestivalDayDetailBlocks": {
		"Value": [{
			"ContentLink": {
				"Id": 45,
				"WorkId": 0,
				"GuidValue": "2c171aad-e1ef-4d4a-b61a-3884069047c3",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 46,
				"WorkId": 0,
				"GuidValue": "f424ec20-7abe-49ed-b234-0b1973b0672d",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	}
}
```
#

### Get Content by Guid ###

Returns content based on the provided content guid:

***Request***

```javascript

    axios.get('/api/episerver/content/8377a392-8614-4cdd-8054-fd044dab1668', {
        headers: {
            'Accept': 'application/json',
            'Accept-Language': 'en'
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

{
    "ContentLink": {
        "Id": 5,
        "WorkId": 0,
        "GuidValue": "4079d7d5-e629-4a54-bb5f-3e7a87ed3092",
        "ProviderName": null
    },
    "Name": "Start Page",
    "Language": {
        "DisplayName": "English",
        "Name": "en"
    },
    "ExistingLanguages": [
        {
            "DisplayName": "English",
            "Name": "en"
        },
        {
            "DisplayName": "Swedish",
            "Name": "sv"
        }
    ],
    "MasterLanguage": {
        "DisplayName": "English",
        "Name": "en"
    },
    "ContentType": [
        "Page",
        "StartPage"
    ],
    "ParentLink": {
        "Id": 1,
        "WorkId": 0,
        "GuidValue": "43f936c9-9b23-4ea3-97b2-61c538ad07c9",
        "ProviderName": null
    },
    "RouteSegment": "start-page",
    "Url": null,
    "Modified": "2017-11-22T16:27:58Z",
    "Created": "2017-11-22T16:27:53Z",
    "StartPublish": "2017-11-22T06:00:00Z",
    "StopPublish": null,
    "Saved": "2017-11-22T16:27:58Z",
    "Status": "Published",
    "Category": {
        "Value": null,
        "PropertyDataType": "PropertyCategory"
    }
}
```
#

### Get Children ###

Returns a collection of child content based on the provided parent content reference.

***Request***

```javascript
    axios.get('/api/episerver/content/7/children', {
            headers: {
                'Accept': 'application/json',
                'Accept-Language': 'en'
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

[{
	"ContentLink": {
		"Id": 8,
		"WorkId": 0,
		"GuidValue": "205d0b4f-c478-4c7d-9bd7-1f774ca4ca2d",
		"ProviderName": null
	},
	"Name": "Discover",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "InformationPage"],
	"ParentLink": {
		"Id": 7,
		"WorkId": 0,
		"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
		"ProviderName": null
	},
	"RouteSegment": "discover",
	"Url": null,
	"Changed": "2017-11-28T21:30:20Z",
	"Created": "2017-11-28T21:21:51Z",
	"StartPublish": "2017-11-28T21:30:20Z",
	"StopPublish": null,
	"Saved": "2017-12-20T17:08:55Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"Title": {
		"Value": "discover",
		"PropertyDataType": "PropertyLongString"
	},
	"HeroContentArea": {
		"Value": [{
			"ContentLink": {
				"Id": 29,
				"WorkId": 0,
				"GuidValue": "594b0639-9e15-438d-9d5a-f4d7c420736d",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	},
	"Preamble": {
		"Value": "",
		"PropertyDataType": "PropertyLongString"
	},
	"MainBody": {
		"Value": "\n<h2>make the last day<br />the best day</h2>\n<p>with so much to see, hear and taste, here's what's going on during the final day of Music Festival.</p>\n\n",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"MainContant": {
		"Value": [{
			"ContentLink": {
				"Id": 12,
				"WorkId": 0,
				"GuidValue": "d449799f-2f2f-4b4d-87c5-f3204b04070b",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 33,
				"WorkId": 0,
				"GuidValue": "a16488c9-2e08-4917-82ce-21db74053212",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 23,
				"WorkId": 0,
				"GuidValue": "14b73395-4f23-4521-b28f-b66bf01de1ff",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 14,
				"WorkId": 0,
				"GuidValue": "0cbc322e-b7fa-40ed-b7c6-0bf5f26e87b9",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 13,
				"WorkId": 0,
				"GuidValue": "0d24367a-c7a2-49ea-8b6f-7c1ccc1ee7a4",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 15,
				"WorkId": 0,
				"GuidValue": "1e849251-1ec3-46fe-9184-c4d4c9ea9134",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 21,
				"WorkId": 0,
				"GuidValue": "db71ff92-9e76-4342-8cb5-9d5fd0f38473",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 16,
				"WorkId": 0,
				"GuidValue": "d7d003fb-4b76-4621-bdae-ff3aced3d3f7",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 28,
				"WorkId": 0,
				"GuidValue": "f75b6348-039a-408b-8e13-dfd787ea6edf",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 32,
				"WorkId": 0,
				"GuidValue": "fab5095b-4dcb-4b99-84e5-da5d14ada5ee",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	}
}, {
	"ContentLink": {
		"Id": 9,
		"WorkId": 0,
		"GuidValue": "aecb9c55-d73c-4dd4-a67b-2cfc335180a9",
		"ProviderName": null
	},
	"Name": "Info",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "InformationPage"],
	"ParentLink": {
		"Id": 7,
		"WorkId": 0,
		"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
		"ProviderName": null
	},
	"RouteSegment": "info",
	"Url": null,
	"Changed": "2017-11-28T21:26:39Z",
	"Created": "2017-11-28T21:17:55Z",
	"StartPublish": "2017-11-28T21:18:30Z",
	"StopPublish": null,
	"Saved": "2017-12-11T16:57:29Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"Title": {
		"Value": "Info",
		"PropertyDataType": "PropertyLongString"
	},
	"HeroContentArea": {
		"Value": [{
			"ContentLink": {
				"Id": 47,
				"WorkId": 0,
				"GuidValue": "e9e2c61f-b6d6-4bbe-9272-c94ee5eb5b01",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	},
	"Preamble": {
		"Value": "",
		"PropertyDataType": "PropertyLongString"
	},
	"MainBody": {
		"Value": "<h2>Accommodations</h2>\n<div>Check out our MusFes Hotel Packages! If you’re coming in from out of town, purchase a package with your wristband and book lodging near the festival. You’ll also receive exclusive Music Festival Merchandise!</div>\n<h2>Transportation</h2>\n<div>RideWithMe is the official rideshare partner of Music Festival, available to help transport you to and from the festival. Carpool together with friends and save some money!</div>\n\n<h2>Locker &amp; Storage</h2>\n<div>Want a safe and secure place to store your things while you attend Music Festival? Personal lockers will be available so you can free up your hands and avoid losing any personal items during the fest.</div>\n\n<h2>First Aid</h2>\n<div>We make every effort to create a safe environment on the festival grounds, including public and private security and medical staff throughout Woods Park. If you need any assistance, seek out the medical tent, or look for a peace officer or festival staff member.</div>\n\n<div><a href=\"http://www.google.com\">Contact Us For More Information</a></div>",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"MainContant": {
		"Value": null,
		"PropertyDataType": "PropertyContentArea"
	}
}, {
	"ContentLink": {
		"Id": 11,
		"WorkId": 0,
		"GuidValue": "f42d6a01-b3a3-46c4-b20a-605c82ca0152",
		"ProviderName": null
	},
	"Name": "Artists",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "ArtistContainerPage"],
	"ParentLink": {
		"Id": 7,
		"WorkId": 0,
		"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
		"ProviderName": null
	},
	"RouteSegment": "artists",
	"Url": null,
	"Changed": "2017-11-22T17:17:30Z",
	"Created": "2017-11-22T17:17:25Z",
	"StartPublish": "2017-11-22T17:17:30Z",
	"StopPublish": null,
	"Saved": "2017-12-02T01:19:23Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	}
}]
```
#

### Get Ancestors ###

Returns a collection of content that are ancestors of the provided content reference:

***Request***

```javascript
    axios.get('/api/episerver/content/7/ancestors', {
        headers: {
            'Accept': 'application/json',
            'Accept-Language': 'en'
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

[{
	"ContentLink": {
		"Id": 5,
		"WorkId": 0,
		"GuidValue": "5894fd79-046d-4823-8086-fceffa5c60cb",
		"ProviderName": null
	},
	"Name": "Start",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "StartPage"],
	"ParentLink": {
		"Id": 1,
		"WorkId": 0,
		"GuidValue": "43f936c9-9b23-4ea3-97b2-61c538ad07c9",
		"ProviderName": null
	},
	"RouteSegment": "start",
	"Url": "/app/index.html",
	"Changed": "2017-12-01T21:08:08Z",
	"Created": "2017-12-01T21:08:04Z",
	"StartPublish": "2017-12-01T21:08:08Z",
	"StopPublish": null,
	"Saved": "2017-12-15T17:49:35Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	}
}, {
	"ContentLink": {
		"Id": 1,
		"WorkId": 0,
		"GuidValue": "43f936c9-9b23-4ea3-97b2-61c538ad07c9",
		"ProviderName": null
	},
	"Name": "Root",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "SysRoot"],
	"ParentLink": {
		"Id": 0,
		"WorkId": 0,
		"GuidValue": null,
		"ProviderName": null
	},
	"RouteSegment": "",
	"Url": null,
	"Changed": "1999-01-01T00:00:00Z",
	"Created": "1999-01-01T00:00:00Z",
	"StartPublish": "1999-01-01T00:00:00Z",
	"StopPublish": null,
	"Saved": "1999-01-01T00:00:00Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	}
}]
```

#

### Get Multiple Content Items by Reference or Guid ###

Returns a collection of content based on a set of provided content references or content guids. The provided values are sent in a query string in comma separated lists.

***Request***

```javascript
        axios.get('/api/episerver/content/', {
            params: {
                'references' : '27,30',
                'guids' : 'a16488c9-2e08-4917-82ce-21db74053212,8ea162bc-7705-44d3-ac57-39d4e667d575'
            },
            headers: {
                'Accept': 'application/json',
                'Accept-Language': 'en'
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
[{
	"ContentLink": {
		"Id": 27,
		"WorkId": 0,
		"GuidValue": "b1b94f8c-0813-4d32-99c4-d162c18ba709",
		"ProviderName": null
	},
	"Name": "Greg Rabbit",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "ArtistPage"],
	"ParentLink": {
		"Id": 11,
		"WorkId": 0,
		"GuidValue": "f42d6a01-b3a3-46c4-b20a-605c82ca0152",
		"ProviderName": null
	},
	"RouteSegment": "greg-rabbit",
	"Url": null,
	"Changed": "2017-11-28T20:08:17Z",
	"Created": "2017-11-28T20:06:52Z",
	"StartPublish": "2017-11-28T20:08:17Z",
	"StopPublish": null,
	"Saved": "2017-12-05T23:52:54Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"ArtistName": {
		"Value": "Greg Rabbit",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistPhoto": {
		"Value": "/siteassets/artist-images/greg-rabbit.jpg",
		"PropertyDataType": "PropertyUrl"
	},
	"ArtistDescription": {
		"Value": "<p>Tincidunt condimentum dictum ullamcorper aliquet iaculis hymenaeos dictum volutpat eros nam et torquent lobortis consectetuer. Adipiscing elementum vestibulum a. Facilisis ligula dis dignissim laoreet vehicula nec consequat hendrerit augue habitasse.</p>",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"ArtistGenre": {
		"Value": "Rock",
		"PropertyDataType": "PropertyLongString"
	},
	"PerformanceStartTime": {
		"Value": "2018-04-01T00:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"PerformanceEndTime": {
		"Value": "2018-04-01T01:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"StageName": {
		"Value": "Maple",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistIsHeadliner": {
		"Value": null,
		"PropertyDataType": "PropertyBoolean"
	}
}, {
	"ContentLink": {
		"Id": 30,
		"WorkId": 0,
		"GuidValue": "0654f5c5-d40d-4b34-82d3-2d497d2ef102",
		"ProviderName": null
	},
	"Name": "During Rewind",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "ArtistPage"],
	"ParentLink": {
		"Id": 11,
		"WorkId": 0,
		"GuidValue": "f42d6a01-b3a3-46c4-b20a-605c82ca0152",
		"ProviderName": null
	},
	"RouteSegment": "during-rewind",
	"Url": null,
	"Changed": "2017-11-28T19:54:18Z",
	"Created": "2017-11-28T19:53:54Z",
	"StartPublish": "2017-11-28T19:54:18Z",
	"StopPublish": null,
	"Saved": "2017-12-05T23:51:08Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"ArtistName": {
		"Value": "During Rewind",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistPhoto": {
		"Value": "/siteassets/artist-images/during-rewind.jpg",
		"PropertyDataType": "PropertyUrl"
	},
	"ArtistDescription": {
		"Value": "<p>Tincidunt condimentum dictum ullamcorper aliquet iaculis hymenaeos dictum volutpat eros nam et torquent lobortis consectetuer. Adipiscing elementum vestibulum a. Facilisis ligula dis dignissim laoreet vehicula nec consequat hendrerit augue habitasse.</p>",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"ArtistGenre": {
		"Value": "Rock",
		"PropertyDataType": "PropertyLongString"
	},
	"PerformanceStartTime": {
		"Value": "2018-03-31T23:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"PerformanceEndTime": {
		"Value": "2018-04-01T00:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"StageName": {
		"Value": "Maple",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistIsHeadliner": {
		"Value": null,
		"PropertyDataType": "PropertyBoolean"
	}
}, {
	"ContentLink": {
		"Id": 33,
		"WorkId": 0,
		"GuidValue": "a16488c9-2e08-4917-82ce-21db74053212",
		"ProviderName": null
	},
	"Name": "Cranky Babe",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "ArtistPage"],
	"ParentLink": {
		"Id": 11,
		"WorkId": 0,
		"GuidValue": "f42d6a01-b3a3-46c4-b20a-605c82ca0152",
		"ProviderName": null
	},
	"RouteSegment": "cranky-babe",
	"Url": null,
	"Changed": "2017-11-28T19:43:30Z",
	"Created": "2017-11-28T19:42:41Z",
	"StartPublish": "2017-11-28T19:43:30Z",
	"StopPublish": null,
	"Saved": "2017-12-05T23:49:54Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"ArtistName": {
		"Value": "Cranky Babe",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistPhoto": {
		"Value": "/siteassets/artist-images/cranky-babe.jpg",
		"PropertyDataType": "PropertyUrl"
	},
	"ArtistDescription": {
		"Value": "<p>Tincidunt condimentum dictum ullamcorper aliquet iaculis hymenaeos dictum volutpat eros nam et torquent lobortis consectetuer. Adipiscing elementum vestibulum a. Facilisis ligula dis dignissim laoreet vehicula nec consequat hendrerit augue habitasse.</p>",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"ArtistGenre": {
		"Value": "Jazz",
		"PropertyDataType": "PropertyLongString"
	},
	"PerformanceStartTime": {
		"Value": "2018-03-31T19:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"PerformanceEndTime": {
		"Value": "2018-03-31T20:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"StageName": {
		"Value": "Birch",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistIsHeadliner": {
		"Value": null,
		"PropertyDataType": "PropertyBoolean"
	}
}, {
	"ContentLink": {
		"Id": 35,
		"WorkId": 0,
		"GuidValue": "8ea162bc-7705-44d3-ac57-39d4e667d575",
		"ProviderName": null
	},
	"Name": "Brave Minor",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "ArtistPage"],
	"ParentLink": {
		"Id": 11,
		"WorkId": 0,
		"GuidValue": "f42d6a01-b3a3-46c4-b20a-605c82ca0152",
		"ProviderName": null
	},
	"RouteSegment": "brave-minor",
	"Url": null,
	"Changed": "2017-11-28T19:39:47Z",
	"Created": "2017-11-28T19:39:22Z",
	"StartPublish": "2017-11-28T19:39:47Z",
	"StopPublish": null,
	"Saved": "2017-12-05T23:36:24Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"ArtistName": {
		"Value": "Brave Minor",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistPhoto": {
		"Value": "/siteassets/artist-images/brave-minor.jpg",
		"PropertyDataType": "PropertyUrl"
	},
	"ArtistDescription": {
		"Value": "<p>Tincidunt condimentum dictum ullamcorper aliquet iaculis hymenaeos dictum volutpat eros nam et torquent lobortis consectetuer. Adipiscing elementum vestibulum a. Facilisis ligula dis dignissim laoreet vehicula nec consequat hendrerit augue habitasse.</p>",
		"PropertyDataType": "PropertyXhtmlString"
	},
	"ArtistGenre": {
		"Value": "Folk",
		"PropertyDataType": "PropertyLongString"
	},
	"PerformanceStartTime": {
		"Value": "2018-04-01T23:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"PerformanceEndTime": {
		"Value": "2018-04-02T00:00:00Z",
		"PropertyDataType": "PropertyDate"
	},
	"StageName": {
		"Value": "Birch",
		"PropertyDataType": "PropertyLongString"
	},
	"ArtistIsHeadliner": {
		"Value": null,
		"PropertyDataType": "PropertyBoolean"
	}
}]
```

#

### Get Content By Reference (with Expand) ###

Returns a content model based on the provided complex reference in the format of contentID[_workID[_providerName]]

***Request***

```javascript
    axios.get('/api/episerver/content/7', {
        params: {
            'expand' : '*'
        },
        headers: {
            'Accept': 'application/json',
            'Accept-Language': 'en'
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

{
	"ContentLink": {
		"Id": 7,
		"WorkId": 0,
		"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
		"ProviderName": null
	},
	"Name": "Music Festival App",
	"Language": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ExistingLanguages": [{
		"DisplayName": "English",
		"Name": "en"
	}, {
		"DisplayName": "Swedish",
		"Name": "sv"
	}],
	"MasterLanguage": {
		"DisplayName": "English",
		"Name": "en"
	},
	"ContentType": ["Page", "AppRootPage"],
	"ParentLink": {
		"Id": 5,
		"WorkId": 0,
		"GuidValue": "5894fd79-046d-4823-8086-fceffa5c60cb",
		"ProviderName": null
	},
	"RouteSegment": "music-festival-app",
	"Url": null,
	"Changed": "2017-12-01T19:44:16Z",
	"Created": "2017-12-01T19:16:41Z",
	"StartPublish": "2017-12-01T19:17:03Z",
	"StopPublish": null,
	"Saved": "2017-12-01T19:44:16Z",
	"Status": "Published",
	"Category": {
		"Value": [],
		"PropertyDataType": "PropertyCategory"
	},
	"MenuItems": {
		"ExpandedValue": [{
			"ContentLink": {
				"Id": 9,
				"WorkId": 0,
				"GuidValue": "aecb9c55-d73c-4dd4-a67b-2cfc335180a9",
				"ProviderName": null
			},
			"Name": "Info",
			"Language": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ExistingLanguages": [{
				"DisplayName": "English",
				"Name": "en"
			}],
			"MasterLanguage": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ContentType": ["Page", "InformationPage"],
			"ParentLink": {
				"Id": 7,
				"WorkId": 0,
				"GuidValue": "8377a392-8614-4cdd-8054-fd044dab1668",
				"ProviderName": null
			},
			"RouteSegment": "info",
			"Url": null,
			"Changed": "2017-11-28T21:26:39Z",
			"Created": "2017-11-28T21:17:55Z",
			"StartPublish": "2017-11-28T21:18:30Z",
			"StopPublish": null,
			"Saved": "2017-12-11T16:57:29Z",
			"Status": "Published",
			"Category": {
				"Value": [],
				"PropertyDataType": "PropertyCategory"
			},
			"Title": {
				"Value": "Info",
				"PropertyDataType": "PropertyLongString"
			},
			"HeroContentArea": {
				"Value": [{
					"ContentLink": {
						"Id": 47,
						"WorkId": 0,
						"GuidValue": "e9e2c61f-b6d6-4bbe-9272-c94ee5eb5b01",
						"ProviderName": null
					},
					"DisplayOption": "",
					"Tag": null
				}],
				"PropertyDataType": "PropertyContentArea"
			},
			"Preamble": {
				"Value": "",
				"PropertyDataType": "PropertyLongString"
			},
			"MainBody": {
				"Value": "<h2>Accommodations</h2>\n<div>Check out our MusFes Hotel Packages! If you’re coming in from out of town, purchase a package with your wristband and book lodging near the festival. You’ll also receive exclusive Music Festival Merchandise!</div>\n<h2>Transportation</h2>\n<div>RideWithMe is the official rideshare partner of Lolla, available to help transport you to and from the festival. Carpool together with friends and save some money!</div>\n\n<h2>Locker &amp; Storage</h2>\n<div>Want a safe and secure place to store your things while you attend Music Festival? Personal lockers will be available so you can free up your hands and avoid losing any personal items during the fest.</div>\n\n<h2>First Aid</h2>\n<div>We make every effort to create a safe environment on the festival grounds, including public and private security and medical staff throughout Woods Park. If you need any assistance, seek out the medical tent, or look for a peace officer or festival staff member.</div>\n\n<div><a href=\"http://www.google.com\">Contact Us For More Information</a></div>",
				"PropertyDataType": "PropertyXhtmlString"
			},
			"MainContant": {
				"Value": null,
				"PropertyDataType": "PropertyContentArea"
			}
		}],
		"Value": [{
			"ContentLink": {
				"Id": 9,
				"WorkId": 0,
				"GuidValue": "aecb9c55-d73c-4dd4-a67b-2cfc335180a9",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	},
	"FestivalDayDetailBlocks": {
		"ExpandedValue": [{
			"ContentLink": {
				"Id": 45,
				"WorkId": 0,
				"GuidValue": "2c171aad-e1ef-4d4a-b61a-3884069047c3",
				"ProviderName": null
			},
			"Name": "Sunday",
			"Language": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ExistingLanguages": [{
				"DisplayName": "English",
				"Name": "en"
			}],
			"MasterLanguage": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ContentType": ["Block", "FestivalDayTimeRangeBlock"],
			"ParentLink": {
				"Id": 44,
				"WorkId": 0,
				"GuidValue": "3ba8456a-2ed5-4855-8ce0-40cbf8d0309d",
				"ProviderName": null
			},
			"RouteSegment": null,
			"Url": null,
			"Changed": "2017-12-01T19:43:37Z",
			"Created": "2017-12-01T19:43:37Z",
			"StartPublish": "2017-12-01T19:43:37Z",
			"StopPublish": null,
			"Saved": "2017-12-01T19:43:37Z",
			"Status": "Published",
			"Category": {
				"Value": [],
				"PropertyDataType": "PropertyCategory"
			},
			"DayOfWeek": {
				"Value": "Sunday",
				"PropertyDataType": "PropertyLongString"
			},
			"PerformancesStartTime": {
				"Value": "2017-12-31T15:00:00Z",
				"PropertyDataType": "PropertyDate"
			},
			"PerformancesEndTime": {
				"Value": "2018-01-01T03:00:00Z",
				"PropertyDataType": "PropertyDate"
			}
		}, {
			"ContentLink": {
				"Id": 46,
				"WorkId": 0,
				"GuidValue": "f424ec20-7abe-49ed-b234-0b1973b0672d",
				"ProviderName": null
			},
			"Name": "Saturday",
			"Language": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ExistingLanguages": [{
				"DisplayName": "English",
				"Name": "en"
			}],
			"MasterLanguage": {
				"DisplayName": "English",
				"Name": "en"
			},
			"ContentType": ["Block", "FestivalDayTimeRangeBlock"],
			"ParentLink": {
				"Id": 44,
				"WorkId": 0,
				"GuidValue": "3ba8456a-2ed5-4855-8ce0-40cbf8d0309d",
				"ProviderName": null
			},
			"RouteSegment": null,
			"Url": null,
			"Changed": "2017-12-01T19:44:13Z",
			"Created": "2017-12-01T19:44:13Z",
			"StartPublish": "2017-12-01T19:44:13Z",
			"StopPublish": null,
			"Saved": "2017-12-06T19:59:58Z",
			"Status": "Published",
			"Category": {
				"Value": [],
				"PropertyDataType": "PropertyCategory"
			},
			"DayOfWeek": {
				"Value": "Saturday",
				"PropertyDataType": "PropertyLongString"
			},
			"PerformancesStartTime": {
				"Value": "2017-12-30T15:00:00Z",
				"PropertyDataType": "PropertyDate"
			},
			"PerformancesEndTime": {
				"Value": "2017-12-31T03:00:00Z",
				"PropertyDataType": "PropertyDate"
			}
		}],
		"Value": [{
			"ContentLink": {
				"Id": 45,
				"WorkId": 0,
				"GuidValue": "2c171aad-e1ef-4d4a-b61a-3884069047c3",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}, {
			"ContentLink": {
				"Id": 46,
				"WorkId": 0,
				"GuidValue": "f424ec20-7abe-49ed-b234-0b1973b0672d",
				"ProviderName": null
			},
			"DisplayOption": "",
			"Tag": null
		}],
		"PropertyDataType": "PropertyContentArea"
	}
}
```