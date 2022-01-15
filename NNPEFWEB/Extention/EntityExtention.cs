
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NNPEFWEB.Extention
{
    public static class EntityExtention
    {
        public static ResponseModel<T> ToResponse<T>(this T entity, string message = null, bool success = false) where T : class
        {

            return new ResponseModel<T>()
            {
                Success = success,
                Message = message == null && !success ? "An error occured" : message,
                Data = entity,
            };
        }

        public static void AddRange<T>(this ConcurrentBag<T> @this, IEnumerable<T> toAdd)
        {
            foreach (var element in toAdd)
            {
                @this.Add(element);
            }
        }

        public static string FullMessage(this Exception ex) 
        {
            var exception = ex;
            var messages = new List<string>();
            while (exception != null)
            {
                messages.Add($" Message: {exception.Message} ---- Stacktrace: {exception.StackTrace} ---- Stacktrace: {exception.ToString()}");
                exception = exception.InnerException;
            } 

            return string.Join(", ", messages);
        }
    }
}
