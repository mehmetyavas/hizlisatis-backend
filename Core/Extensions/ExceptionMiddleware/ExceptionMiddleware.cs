﻿using Core.Exceptions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = string.Empty;
            if (e.GetType() == typeof(AggregateException))
            {
                var ex = ((AggregateException)e).InnerExceptions;
                foreach (var item in ex)
                {
                    message = message + GetException(httpContext, item.InnerException) + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(message))
            {
                message = GetException(httpContext, e);
            }

            await httpContext.Response.WriteAsync(message);
        }

        private static string GetException(HttpContext httpContext, Exception e)
        {
            string message;
            if (e.GetType() == typeof(ValidationException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = (int)HttpStatusCode.TemporaryRedirect;
            }
            else if (e.GetType() == typeof(TransactionScopeException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(ArgumentException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            
            else
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return message;
        }
    }
}
