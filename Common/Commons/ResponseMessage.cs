using System.Runtime.Serialization;

namespace Common.Commons
{
    [DataContract]
    public class ResponseMessage<T> : ResponseService<T>
    {
        [DataMember]
        public string topic { get; set; }
        [DataMember]
        public string tenant_id { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string token { get; set; }
        public ResponseMessage() : base() { }
        public ResponseMessage(string topic, Exception ex) : base(ex)
        {
            this.topic = topic;
        }
        public ResponseMessage(string topic, T data, string token = null, string tenant_id = null, string email = null) : base(data)
        {
            this.token = token;
            this.topic = topic;
            this.tenant_id = tenant_id;
            this.email = email;
        }
        /// <summary>
        /// return response error with message exception
        /// </summary>
        /// <param name="ex"></param>
        public ResponseMessage(string topic, Exception ex)
        {
            this.status = false;
            this.message = ex.Message;
            this.data = default;
            this.exception = ex;
            this.topic = topic;
        }
        /// <summary>
        /// return response error with message exception
        /// </summary>
        /// <param name="ex"></param>
        public ResponseMessage(string topic, KafkaException ex, string email = null)
        {
            this.status = false;
            this.message = ex.Message;
            this.data = default;
            this.exception = ex;
            this.topic = topic;
            this.email = email;
        }
        /// <summary>
        /// return response error with message 
        /// </summary>
        /// <param name="ex"></param>
        public ResponseMessage(string topic, string message, string email = null)
        {
            this.status = false;
            this.message = message;
            this.data = default;
            this.topic = topic;
            this.email = email;
        }
    }
}
