# UserService API

### Description

This `.NET 7.0` library provides easy access to the UserService. It is a wrapper around the UserService `REST API`.

# Usage

## Program.cs

Add the following lines to your Program.cs

```csharp
//This implements the Services
builder.Services.AddUserServiceApi(API_URL, YOUR_API_KEY);

//This implements the Authorization
builder.Services.AddUserServiceAuthorization();
```

## App.razor

Your file should look similar to that

```html
<CascadingAuthenticationState>
    <Router AppAssembly="@typeof(App).Assembly">
        <Found Context="routeData">
            <PageTitle>Title</PageTitle>
            <AuthorizeRouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)">
                <NotAuthorized>
                     not authorized
                </NotAuthorized>
                <Authorizing>
                    authorizing
                </Authorizing>
            </AuthorizeRouteView>
            <FocusOnNavigate RouteData="@routeData" Selector="h1"/>
        </Found>
        <NotFound>
            <PageTitle>Not found</PageTitle>
            <LayoutView Layout="@typeof(MainLayout)">
                <p role="alert">Sorry, there's nothing at this address.</p>
            </LayoutView>
        </NotFound>
    </Router>
</CascadingAuthenticationState>
```

## Usage in a Blazor page

```csharp
@page "/test"

@attribute [Authorize(Roles = "Administrator")]
    
//Your content here

@code {
    //your code here
}
```

In this example, the page is only accessible for users with the role "Administrator".
You can use several roles by separating them with a comma.

## Current Roles

- Administrator
- Assistant
- Patient
- Taxconsultant

#### The previously known `Userservice` is now named `IdentityProvider`.

## Retrieve the id of the current user

```csharp
@page "/user"

@inject IdentityProvider IdentityProvider
    
@attribute [Authorize]

    
//your content here

@code {
    protected override async Task OnInitializedAsync() {
        var id = IdentityProvider.CurrentUser!.Id; //the nullable warning can be ignored because you need to be authorized to view this page.
    }
}
```