using Common.Models;
using Microsoft.Win32;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Common.Commons
{
    [DataContract]
    public class ResponseService<T> : BaseResponseService
    {
        [DataMember]
        public bool status { get; set; }
        [DataMember]
        public int service_code = Constants.SERVICE_CODE;
        [DataMember]
        public int error_code { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public T data { get; set; }
        public HttpStatusCode status_code { get; set; }

        /// <summary>
        /// return response success with resource response
        /// </summary>
        /// <param name="data"></param>
        public ResponseService(T data)
        {
            this.status = true;
            this.message = string.Empty;
            this.data = data;
            this.status_code = HttpStatusCode.OK;
        }
        /// <summary>
        /// return response fail with resource response
        /// </summary>
        /// <param name="data"></param>
        public ResponseService(T data, string mess)
        {
            this.status = true;
            this.message = mess;
            this.data = data;
            this.status_code = HttpStatusCode.OK;
        }
        public ResponseService()
        {
            this.status = true;
            this.message = string.Empty;
        }

        /// <summary>
        /// return response error with message exception
        /// </summary>
        /// <param name="ex"></param>
        public ResponseService(Exception ex)
        {
            this.status = false;
            this.message = ex.Message;
            this.data = default;
            this.exception = ex;
            this.status_code = HttpStatusCode.InternalServerError;
            this.ready_commit = false;
        }
        public ResponseService(Exception ex, string error)
        {
            this.status = false;
            this.message = error;
            this.data = default;
            this.exception = ex;
            this.status_code = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// return response error with message
        /// </summary>
        /// <param name="ex"></param>
        public ResponseService(string mess)
        {
            this.status = false;
            this.message = mess;
            this.data = default;
            this.status_code = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        ///  return custom response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResponseService(bool status, string message, T data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
            this.status_code = HttpStatusCode.InternalServerError;
        }

        public ResponseService<T> BadRequest()
        {
            this.status_code = HttpStatusCode.BadRequest;
            this.status = false;
            return this;
        }
        public ResponseService<T> BadRequest(int errorCode = -1)
        {
            this.status_code = HttpStatusCode.BadRequest;
            this.status = false;
            this.error_code = errorCode != -1 ? errorCode : this.error_code;
            return this;
        }
        public ResponseService(T data, string mess, int errorCode)
        {
            this.status = true;
            this.message = mess;
            this.data = data;
            this.status_code = HttpStatusCode.OK;
            this.error_code = errorCode;
        }

        public ResponseService<T> Unauthorized()
        {
            this.status_code = HttpStatusCode.Unauthorized;
            this.status = false;
            return this;
        }
    }
}
