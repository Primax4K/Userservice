﻿global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.Contracts;
global using System.Net.Http.Json;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.Json.Serialization;
global using UserServiceApi7.Auth.Services;
global using UserServiceApi7.Auth.StateProvider;
global using UserServiceApi7.DTOs.Address;
global using UserServiceApi7.DTOs.Application;
global using UserServiceApi7.DTOs.ApplicationRole;
global using UserServiceApi7.DTOs.Country;
global using UserServiceApi7.DTOs.Gender;
global using UserServiceApi7.DTOs.LoginAttempt;
global using UserServiceApi7.DTOs.LoginUser;
global using UserServiceApi7.DTOs.RegisteredUser;
global using UserServiceApi7.DTOs.RegisteredUserRole;
global using UserServiceApi7.DTOs.Role;
global using UserServiceApi7.DTOs.Session;
global using UserServiceApi7.DTOs.State;
global using UserServiceApi7.DTOs.User;
global using UserServiceApi7.ErrorHandling;
global using UserServiceApi7.Exceptions;
global using UserServiceApi7.Extensions;
global using UserServiceApi7.Implementations;
global using UserServiceApi7.Implementations.Abstract;
global using UserServiceApi7.Interfaces;
global using UserServiceApi7.Models;
global using UserServiceApi7.Providers;
global using UserServiceApi7.DTOs.FullUser;
global using UserServiceApi7.Attributes;