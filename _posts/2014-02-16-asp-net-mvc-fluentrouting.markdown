---
layout: post
title:  "ASP.NET MVC Fluent Routing"
date:   2014-02-16
categories: asp net aspnet mvc fluent routing
description: "ASP.NET MVC Fluent Routing is a thin wrapper around the ASP.NET MVC attribute routing engine. With Fluent Routing you can define your routes using a fluent interface, but with the full power of the attribute routing engine (inline route constraints, optional URI parameters and default values)."
---

ASP.NET MVC Fluent Routing is a thin wrapper around the ASP.NET MVC attribute routing engine. With Fluent Routing you can define your routes using a fluent interface, but with the full power of the attribute routing engine (inline route constraints, optional URI parameters and default values), for example:

```csharp
routes.For<HomeController>()
    .CreateRoute("").WithName("my route name").To(controller => controller.Index())
        .WithConstraints().HttpMethod(HttpMethod.Get);
```

Note: This is an open source project. The source code can be found on the [ASP.NET MVC Fluent Routing GitHub repository](https://github.com/dotarj/FluentRouting/), which also contains a demo project.

##Getting started

Getting started with ASP.NET MVC Fluent Routing is easy! Just follow the steps below and soon you will fluently define your ASP.NET MVC routes.

###Install the NuGet package

Open the Visual Studio Package Manager Console and run the following command:

```
Install-Package FluentRouting.Mvc
```

###Add the FluentRouting.Mvc namespace

Open the file where your routes will be registered (eg. RouteConfig.cs, Global.asax.cs, or ...) and add the following namespace:

```csharp
using FluentRouting.Mvc
```

###Define your routes

You are now ready to define your routes using Fluent Routing. Fluent Routing provides an extension method for RouteCollection called `For`, which can be used to map the methods of a controller:

```csharp
public static void RegisterRoutes(RouteCollection routes)
{
    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

    routes.For<HomeController>()
        .CreateRoute("").To(controller => controller.Index());

    routes.For<ContactController>()
        .CreateRoute("contact").To(controller => controller.Index())
        .CreateRoute("contact").To(controller => controller.Post(null))
            .WithConstraints().HttpMethod(HttpMethod.Post)
        .WithGroupConstraints().HttpMethod(HttpMethod.Get);
}
```

##Basic usage

Mapping routes using Fluent Routing starts with the `For` method, which is an extension method for `RouteCollection`. By using the generic type parameter `TController` you can specify the controller to map routes for, eg: `For<ContactController>()`. The `For` method returns a route group builder which can by used to map the actual routes using the `CreateRoute` method. The `CreateRoute` method takes a single argument of type `string` which can be used to specify the route template, eg: `CreateRoute("contactme")`.

The `CreateRoute` method returns a route template builder which can by used to map the route template to a method (defined in the specified `TController` controller) using the `To` method. The `To` method takes a single argument of type `Expression<Action<TController>>` which can be used to specify the route method, eg: `To(controller => controller.Index())`.

As a result, the method `Index` defined in the `ContactController` will be available through the route `contactme`:

```csharp
routes.For<ContactController>()
    .CreateRoute("contactme").To(controller => controller.Index());
```

##Named routes

To specify a name for a route, use the `WithName` method. The `WithName` method takes a single argument of type `string` which can be used to specify the route name, eg: `WithName("contactroute")`. For example:

```csharp
routes.For<ContactController>()
    .CreateRoute("contactme").WithName("contactroute").To(controller => controller.Index());
```

##Route constraints

You can add route constraints to a route and to all routes in a group using the fluent interface, and you can add inline route constraints using the route template.

###Route constraints for a single route

To specify constrains for a route, use the `WithConstraints` method. The `WithConstraints` method returns a route constraint builder which can be used to specify the constraints. For example:

```csharp
routes.For<ContactController>()
    .CreateRoute("contact").To(controller => controller.Post(null))
        .WithConstraints().HttpMethods(HttpMethod.Post);
```

By adding the `HttpMethod.Post` constraint to the route, it will only be accessible by requests using the POST HTTP method.

###Route constraints for multiple routes

To specify constrains for multiple routes, use the `WithGroupConstraints` method. The `WithGroupConstraints` method returns a route group constraint builder which can be used to specify the constraints. For example:

```csharp
routes.For<ContactController>()
    .CreateRoute("contact").To(controller => controller.Index())
    .CreateRoute("contact").To(controller => controller.Post(null))
        .WithConstraints().HttpMethods(HttpMethod.Post)
    .WithGroupConstraints().HttpMethod(HttpMethod.Get));
```

Note: A group constraint is only applied if the route does not already contain a constrain of the same type. In the above example the `HttpMethod.Get` constraint is applied for the first two routes and not for the last route, since it already has the `HttpMethod.Post` constraint added.

###Available route constraints

The following table lists the constraints that are supported.

| Constraint  | Description                                               | Example                                        |
|-------------|-----------------------------------------------------------|------------------------------------------------|
| HttpMethod  | Matches the HTTP method to the specified HTTP method      | `HttpMethods(HttpMethod.Get)`                  |
| HttpMethods | Matches the HTTP method to the specified HTTP methods     | `HttpMethods(HttpMethod.Get, HttpMethod.Post)` |
| Host        | Matches the HTTP host header value to the specified host  | `Host("localhost")`                            |
| Hosts       | Matches the HTTP host header value to the specified hosts | `Hosts("localhost", "remotehost")`             |
| Custom      | Allows you to specify a custom `IRouteConstraint`         | `Custom(new CustomRouteConstraint())`          |

##Inline route constraints

Just like ASP.NET MVC Attribute Routing, ASP.NET MVC Fluent Routing allows you to using inline route constraints. Inline route constraints let you restrict how the parameters in the route template are matched. The general syntax is `{parameter:constraint}`. For example:

```csharp
routes.For<HomeController>()
    .CreateRoute("{id:range(1:1000)").To(controller => controller.Detail(0));
```

For this route the parameter id will only be matched if it&#39;s greater than or equal to 1 and less than or equal to 1000.

###Available inline route constraints

The following table lists the constraints that are supported.

| Constraint | Description                                                                        | Example                          |
|------------|------------------------------------------------------------------------------------|----------------------------------|
| alpha      | Matches uppercase or lowercase Latin alphabet characters (a-z, A-Z)                | `{x:alpha}`                      |
| bool       | Matches a Boolean value.                                                           | `{x:bool}`                       |
| datetime   | Matches a DateTime value.                                                          | `{x:datetime}`                   |
| decimal    | Matches a decimal value.                                                           | `{x:decimal}`                    |
| double     | Matches a 64-bit floating-point value.                                             | `{x:double}`                     |
| float      | Matches a 32-bit floating-point value.                                             | `{x:float}`                      |
| guid       | Matches a GUID value.                                                              | `{x:guid}`                       |
| int        | Matches a 32-bit integer value.                                                    | `{x:int}`                        |
| length     | Matches a string with the specified length or within a specified range of lengths. | `{x:length(6)} {x:length(1,20)}` |
| long       | Matches a 64-bit integer value.                                                    | `{x:long}`                       |
| max        | Matches an integer with a maximum value.                                           | `{x:max(10)}`                    |
| maxlength  | Matches a string with a maximum length.                                            | `{x:maxlength(10)}`              |
| min        | Matches an integer with a minimum value.                                           | `{x:min(10)}`                    |
| minlength  | Matches a string with a minimum length.                                            | `{x:minlength(10)}`              |
| range      | Matches an integer within a range of values.                                       | `{x:range(10,50)}`               |
| regex      | Matches a regular expression.                                                      | `{x:regex(^\d{3}-\d{3}-\d{4}$)}` |

####Inline route constraint resolver

To specify a custom inline constraint resolver for a route template, use the `WithInlineConstraintResolver` method. The `WithInlineConstraintResolver` method takes a single argument of type `IInlineConstraintResolver` which can be used to specify the inline constraint resolver, eg: `WithInlineConstraintResolver(new CustomInlineConstraintResolver())`. For example:

```csharp
routes.For<ContactController>()
    .CreateRoute("contactme").WithInlineConstraintResolver(new CustomInlineConstraintResolver()).To(controller => controller.Index());
```

##Default values

To specify a default value for a parameter use the method call expression. For example:

```csharp
routes.For<HomeController>()
    .CreateRoute("detail/{id}").To(controller => controller.Detail(1000));
```

For this route the default value for parameter id will be 1000. You can also specify the default value in the route template. For example:

```csharp
routes.For<HomeController>()
    .CreateRoute("detail/{id=1000}").To(controller => controller.Detail(0));
```

##Optional URL parameters

To make an URI parameter optional add a question mark to the route parameter. For example:

```csharp
routes.For<HomeController>()
    .CreateRoute("detail/{name?}").To(controller => controller.Detail(null));
```

##Alternate action names

To specify an alternate action name for a route, use the `WithActionName` method. The `WithActionName` method takes a single argument of type `string` which can be used to specify the action name, eg: `WithActionName("ContactMe")`. For example:

```csharp
routes.For<ContactController>()
    .CreateRoute("contactme").To(controller => controller.Index()).WithActionName("ContactMe");
```

Note: Portions of this text were copied from the [.NET Web Development and Tools Blog](http://blogs.msdn.com/b/webdev/archive/2013/10/17/attribute-routing-in-asp-net-mvc-5.aspx) and modified for Fluent Routing.
