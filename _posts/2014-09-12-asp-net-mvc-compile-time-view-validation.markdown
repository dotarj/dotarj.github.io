---
layout: post
title:  "ASP.NET MVC compile-time view validation"
date:   2014-09-12
categories: asp net aspnet mvc view validation
description: "When I started using ASP.NET MVC my biggest concern was the views; every syntax error in a view would result in a runtime exception. Fortunately, Microsoft has added support for compile-time view validation."
---

When I started using ASP.NET MVC my biggest concern was the views; every syntax error in a view would result in a runtime exception ("An exception of type 'Microsoft.CSharp.RuntimeBinder.RuntimeBinderException' occurred in System.Core.dll but was not handled in user code"), for example a renamed model property. When renaming a property in regular C# you have the option of updating all references, but this doesn't include references in ASP.NET MVC views. Of course, this could easily result in runtime errors.
Fortunately, Microsoft has added support for compile-time view validation. This feature has been around for quite some time (since ASP.NET MVC 3), but is not well documented and I eventually stumbled upon it by accident.
By default, this feature is disabled and enabling it requires manually modifying the project file, by following these steps:

1. Right click the ASP.NET MVC project in the solution explorer and select *Unload project*.
2. Right click the ASP.NET MVC project in the solution explorer and select *Edit Project.csproj*.
3. Locate the element ´MvcBuildViews´ in the /Project/PropertyGroup element and set it's value to ´true´ (if the project file does not contain the ´MvcBuildViews´ element it can be appended to the /Project/PropertyGroup element).
4. Save and close the project file.
5. Right click the ASP.NET MVC project in the solution explorer and select *Reload project*.

´´´xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="12.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <!-- Elements omitted -->
    <MvcBuildViews>true</MvcBuildViews>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <!-- Elements omitted -->
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <!-- Elements omitted -->
  </PropertyGroup>
</Project>
´´´

With the ´MvcBuildViews´ property enabled, views are compiled during project compilation and syntax errors will result in a build failure.

## Performance impact

There is a drawback; compiling views does have a significant impact on the build time. For release builds this is usually not a problem, but it can get quite frustrating for debug builds. For this, you could move the ´MvcBuildViews´ inside the /Project/PropertyGroup element with the Condition attribute set to release (eg. ´'$(Configuration)|$(Platform)' == 'Release|AnyCPU'´).

´´´xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="12.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <!-- Elements omitted -->
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <!-- Elements omitted -->
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <!-- Elements omitted -->
    <MvcBuildViews>true</MvcBuildViews>
  </PropertyGroup>
</Project>
´´´

Now, the views are only compiled during release builds and debug builds are not affected.
