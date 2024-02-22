using Newtonsoft.Json;

namespace Server
{
    public class Response
    {
        public int State { get; set; }
        public object Data { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
