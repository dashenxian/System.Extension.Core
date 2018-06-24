﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace EInfrastructure.Core.HelpCommon.Serialization.JsonAdapter
{
    public class NewtonsoftJsonProvider : BaseJsonProvider
    {
        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public override string Serializer(object o, bool format = false)
        {
            using (StringWriter sw = new StringWriter())
            {
                JsonSerializer serializer = JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                serializer.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                JsonWriter jsonWriter;
                if (format)
                {
                    jsonWriter = new JsonTextWriter(sw)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                }
                else
                {
                    jsonWriter = new JsonTextWriter(sw);
                }

                using (jsonWriter)
                {
                    serializer.Serialize(jsonWriter, o);
                }
                return sw.ToString();
            }
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override object Deserialize(string s, Type type)
        {
            return JsonConvert.DeserializeObject(s, type);
        }
    }
}
