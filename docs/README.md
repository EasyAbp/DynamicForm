# DynamicForm

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FDynamicForm%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.DynamicForm.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.DynamicForm.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.DynamicForm.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.DynamicForm.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/DynamicForm?style=social)](https://www.github.com/EasyAbp/DynamicForm)

An ABP module helps users to define and use dynamic forms at runtime.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.DynamicForm.Application
    * EasyAbp.DynamicForm.Application.Contracts
    * EasyAbp.DynamicForm.Domain
    * EasyAbp.DynamicForm.Domain.Shared
    * EasyAbp.DynamicForm.EntityFrameworkCore
    * EasyAbp.DynamicForm.HttpApi
    * EasyAbp.DynamicForm.HttpApi.Client
    * EasyAbp.DynamicForm.Web

2. Add `DependsOn(typeof(DynamicFormXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Add `builder.ConfigureDynamicForm();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

4. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

Todo.

## Roadmap

Todo.
