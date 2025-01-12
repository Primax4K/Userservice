﻿global using BC = BCrypt.Net.BCrypt;
global using Domain.Repositories.Interfaces;
global using Mapster;
global using Microsoft.AspNetCore.Mvc;
global using Model.Entities.Users;
global using WebApi.Controllers.Abstract;
global using System.Security.Claims;
global using System.Text.Encodings.Web;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Domain.Repositories.Implementations;
global using Microsoft.AspNetCore.HttpOverrides;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging.Abstractions;
global using Model.Configurations;
global using WebApi.Services;
global using Microsoft.AspNetCore.Authorization;
global using Model.Entities.Addresses;
global using Model.Entities.Applications;
global using Model.Entities.Auth;
global using Model.Entities.Roles;
global using WebApi.Services.MiddleWare;
global using UserServiceApi7.DTOs.FullUser;
global using UserServiceApi7.DTOs.LoginUser;
global using UserServiceApi7.Models;
global using UserServiceApi7.DTOs.Gender;
global using UserServiceApi7.DTOs.Address;
global using UserServiceApi7.DTOs.LoginAttempt;
global using UserServiceApi7.DTOs.RegisteredUserRole;
global using UserServiceApi7.DTOs.Role;
global using UserServiceApi7.DTOs.ApplicationRole;
global using UserServiceApi7.DTOs.Country;
global using UserServiceApi7.DTOs.State;
global using UserServiceApi7.DTOs.Application;
global using UserServiceApi7.DTOs.RegisteredUser;
global using UserServiceApi7.DTOs.User;
global using UserServiceApi7.DTOs.Session;
global using UserServiceApi7.Extensions;