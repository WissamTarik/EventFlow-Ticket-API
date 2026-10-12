using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Common
{
    public sealed record Error(string code,string description,ErrorTypes errorType=ErrorTypes.Failure)
    {
        public static Error None() => new(string.Empty, string.Empty, ErrorTypes.None);
        public static Error Faliure(string code= "General.Failure", string description= "General error has occured") => new (code,description,ErrorTypes.Failure);
        public static Error Forbidden(string code= "General.Forbidden", string description= "A Forbidden error has occured") => new(code,description, ErrorTypes.Forbidden);
        public static Error NotFound(string code="General.NotFound", string description="Not found error has occured") => new(code,description, ErrorTypes.NotFound);
        public static Error UnAuthorized( string code="General.UnAuthorized",string description= "an UnAuthorized error has occured") => new(code,description, ErrorTypes.UnAuthorized);
        public static Error Conflict(string code="General.Conflict",string description= "conflict error has occured") => new(code,description, ErrorTypes.Conflict);
        public static Error Validation(string code="General.Validation",string description= "A Validation error has occured") => new(code,description, ErrorTypes.Validation);
        public static Error InvalidCredential(string code="General.InvalidCredential", string description="An Invalid credential  error has occured") => new(code,description, ErrorTypes.InvalidCredential);

    }
    public enum ErrorTypes
    {
        None,
        Forbidden,
        Validation,
        InvalidCredential,
        NotFound,
        UnAuthorized,
        Failure,
        Conflict
    }
}
