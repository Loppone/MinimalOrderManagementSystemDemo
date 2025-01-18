global using System.Text.Json.Serialization;

global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

global using Carter;
global using FluentResults;
global using FluentValidation;
global using Mapster;
global using MassTransit;
global using MediatR;

global using BuildingBlocks.Abstractions;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.Common.Errors;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Infrastructure;
global using BuildingBlocks.Messaging.Event;
global using BuildingBlocks.Models;

global using ProductService.Api.Application.Validation;
global using ProductService.Api.Application.Handlers;
global using ProductService.Api.Features.CreateCategory;
global using ProductService.Api.Features.CreateProduct;
global using ProductService.Api.Features.GetCategories;
global using ProductService.Api.Features.GetProductById;
global using ProductService.Api.Features.GetProducts;
global using ProductService.Api.Features.UpdateProduct;
global using ProductService.Api.Infrastructure;
global using ProductService.Api.Models;
