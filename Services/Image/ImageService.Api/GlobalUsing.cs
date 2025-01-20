global using System.IO.Abstractions;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;

global using Carter;
global using FluentResults;
global using FluentValidation;
global using MassTransit;
global using MediatR;

global using BuildingBlocks.Abstractions;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.Infrastructure;
global using BuildingBlocks.Common.Errors;
global using BuildingBlocks.Helpers;
global using BuildingBlocks.Messaging.Enums;
global using BuildingBlocks.Messaging.Event;
global using BuildingBlocks.Messaging.Extensions;

global using ImageService.Api.Domain.Configuration;
global using ImageService.Api.Domain.Models;
global using ImageService.Api.Features.SaveImage;
global using ImageService.Api.Infrastructure;



