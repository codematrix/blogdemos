﻿using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using ExceptionIntercepts.CustomExceptions;

namespace ExceptionIntercepts.RealisticSamples
{
    public enum ExceptionCategoryType
    {
        /// <summary>
        /// Signifies that this exception is an expected exception such as Validations or NotFound, etc.
        /// </summary>
        Soft,

        /// <summary>
        /// Signifies that the exception is either an Unauthorized or Forbidden.
        /// </summary>
        Unauthorization,

        /// <summary>
        /// Signifies that the exception was not handled by the application.
        /// </summary>
        Unhandled
    }

    public class ExceptionCategory
    {
        public ExceptionCategoryType Category { get; internal set; }
        public HttpStatusCode HttpStatus { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public bool DeveloperMode { get; internal set; }
    }
    
    public class ExceptionCategorizer
    {
        public ExceptionCategory Categorizer(Exception exception)
        {
            bool developerMode = false;
            #if DEBUG
            developerMode = true;
            #endif

            var category = new ExceptionCategory() { DeveloperMode = developerMode };

            if (exception is ValidationException)
            {
                category.Category = ExceptionCategoryType.Soft;
                category.HttpStatus = HttpStatusCode.BadRequest;
                category.ErrorMessage = exception.Message;
                return category;
            }

            if (exception is NotFoundException)
            {
                category.Category = ExceptionCategoryType.Soft;
                category.HttpStatus = HttpStatusCode.NotFound;
                category.ErrorMessage = exception.Message;
                return category;
            }

            if (exception is UnauthorizedAccessException)
            {
                category.Category = ExceptionCategoryType.Unauthorization;
                category.HttpStatus = HttpStatusCode.Unauthorized;
                category.ErrorMessage = developerMode ? exception.Message : "Unauthorized access. Your request wad denied.";
                return category;
            }

            if (exception is ForbiddenAccessException)
            {
                category.Category = ExceptionCategoryType.Unauthorization;
                category.HttpStatus = HttpStatusCode.Forbidden;
                category.ErrorMessage = developerMode ? exception.Message : "You are unauthorized to access this resource.";
                return category;
            }

            // unhandled exception
            category.Category = ExceptionCategoryType.Unhandled;
            category.HttpStatus = HttpStatusCode.InternalServerError;
            category.ErrorMessage = developerMode ? exception.Message : "Oops, something went wrong with your request.";
            return category;
        }
    }
}
