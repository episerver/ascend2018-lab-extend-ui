# Content Search Api

The Content Search Api exposes the ability to query for content in Episerver Find, providing robust filtering and sorting via OData syntax, with support for personalization, proper access control, and a feature for loading referenced content in the same request.

## Localization ##

Content can be retrieved in one or many languages within the same request to the Content Search Api endpoint. In order to indicate the desired languages of content to return, attach the `Accept-Language` header to your request. If you wish to receive content in all languages, pass an empty `Accept-Language` header or pass a wildcard "*" value.

```
Accept-Language: en
Accept-Language: en, sv
Accept-Language: en-US,fr-CA
Accept-Language: *
```

## Access Control ##

The Content Search Api only returns published content that the current user has access to view.

Users can authenticate via Cookie Authentication or Content API Authorization using OAuth, and either method can be used to call the API from an authenticated context.


## Personalization ##

The Content Search Api leverages indexed data within Episerver Find to deliver search results quickly and efficiently. By default, content is not personalized based on Visitor Group criteria since it is loaded directly from Episerver Find. In order to retrieve personalized property values (based on the current user and context), pass the `personalize` query string parameter as `true`:

```
?personalize=true
```

## Free Text Search ##

The Content Search Api endpoint allows callers to pass in a free text query, which is evaluated by Episerver Find.

```
?query=apples
```

## Filtering ##

Search results can be filtered using the OData v4 filter syntax, providing a capability to quickly write queries to filter down to specific subsets of content.

OData filters can be passed in the `filter` query string, with support for the following operators and functions:

   Logical Operators: `eq, ne, gte, gt, lte, lt, and, or`
   
   String Functions: `tolower, contains`

   Collection Functions: `any` (Limited support - see Filter by Content Type below)

For more information on OData v4 syntax, see [URL Conventions for OData v4](http://docs.oasis-open.org/odata/odata/v4.0/odata-v4.0-part2-url-conventions.html).

**Filter by Content Type**

Content can be filtered by a specific content type name, or by the following set of base types: Page, Block, Media, Image, Video

```
?filter=ContentType/any(t:t eq 'Page')
?filter=ContentType/any(t:t eq 'ArticlePage')
?filter=any(t:t eq 'ArticlePage') or any(t:t eq 'CategoryPage')
?filter=ContentType/any(t:t ne 'Block')
```

Note: `any` function support is limited to matching single property expressions on built-in simple Collection properties, namely ContentType. To filter in other collections on all properties, such as by Category or by your own Content Area properties, use `eq` and `ne` filters (See "Filter by Category", "Filter by Content Reference", and "Filter by Content Area Value" below).


**Filter by Category**
```
?filter=Category/Value/Id eq 2
?filter=Category/Value/Name eq 'Category Name'
```

**Filter by Content Reference**
```
?filter=ContentLink/Id eq 6
?filter=ContentLink/Id eq null
?filter=ContentLink/Id ne 6
```

**Filter by Content Area Value**
```
?filter=ContentAreaProperty/Value/ContentLink/Id eq 17
?filter=ContentAreaProperty/Value/ContentLink/Id ne 4
```

**Filter by Date Range**
```
?filter=StartPublish gte 2016-01-01T00:00:00Z and StartPublish lte 2017-01-01T00:00:00Z
?filter=StartPublish gte 2016-01-01T00:00:00Z and StartPublish lte 2017-01-01T00:00:00Z
```

**Filtering with String Matching**
```
?filter=tolower(Name) eq 'start'
?filter=contains(MainBody/Value, 'Start')
?filter=contains(tolower(MainBody/Value), 'bees')
```

## Ordering ##

Search results can be ordered using the OData v4 orderby syntax, with support for multiple sorting criteria.

Orderby expressions are passed in the `orderby` query string, with support for indicating sort direction with `asc` and `desc` descriptors. If a sort direction is not included when passing an orderby string, the direction is defaulted to Ascending.

If the `orderby` string is excluded or empty, search results are ordered by relevance.

For more information on OData v4 syntax, see [URL Conventions for OData v4](http://docs.oasis-open.org/odata/odata/v4.0/odata-v4.0-part2-url-conventions.html

**Order by Name**
```
?orderby=Name
```

**Order by Recently Changed**
```
?orderby=Changed desc
```

**Order by multiple properties**
```
?orderby=Name asc, Changed desc
```

## Top / Skip ##

Use the `top` and `skip` parameters to paginate through search results returned from the Content API search endpoint. The `TotalMatching` property of the search response can be used to calculate the total number of pages.

```
?top=10&skip=10
```


## Expand (Returning Referenced Content) ##

Use the `expand` parameter to return data from reference properties in the same request.

```
?expand=MyContentReference
?expand=MyContentReference,MyContentArea
?expand=*
```

The following property types can be expanded: Content Area, Content Reference, Content Reference List, Link Collection

When reference properties are expanded, the full object for the referenced IContent instances will be returned in the response on the "ExpandedValue" property of that property's object.

Expanded properties are fully compatible with the `personalize` parameter, meaning that only anonymous content is returned when a given property is expanded and `personalize` is set to `false`.

Properties on objects that are returned in the ExpandedValue cannot be expanded further in the same request.

## Example Request / Response ##

All example request use [Axios](https://github.com/axios/axios) for generating requests in Javascript.

Add Axios (and a polyfill for UrlSearchParams for older browsers) via CDN for easy testing:

```html
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/url-search-params/0.10.0/url-search-params.js"></script>
```
***Request***

```javascript
    axios.get('/api/episerver/search/content/', {
            params: {
                'query' : 'farmer',
                'skip' : 0,
                'top' : 2,
                'expand' : '*',
                'filter' : 'ContentType/any(t:t eq \'ArtistPage\')',
                'orderby' : 'Changed desc',
                'personalize' : 'true'
            },
            headers: {
                'Accept': 'application/json',
                'Accept-Language': 'en,sv'
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
	"TotalMatching": 2,
	"Results": [{
		"ContentLink": {
			"Id": 24,
			"WorkId": 0,
			"GuidValue": "637608d4-ac47-4087-bdc1-929b6cffa9eb",
			"ProviderName": null
		},
		"Name": "Mister Farmer",
		"Language": {
			"DisplayName": "Swedish",
			"Name": "sv"
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
		"RouteSegment": "mister-farmer",
		"Url": null,
		"Changed": "2017-11-28T21:38:40Z",
		"Created": "2017-11-28T21:37:15Z",
		"StartPublish": "2017-11-28T21:38:40Z",
		"StopPublish": null,
		"Saved": "2017-11-28T21:38:40Z",
		"Status": "Published",
		"Category": {
			"Value": [],
			"PropertyDataType": "PropertyCategory"
		},
		"ArtistName": {
			"Value": "Mister Farmer",
			"PropertyDataType": "PropertyLongString"
		},
		"ArtistPhoto": {
			"Value": "/siteassets/artist-images/mister-farmer.jpg",
			"PropertyDataType": "PropertyUrl"
		},
		"ArtistDescription": {
			"Value": "",
			"PropertyDataType": "PropertyXhtmlString"
		},
		"ArtistGenre": {
			"Value": "Rock",
			"PropertyDataType": "PropertyLongString"
		},
		"PerformanceStartTime": {
			"Value": "2018-04-02T01:00:00Z",
			"PropertyDataType": "PropertyDate"
		},
		"PerformanceEndTime": {
			"Value": "2018-04-02T02:00:00Z",
			"PropertyDataType": "PropertyDate"
		},
		"StageName": {
			"Value": "Maple",
			"PropertyDataType": "PropertyLongString"
		},
		"ArtistIsHeadliner": {
			"Value": true,
			"PropertyDataType": "PropertyBoolean"
		}
	}, {
		"ContentLink": {
			"Id": 24,
			"WorkId": 0,
			"GuidValue": "637608d4-ac47-4087-bdc1-929b6cffa9eb",
			"ProviderName": null
		},
		"Name": "Mister Farmer",
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
		"RouteSegment": "mister-farmer",
		"Url": null,
		"Changed": "2017-11-28T20:20:10Z",
		"Created": "2017-11-28T20:19:41Z",
		"StartPublish": "2017-11-28T20:20:10Z",
		"StopPublish": null,
		"Saved": "2017-12-05T23:54:19Z",
		"Status": "Published",
		"Category": {
			"Value": [],
			"PropertyDataType": "PropertyCategory"
		},
		"ArtistName": {
			"Value": "Mister Farmer",
			"PropertyDataType": "PropertyLongString"
		},
		"ArtistPhoto": {
			"Value": "/siteassets/artist-images/mister-farmer.jpg",
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
			"Value": "2018-04-02T01:00:00Z",
			"PropertyDataType": "PropertyDate"
		},
		"PerformanceEndTime": {
			"Value": "2018-04-02T02:00:00Z",
			"PropertyDataType": "PropertyDate"
		},
		"StageName": {
			"Value": "Maple",
			"PropertyDataType": "PropertyLongString"
		},
		"ArtistIsHeadliner": {
			"Value": true,
			"PropertyDataType": "PropertyBoolean"
		}
	}]
}

```