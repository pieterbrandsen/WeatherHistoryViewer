﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface IHttpStatus
    {
        public HttpStatusResponse GetErrorModel(HttpStatusTypes errorType);
    }
    public class HttpStatus : IHttpStatus
    {
        public HttpStatusResponse GetErrorModel(HttpStatusTypes errorType)
        {
            var info = default(string);

            switch (errorType)
            {
                case HttpStatusTypes.not_found:
                    info = "User requested a resource which does not exist.";
                    break;
                case HttpStatusTypes.invalid_acces_key:
                    info = "User supplied an invalid access key or is missing one.";
                    break;
                case HttpStatusTypes.missing_query:
                    info = "An invalid (or missing) query value was specified.";
                    break;
                case HttpStatusTypes.no_results:
                    info = "The API request did not return any results.";
                    break;
                case HttpStatusTypes.invalid_unit:
                    info = "An invalid unit value was specified.";
                    break;
                case HttpStatusTypes.request_failed:
                    info = "API request has failed.";
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return new HttpStatusResponse()
            {
                StatusModel = new HttpStatusModel()
                {
                    Code = (short) errorType,
                    Type = errorType.ToString(),
                    Info = info
                }
            };
        }
    }
}