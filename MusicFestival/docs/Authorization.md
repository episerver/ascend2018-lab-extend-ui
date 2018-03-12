# Authorization & Access Control

All Content Api operations are built in such a way that they only return content that the current user is authorized to access. In order to establish the user making the request, these APIs support both standard cookie-based authentication (via Forms or ASP.NET Identity) when the request is made on the same domain, or alternatively, any number of OWIN-based ASP.NET Identity configurations, which may leverage authentication middleware for OAuth, or SAML-based authentication. In addition to this, anonymous users can also access the API without credentials, and will be able to query content which is accessible to the `Everyone` role within Episerver, as long as it is not filtered out based on Multi-Site or Required Role Filtering (see [Installation](Installation.md) for more information on these concepts).

In order to provide an easy to install configuration to leverage Episerver user authentication in external systems, a bolt-on authentication mechanism has been created which leverages OAuth in a way similar to the implementation within the EPiServer Service Api. 

The authentication scheme used in `EPiServer.ContentApi.Authorization` is an implementation of OAuth 2.0's "Resource Owner Password Credentials Grant". This grant assumes that client applications can be implicitly trusted with users entering their usernames and passwords in a secure environment within an external application, exchanging them for a short-lived, non-revokable Access Token. This Access Token can be then sent along with each request to the Content Api, and result in an authenticated context being established for the duration of that call. 

This context may result in different data being returned in the API in the form of:

* Episerver Content which may be limited to certain roles
* Personalized content based on Visitor Group Criteria

A revokable Refresh Token will also be issued alongside each Access Token, allowing client applications to maintain user access without users needing to re-enter their passwords each time the Access Token expires. In order to maintain access, the Refresh Token must be exchanged for an Access Token with Episerver.


## Installation

In order to get started, the following two packages must be installed in the solution:

```
EPiServer.ContentApi.Authorization
EPiServer.ContentApi.Authorization.UI
```

Once installed, the OAuth implementation must be initialized after the standard EPiServer configuration within the OWIN Startup class, using the `UseContentApiIdentityOAuthAuthorization` extension method as follows:

```
        public void Configuration(IAppBuilder app)
        {
            // Add CMS integration for ASP.NET Identity
            app.AddCmsAspNetIdentity<ApplicationUser>();   

            // Use cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(login-path),
                Provider = new CookieAuthenticationProvider
                {
                        OnValidateIdentity = 
                            SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<ApplicationUser>,ApplicationUser>(
                                validateInterval: TimeSpan.FromMinutes(30),
                                regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
                }
            });

            ...

            app.UseContentApiIdentityOAuthAuthorization<ApplicationUserManager<ApplicationUser>, ApplicationUser>();
        }

```

if your solution is using a custom user model, be sure to update the call to UseContentApiIdentityOAuthAuthorization to utilize you user class, and any custom implementation of ApplicationUserManager which has been implemented.

The OAuth implemenation can be configured from here by passing in an instance of `ContentApiOAuthOptions`, allowing the following elements of the implementation to be configured:

### Require SSL ###

By default, the endpoint for exchanging credentials for access and refresh tokens requires requests be made securely over an https connection. It is highly recommended to maintain this in public-facing environments due to the nature of the Resource Owner Password Credentials Grant flow, but setting the `RequireSsl` property to `false` will enable this endpoint to be accessed over http for development purposes.

### Token Endpoint ###

By default, the endpoint for tokens is located at `/api/episerver/auth/token`. This can be customized by changing the `TokenEndpointPath` parameter to another valid relative url, taking care not to collide with other routing in your existing solution.

### Access Token & Refresh Token Expiration Timespan ###

By default, Access Tokens are issued for 20 minutes, and Refresh Tokens are valid for 14 days to be exchanged, unless otherwise revoked.

In order to adjust these timespans, configure the `AccessTokenExpireTimeSpan` and `RefreshTokenExpireTimeSpan` to your desired values. Keep in mind that Access Tokens should generally be short-lived, because they are not revokable

## Configuring Client Applications ##

By default, the Content Api is configured via `ContentApiOptions` to include a single Client with ClientId: `Default` and a configured `*` wildcard `AccessControlAllowOrigin` setting. As such, when users authenticate with the Authorization endpoint, requests can be made from any origin within the browser. In addition, subsequent requests to resources in the Content Api which include an access token in the `Authorization` header can occur from any origin.

In cases where the Content Api will be used from multiple client applications, multiple clients should be configured within `ContentApiOptions`. If these clients are restricted to specific origins, then Authorization requests for each client will only succeed on their configured origin for security reasons.

In certain authentication configurations, the XHR `withCredentials` flag may need to be used in order to pass cookies, HTTP Basic Credentials, or TLS Client certificates in cross-domain requests. Requests where the `withCredentials` parameter is set to `true` will not succeed with the default Client configuration since the CORS specification dictates that credentials cannot be sent with `Access-Control-Allow-Origin` set to `*`. In this case, the Content Api Client must be configured with a specific origin in order to allow credentialed cross-domain requests to succeed.


## Authenticating with the API ##

All example request use [Axios](https://github.com/axios/axios) for generating requests in Javascript.

Add Axios (and a polyfill for UrlSearchParams for older browsers) via CDN for easy testing:

```html
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/url-search-params/0.10.0/url-search-params.js"></script>
```

In order to acquire an Access Token, make a call to the token endpoint with `grant_type=password`, passing in the entered username and password, as well as the Client ID configured in `ContentApiOptions`. If using the default options, the `client_id=Default` can be used:

```javascript
        var params = new URLSearchParams();
        params.append('grant_type', 'password');
        params.append('username', 'admin');
        params.append('password', 'Mw123!');
        params.append('client_id', 'Default');

        axios.post('/api/episerver/auth/token', params, {
                    headers: {
                        'Accept': 'application/json',
                    }
                })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
  
```

If the provided user credentials are valid, along with the client ID, an Access Token will be issued, alongside a Refresh Token in the response:

```json
{
    "access_token": "oJRvAIml_KU_LWxSpRsQe6GKps8T5gdWXVsu_kuPpir8RzaDS1SBPqBVtHojKvYIZX4JjxwkDrUGuxN5eLhrxlN3dO7NZx_xbjUyWJ2gaxLRMpuQSdOo97vLQNGmfWEzBnvB6H91Nott7X7wQVF9CJkbETPzkSQPbyQaLhm-MoBtwkP0sDy6SP5KRy6WzXfj-tFX2kKGFM3EBoegGP4Hxg0vsToA5iEH4YjlhP82lb2Dv0yoUDfKRc3nxwbaHgeV_ypWhaMa2Da1PgyEnN783LkKJ5hkTJHWDYCI1QP282ec0WwiBfQwWYEFkGBlslyq3aw4sN5qPbT7yTa2cBGubC8Qndl3NEHLxcDZnniT2-Ak4IcqxcO9-7HT47LcMaghj0WRzqf28JjDNDzzhjKim5pNfZblKbllO-aIhSjG2cv0FtS9Lz7YEgD-rg1UaYFxVf98Ojz34RFVAOGc0kQ2Th7CNDD4gOUOn4QiWsrBtRNe47_QYmzlkxgXdxD-VNli9e766O94LI1wV3-K70FZHjPp5iTQYyWp8ub2OS6HhsM",
    "token_type": "bearer",
    "expires_in": 1199,
    "refresh_token": "55109f18685d4847aa0ab62e9f95fba5",
    "client_id": "Default",
    "username": "myuser",
    ".issued": "Wed, 20 Dec 2017 23:28:53 GMT",
    ".expires": "Wed, 20 Dec 2017 23:48:53 GMT"
}
```

The `access_token` property contains the full access token which can be used in subsequent calls to the Content Api. The `expires_in` indicates the number of seconds the Access Token will remain valid, after which point the Refresh Token (from the `refresh_token` parameter) must be used to obtain a new Access Token.

Once an Access Token is acquired, it can be attached to any Content Api request in the `Authorization` header, denoted as a bearer token: `Authorization: Bearer <access-token-goes-here>`. See the following sample request:

```javascript
    axios.get('/api/episerver/content/7', {
            headers: {
                'Accept': 'application/json',
                'Accept-Language': 'en',
                'Authorization': 'Bearer JRvAIml_KU_LWxSpRsQe6GKps8T5gdWXVsu_kuPpir8RzaDS1SBPqBVtHojKvYIZX4JjxwkDrUGuxN5eLhrxlN3dO7NZx_xbjUyWJ2gaxLRMpuQSdOo97vLQNGmfWEzBnvB6H91Nott7X7wQVF9CJkbETPzkSQPbyQaLhm-MoBtwkP0sDy6SP5KRy6WzXfj-tFX2kKGFM3EBoegGP4Hxg0vsToA5iEH4YjlhP82lb2Dv0yoUDfKRc3nxwbaHgeV_ypWhaMa2Da1PgyEnN783LkKJ5hkTJHWDYCI1QP282ec0WwiBfQwWYEFkGBlslyq3aw4sN5qPbT7yTa2cBGubC8Qndl3NEHLxcDZnniT2-Ak4IcqxcO9-7HT47LcMaghj0WRzqf28JjDNDzzhjKim5pNfZblKbllO-aIhSjG2cv0FtS9Lz7YEgD-rg1UaYFxVf98Ojz34RFVAOGc0kQ2Th7CNDD4gOUOn4QiWsrBtRNe47_QYmzlkxgXdxD-VNli9e766O94LI1wV3-K70FZHjPp5iTQYyWp8ub2OS6HhsM'
            }
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
```

As your Access Token nears expiration, use your issued Refresh Token to request a new Access Token, by making a call to the token endpoint with `grant_type=refresh_token`, passing in the Refresh Token in the `refresh_token` parameter, as well as the Client ID from the client application. If using the default options, `client_id=Default` can be used:

```javascript
        var params = new URLSearchParams();
        params.append('grant_type', 'refresh_token');
        params.append('refresh_token', '55109f18685d4847aa0ab62e9f95fba5');
        params.append('client_id', 'Default');

        axios.post('/api/episerver/auth/token', params, {
                    headers: {
                        'Accept': 'application/json',
                    }
                })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
  
```

In response, assuming the Refresh Token is still valid, a new Access Token will be issued, allowing the authentication to be maintained:

```json

{
    "access_token": "7GXJ8z4ykAKi-MbnkKTAwQc_hc64PIA7rDFDj-B9Yy5r9NKJVVl9w-g3EJv6mRpobwUV4BZH779538u7LdDDZj5vxgVkiDHP1690P14AtjpvT-wJ3_3voDVX-IGz4Ec_0TuClIneS4eVP650QO8jlaSGUTVgEa17Pb7fh1RoaQl_kQBoZSziiQ_-SqMvZqwf13wnfQDBb3UDVRVpGY4gLcjX7PS8qyL3ciq75NicIPznSIxM50oj8NPjV-KKA009W4KTJLFprSHKK1oWVYDv7uLZhLt-OydTQQvOthu2rUVCmPQyUkAcwNnAvwzokZjxyvLe1wHWUOZ_HUyzuE5nLg7BUBHhQdnwhPa9iQ5u-b94Ju1DSVml7gWM20r2U5115BZqlSJ3hjwwxqWyiXb-HowUIedbFl7uXkF8DaTj0HHHjwxhQ4fFZ6r9b6VNqfCiT3QxECq59peImaEeQwAAhSr6QYqN3T-Z2uL0sY4MpUKSI8uaL0SdEuhr3zou3xCJtXQfuu-bZc5oARemWqvaeayvvRHdqLNWi0qQM5ueiRE",
    "token_type": "bearer",
    "expires_in": 1199,
    "refresh_token": "9e490c3f07554a4c81b3b5a85a554be2",
    "client_id": "Default",
    "username": "myuser",
    ".issued": "Wed, 20 Dec 2017 23:40:48 GMT",
    ".expires": "Thu, 21 Dec 2017 00:00:48 GMT"
}

```

## Managing Refresh Tokens ##

Since Access Tokens cannot be revoked, Refresh Tokens are key to enabling users to revoke their own access in client applications, or for administrators to revoke users access in client applications.

The `EPiServer.ContentApi.Authorization.UI` package provides a simple user interface to manage issued refresh tokens for a user. This interface is accessible under the "Content Api Settings" tab when a user views their 'My Settings' menu item in EPiServer CMS Edit Mode, as well as from the "Edit User" page in the EPiServer Admin Section when viewing a specific user:

![Refresh Token Administration](images/RefreshTokenUI.jpg)

This user interface will display all Refresh Tokens issued to various configured Client applications within Episerver. Each user will only have one valid Refresh Token for each client (if issued). By clicking 'Revoke', a given Refresh Token can be removed, forcing re-authentication for a given user in that Client Application.


## Managing Access & Refresh Tokens in Client Applications ##

When utilizing Access Tokens and Refresh Tokens in Client Applications, it's important to securely store these secrets in such a way to protect them against Cross-Site Request Forgery (XSRF / CSRF) and Cross-Site Scripting (XSS) attacks. We recommend that you:

* Utilize Local Storage or Session Storage for storing tokens in the Browser
* Ensure all requests are sent via HTTPS
* Leverage short-lived Access Tokens