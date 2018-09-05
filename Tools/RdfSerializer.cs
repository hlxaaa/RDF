using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace Tools
{
    /// <summary>
    /// 深度序列化
    /// </summary>
    public static class RdfSerializer
    {
        private static MethodInfo _method;

        private static MethodInfo Method
        {
            get
            {
                return _method ?? (_method = Type.GetType("Tools.RdfSerializer", false, true)
                    .GetMethod("JsonToObj",
                        BindingFlags.Public | BindingFlags.IgnoreCase |
                        BindingFlags.Instance | BindingFlags.Static));
            }
        }
        static readonly JavaScriptSerializer Js = new JavaScriptSerializer();
        /// <summary>
        /// json反序列化成指定对象 来自JavaScriptSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return Js.Deserialize<T>(json);
        }
        /// <summary>
        /// json反序列化成指定对象 来自JavaScriptSerializer
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Js.Serialize(obj);
        }
        /// <summary>
        /// json反序列化成指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObj<T>(string json)
        {
            Type type = typeof(T);
            string typeName = type.Name.ToLower();
            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, json);
            }
            object val;
            if (type.IsValueType)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                switch (typeName)
                {
                    case "boolean":
                        bool defBool;
                        bool.TryParse(json, out defBool);
                        val = defBool;
                        break;
                    case "char":
                        char defChar;
                        char.TryParse(json, out defChar);
                        val = defChar;
                        break;
                    case "int32":
                        int defInt;
                        int.TryParse(json, out defInt);
                        val = defInt;
                        break;
                    case "int64":
                        long defLong;
                        long.TryParse(json, out defLong);
                        val = defLong;
                        break;
                    case "decimal":
                        decimal defDecimal;
                        decimal.TryParse(json, out defDecimal);
                        val = defDecimal;
                        break;
                    case "double":
                        double defDouble;
                        double.TryParse(json, out defDouble);
                        val = defDouble;
                        break;
                    case "datetime":
                        DateTime defDate;
                        DateTime.TryParse(json, out defDate);
                        val = defDate;
                        break;
                    case "guid":
                        Guid defGuid;
                        Guid.TryParse(json, out defGuid);
                        val = defGuid;
                        break;
                    default:
                        val = json;
                        break;
                }
                return (T)val;
            }
            if (typeName.Equals("string"))
            {
                val = json;
                return (T)val;
            }
            if (type.IsArray)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                return (T)JsonToArray(typeName, json);
            }
            if (type.IsGenericType)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                if (type.GetGenericArguments().Count() == 1)
                    return (T)JsonToList(type, json);
                if (type.GetGenericArguments().Count() == 2)
                    return (T)JsonToDic(type, json);
                ExceptionHandler("暂不支持json序列化成泛型类型:" + type.Name);
            }
            if (typeName.Equals("datatable"))
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                List<object> list = (List<object>)JsonToList(typeof(List<object>), json);
                val = ListToDataTable(list);
                return (T)val;
            }
            if (typeName.Equals("object"))
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    val = json;
                    return (T)val;
                }
                if (json[0] == '{')
                    val = JsonToDic(typeof(Dictionary<string, object>), json);
                else if (json[0] == '[')
                    val = JsonToList(typeof(List<object>), json);
                else
                    val = json;
                return (T)val;
            }
            if (type.IsClass)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                return JsonToPropertyOrField<T>(type, json);
            }
            val = json;
            return (T)val;
        }
        /// <summary>
        /// json转键值对
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private static object JsonToDic(Type type, string json)
        {
            if (json.ToLower().Equals("null"))
                return null;
            IDictionary dic = (IDictionary)Activator.CreateInstance(type);
            if (json.Equals("{}"))
                return dic;
            Type valType = type.GenericTypeArguments[1];
            MethodInfo mInfo = Method.MakeGenericMethod(valType);
            json = json.Substring(1, json.Length - 2);
            string typeName = valType.Name.ToLower();
            while (json != "")
            {
                int frist = json.IndexOf(':');
                if (frist == -1)
                    ExceptionHandler("json转键值对时检测到无效json缺失:");
                string key = FormartTo(json.Substring(0, frist));
                json = json.Substring(frist + 1, json.Length - frist - 1);

                if (typeName == "string" || valType.IsValueType)
                {
                    dic.Add(key, JsonToValueHandler(ref json, mInfo));
                    continue;
                }
                if (typeName == "object")
                {
                    dic.Add(key, JsonToObjectHandler(ref json, mInfo));
                    continue;
                }
                if (valType.IsGenericType)
                {
                    if (valType.GetGenericArguments().Count() == 1)
                        dic.Add(key, JsonToListHandler(ref json, mInfo));
                    else if (valType.GetGenericArguments().Count() == 2)
                        dic.Add(key, JsonToClassHandler(ref json, mInfo));
                    else
                        ExceptionHandler("暂不支持json序列化成键值对:" + typeName);
                    continue;
                }
                if (valType.IsClass)
                {
                    dic.Add(key, JsonToClassHandler(ref json, mInfo));
                    continue;
                }
                ExceptionHandler("暂不支持json序列化成键值对:" + typeName);
            }
            return dic;
        }
        /// <summary>
        /// json转数组
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private static object JsonToArray(string typeName, string json)
        {
            json = json.Replace("[", "").Replace("]", "");
            List<string> list = null;
            if (typeName.Equals("string[]"))
            {
                json = json.Substring(1, json.Length - 2);
                list = json.Split(new string[] { "\",\"" }, StringSplitOptions.None).ToList();
            }
            else
                list = json.Split(',').ToList();
            switch (typeName)
            {
                case "object[]":
                    object[] objList = new object[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        objList[i] = list[i];
                    }
                    return objList;
                case "string[]":
                    string[] strList = new string[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        strList[i] = list[i];
                    }
                    return strList;
                case "char[]":
                    char[] charList = new char[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        charList[i] = Convert.ToChar(FormartTo(list[i]));
                    }
                    return charList;
                case "int32[]":
                    int[] intList = new int[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        intList[i] = Convert.ToInt32(list[i]);
                    }
                    return intList;
                case "decimal[]":
                    decimal[] decimalList = new decimal[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        decimalList[i] = Convert.ToDecimal(list[i]);
                    }
                    return decimalList;
                case "double[]":
                    double[] doubleList = new double[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        doubleList[i] = Convert.ToDouble(list[i]);
                    }
                    return doubleList;
                case "datetime[]":
                    DateTime[] dateTimeList = new DateTime[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        dateTimeList[i] = Convert.ToDateTime(list[i]);
                    }
                    return dateTimeList;
                case "boolean[]":
                    bool[] boolList = new bool[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        boolList[i] = Convert.ToBoolean(list[i]);
                    }
                    return boolList;
                default:
                    ExceptionHandler("暂不支持json反序列化数组:" + typeName);
                    return null;
            }
        }
        /// <summary>
        /// json转泛型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private static object JsonToList(Type type, string json)
        {
            if (json.ToLower().Equals("null"))
                return null;
            IList list = (IList)Activator.CreateInstance(type);
            if (json.Equals("[]"))
                return list;
            json = json.Substring(1, json.Length - 2);
            Type valType = type.GenericTypeArguments[0];
            string typeName = valType.Name.ToLower();
            MethodInfo mInfo = Method.MakeGenericMethod(valType);
            while (json != "")
            {
                if (typeName == "string" || valType.IsValueType)
                {
                    list.Add(JsonToValueHandler(ref json, mInfo));
                    continue;
                }
                if (valType.IsGenericType)
                {
                    if (valType.GetGenericArguments().Count() == 1)
                        list.Add(JsonToListHandler(ref json, mInfo));
                    else if (valType.GetGenericArguments().Count() == 2)
                        list.Add(JsonToClassHandler(ref json, mInfo));
                    else
                        ExceptionHandler("暂不支持json序列化成泛型集合:" + typeName);
                    continue;
                }
                if (typeName == "object")
                {
                    list.Add(JsonToObjectHandler(ref json, mInfo));
                    continue;
                }
                if (typeName.Equals("datatable"))
                {
                    list.Add(JsonToListHandler(ref json, mInfo));
                    continue;
                }
                if (valType.IsClass)
                {
                    list.Add(JsonToClassHandler(ref json, mInfo));
                    continue;
                }
                ExceptionHandler("暂不支持json反序列化泛型集合:" + typeName);
            }
            return list;
        }
        /// <summary>
        /// json转对象属性或字段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private static T JsonToPropertyOrField<T>(Type type, string json)
        {
            T entity = (T)Activator.CreateInstance(type);
            Type enType = entity.GetType();
            json = json.Substring(1, json.Length - 2);
            while (json != "")
            {
                int frist = json.IndexOf(':');
                if (frist == -1)
                    ExceptionHandler("json转键值对时检测到无效json缺失:");
                string key = FormartTo(json.Substring(0, frist));
                json = json.Substring(frist + 1, json.Length - frist - 1);
                PropertyInfo pInfo = enType.GetProperty(key);
                if (pInfo == null)
                {
                    FieldInfo fInfo = enType.GetField(key);
                    if (fInfo == null)
                        ExceptionHandler("无效属性或字段" + key);
                    JsonToFieldHandler(fInfo, ref json, entity);
                }
                else
                    JsonToPropertyHandler(pInfo, ref json, entity);
            }
            return entity;
        }
        /// <summary>
        /// json转对象属性
        /// 根据json串和属性 取json最前面的值
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="json"></param>
        /// <param name="entity"></param>
        private static void JsonToPropertyHandler(PropertyInfo pInfo, ref string json, object entity)
        {
            MethodInfo mInfo = Method.MakeGenericMethod(pInfo.PropertyType);
            string typeName = pInfo.PropertyType.Name.ToLower();
            if (typeName == "string" || pInfo.PropertyType.IsValueType)
            {
                pInfo.SetValue(entity, JsonToValueHandler(ref json, mInfo));
                return;
            }
            if (pInfo.PropertyType.IsArray)
            {
                pInfo.SetValue(entity, JsonToListHandler(ref json, mInfo));
                return;
            }
            if (pInfo.PropertyType.IsGenericType)
            {
                if (pInfo.PropertyType.GetGenericArguments().Count() == 1)
                    pInfo.SetValue(entity, JsonToListHandler(ref json, mInfo));

                if (pInfo.PropertyType.GetGenericArguments().Count() == 2)
                    pInfo.SetValue(entity, JsonToClassHandler(ref json, mInfo));
                return;
            }
            if (typeName == "datatable")
            {
                ExceptionHandler("暂不支持json反序列DataTable:" + typeName);
            }
            if (typeName == "object")
            {
                pInfo.SetValue(entity, JsonToObjectHandler(ref json, mInfo));
                return;
            }
            if (pInfo.PropertyType.IsClass)
            {
                pInfo.SetValue(entity, JsonToClassHandler(ref json, mInfo));
                return;
            }
            ExceptionHandler("暂不支持json反序列对象属性:" + typeName);
        }
        /// <summary>
        /// json转对象字段
        /// 根据json串和字段 取json最前面的值
        /// </summary>
        /// <param name="fInfo"></param>
        /// <param name="json"></param>
        /// <param name="entity"></param>
        private static void JsonToFieldHandler(FieldInfo fInfo, ref string json, object entity)
        {
            MethodInfo mInfo = Method.MakeGenericMethod(fInfo.FieldType);
            string typeName = fInfo.FieldType.Name.ToLower();
            if (typeName == "string" || fInfo.FieldType.IsValueType)
            {
                fInfo.SetValue(entity, JsonToValueHandler(ref json, mInfo));
                return;
            }
            if (fInfo.FieldType.IsArray)
            {
                fInfo.SetValue(entity, JsonToListHandler(ref json, mInfo));
                return;
            }
            if (fInfo.FieldType.IsGenericType)
            {
                if (fInfo.FieldType.GetGenericArguments().Count() == 1)
                    fInfo.SetValue(entity, JsonToListHandler(ref json, mInfo));

                if (fInfo.FieldType.GetGenericArguments().Count() == 2)
                    fInfo.SetValue(entity, JsonToClassHandler(ref json, mInfo));
                return;
            }
            if (typeName == "datatable")
            {
                fInfo.SetValue(entity, JsonToListHandler(ref json, mInfo));
                return;
            }
            if (typeName == "object")
            {
                fInfo.SetValue(entity, JsonToObjectHandler(ref json, mInfo));
                return;
            }
            if (fInfo.FieldType.IsClass)
            {
                fInfo.SetValue(entity, JsonToClassHandler(ref json, mInfo));
                return;
            }
            ExceptionHandler("暂不支持json反序列对象字段:" + typeName);
        }
        /// <summary>
        /// json是集合的处理
        /// 根据json串 取json最前面的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mInfo"></param>
        /// <returns></returns>
        private static object JsonToListHandler(ref string json, MethodInfo mInfo)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                json = "";
                return null;
            }
            int jsonLength = json.Length;
            if (jsonLength >= 4 && json.Substring(0, 4).ToLower().Equals("null"))
            {
                json = jsonLength > 4 ? json.Substring(5, jsonLength - 5) : "";
                return null;
            }
            int count = 0;
            for (int i = 0; i < jsonLength; i++)
            {
                if (json[i] == '[')
                    count += 1;
                else if (json[i] == ']')
                    count -= 1;
                if (count == 0)
                {
                    count = i;
                    break;
                }
            }
            if (count == 0)
                ExceptionHandler("json转集合时检测到无效json缺失]");
            string val = json.Substring(0, count + 1);
            json = val == json ? "" : json.Substring(count + 2, jsonLength - count - 2);

            object obj = mInfo.Invoke(null, new object[] { val });
            return obj;
        }
        /// <summary>
        /// json是object的处理
        /// 根据json串 取json最前面的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mInfo"></param>
        /// <returns></returns>
        private static object JsonToObjectHandler(ref string json, MethodInfo mInfo)
        {
            if (string.IsNullOrWhiteSpace(json) || json.ToLower().Equals("null"))
            {
                json = "";
                return null;
            }
            char fristChar = json[0];
            if (fristChar == '{')
                return JsonToClassHandler(ref json, mInfo);
            if (fristChar == '[')
                return JsonToListHandler(ref json, mInfo);
            return JsonToValueHandler(ref json, mInfo);
        }
        /// <summary>
        /// json是键值对的处理
        /// 根据json串 取json最前面的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mInfo"></param>
        /// <returns></returns>
        private static object JsonToClassHandler(ref string json, MethodInfo mInfo)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                json = "";
                return null;
            }
            int jsonLength = json.Length;
            if (jsonLength >= 4 && json.Substring(0, 4).ToLower().Equals("null"))
            {
                json = jsonLength > 4 ? json.Substring(5, jsonLength - 5) : "";
                return null;
            }

            int count = 0;
            for (int i = 0; i < jsonLength; i++)
            {
                if (json[i] == '{')
                    count += 1;
                else if (json[i] == '}')
                    count -= 1;
                if (count == 0)
                {
                    count = i;
                    break;
                }
            }
            if (count == 0)
                ExceptionHandler("json转键值对时检测到无效json缺失{");
            string val = json.Substring(0, count + 1);
            json = val == json ? "" : json.Substring(count + 2, jsonLength - count - 2);
            object obj = mInfo.Invoke(null, new object[] { val });
            return obj;
        }
        /// <summary>
        /// json是string或值类型的处理
        /// 根据json串 取json最前面的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mInfo"></param>
        /// <returns></returns>
        private static object JsonToValueHandler(ref string json, MethodInfo mInfo)
        {
            if (string.IsNullOrWhiteSpace(json))
                return json;
            if (json == "\"\"")
            {
                json = "";
                return json;
            }
            string val;
            string indexOf = ",";
            int indexCount = 0;
            if (json[0].Equals('"'))
            {
                indexOf = "\",\"";
                indexCount = 1;
            }
            int valIndex = json.IndexOf(indexOf, StringComparison.Ordinal);
            if (valIndex != -1)
            {
                val = json.Substring(0, valIndex + indexCount);
                json = json.Substring(valIndex + 1 + indexCount, json.Length - valIndex - 1 - indexCount);
            }
            else
            {
                val = json;
                json = "";
            }
            if (val.ToLower().Equals("null"))
                return null;
            object obj = mInfo.Invoke(null, new object[] { FormartTo(val) });
            return obj;
        }
        /// <summary>
        /// 深度序列化json
        /// </summary>
        /// <param name="obj">数据源</param>
        /// <param name="dateFormat">日期格式，默认yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string ObjToJson(object obj, string dateFormat = "yyyy-MM-dd HH:mm:ss")
        {
            if (obj == null)
                return "null";
            StringBuilder sb = new StringBuilder();
            Type type = obj.GetType();
            string typeName = type.Name.ToLower();

            if (type.IsEnum)
            {
                sb.Append((int)obj);
                return sb.ToString();
            }
            if (type.IsValueType)
            {
                switch (typeName)
                {
                    case "boolean":
                        sb.Append(obj.ToString().ToLower());
                        break;
                    case "timespan":
                    case "char":
                    case "guid":
                        sb.Append("\"" + obj + "\"");
                        break;
                    case "datetime":
                        sb.Append("\"" + Convert.ToDateTime(obj).ToString(dateFormat) + "\"");
                        break;
                    default:
                        sb.Append(obj);
                        break;
                }
                return sb.ToString();
            }
            if (typeName.Equals("string"))
            {
                sb.Append("\"" + Formart(obj.ToString()) + "\"");
                return sb.ToString();
            }
            if (type.IsArray)
            {
                sb.Append(ArrayToJson(typeName, obj, dateFormat));
                return sb.ToString();
            }
            if (type.IsGenericType)
            {
                if (type.GetGenericArguments().Count() == 1)
                {
                    sb.Append(ListToJson(obj));
                    return sb.ToString();
                }
                if (type.GetGenericArguments().Count() == 2)
                {
                    sb.Append(DicToJson(obj));
                    return sb.ToString();
                }
                if (type.IsClass)
                {
                    sb.Append(PropertyOrFieldToJson(type, obj));
                    return sb.ToString();
                }
                ExceptionHandler("不支持" + type.Name + "的序列化");
            }
            if (typeName.Equals("datatable"))
            {
                sb.Append(DataTableToJson(obj, dateFormat));
                return sb.ToString();
            }
            if (type.IsClass)
            {
                sb.Append(PropertyOrFieldToJson(type, obj));
                return sb.ToString();
            }
            return sb.ToString();
        }
        /// <summary>
        /// 数组转json
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="obj"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        private static string ArrayToJson(string typeName, object obj, string dateFormat)
        {
            StringBuilder sb = new StringBuilder();
            switch (typeName)
            {
                case "object[]":
                    object[] objArr = (object[])obj;
                    sb.Append(string.Join(",", objArr.ToList().ConvertAll(item => "\"" + Formart(item.ToString()) + "\"")));
                    break;
                case "char[]":
                    char[] charArr = (char[])obj;
                    sb.Append(string.Join(",", charArr.ToList().ConvertAll(item => "\"" + Formart(item.ToString()) + "\"")));
                    break;
                case "string[]":
                    string[] strArr = (string[])obj;
                    sb.Append(string.Join(",", strArr.ToList().ConvertAll(item => "\"" + Formart(item.ToString()) + "\"")));
                    break;
                case "int32[]":
                    int[] intArr = (int[])obj;
                    sb.Append(string.Join(",", intArr.ToList().ConvertAll(item => item)));
                    break;
                case "decimal[]":
                    decimal[] decArr = (decimal[])obj;
                    sb.Append(string.Join(",", decArr.ToList().ConvertAll(item => item)));
                    break;
                case "double[]":
                    double[] douArr = (double[])obj;
                    sb.Append(string.Join(",", douArr.ToList().ConvertAll(item => item)));
                    break;
                case "datetime[]":
                    DateTime[] dateArr = (DateTime[])obj;
                    sb.Append(string.Join(",", dateArr.ToList().ConvertAll(item => "\"" + item.ToString(dateFormat) + "\"")));
                    break;
                case "boolean[]":
                    bool[] boolArr = (bool[])obj;
                    sb.Append(string.Join(",", boolArr.ToList().ConvertAll(item => item.ToString().ToLower())));
                    break;
                default:
                    ExceptionHandler("暂不支持数组序列化Json:" + typeName);
                    break;
            }
            return "[" + sb + "]";
        }
        /// <summary>
        /// 泛型转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string ListToJson(object obj)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable list = obj as IEnumerable;
            if (list == null)
                return "[]";
            bool index = false;
            foreach (var item in list)
            {
                if (index)
                    sb.Append(",");
                sb.Append(ObjToJson(item));
                index = true;
            }
            return "[" + sb + "]";
        }
        /// <summary>
        /// 键值对转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string DicToJson(object obj)
        {
            StringBuilder sb = new StringBuilder();
            IDictionary dic = obj as IDictionary;
            if (dic == null)
                return "{}";
            bool index = false;
            foreach (var item in dic.Keys)
            {
                if (!index && item.GetType().Name.ToLower() != "string")
                    ExceptionHandler(obj.GetType().Name + "序列化时，键必须为String类型");

                if (index)
                    sb.Append(",");
                sb.Append("\"" + item + "\":" + ObjToJson(dic[item]));
                index = true;
            }
            return "{" + sb + "}";
        }
        /// <summary>
        /// 对象属性或字段转json
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string PropertyOrFieldToJson(Type type, object obj)
        {
            StringBuilder sb = new StringBuilder();
            bool index = false;
            //公共属性
            PropertyInfo[] pInfo = type.GetProperties();
            int pCount = pInfo.Count();
            for (int j = 0; j < pCount; j++)
            {
                var item = pInfo[j];
                RdfNonSerialized ser = PropertyIsSerializer(item);
                if (ser != null && ser.Serialized == false)
                    continue;

                if (index)
                    sb.Append(",");
                sb.Append("\"" + GetJsonName(item, ser) + "\":" + GetJsonValue(item.GetValue(obj), ser));
                index = true;
            }
            //公共字段
            FieldInfo[] fInfo = type.GetFields();
            int fCount = fInfo.Count();
            for (int j = 0; j < fCount; j++)
            {
                var item = fInfo[j];
                RdfNonSerialized ser = FieldIsSerializer(item);
                if (ser != null && ser.Serialized == false)
                    continue;

                if (index)
                    sb.Append(",");
                sb.Append("\"" + GetJsonName(item, ser) + "\":" + GetJsonValue(item.GetValue(obj), ser));
                index = true;
            }
            return "{" + sb + "}";
        }
        private static string GetJsonName(MemberInfo info, RdfNonSerialized serizlized)
        {
            if (serizlized == null || string.IsNullOrWhiteSpace(serizlized.Filed))
                return info.Name;
            return serizlized.Filed;
        }
        private static string GetJsonValue(object obj, RdfNonSerialized serizlized)
        {
            if (serizlized == null || string.IsNullOrWhiteSpace(serizlized.Format))
                return ObjToJson(obj);
            return ObjToJson(obj, serizlized.Format);
        }
        /// <summary>
        /// 验证属性是否被序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static RdfNonSerialized PropertyIsSerializer(PropertyInfo item)
        {
            //如果是索引器也不序列化
            if (item.GetIndexParameters().Length > 0)
                return new RdfNonSerialized();
            for (int i = 0; i < item.GetCustomAttributes(false).Count(); i++)
            {
                if (item.GetCustomAttributes(false)[i] is RdfNonSerialized)
                    return (RdfNonSerialized)item.GetCustomAttributes(false)[i];
            }
            return null;
        }
        /// <summary>
        /// 验证字段是否被序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static RdfNonSerialized FieldIsSerializer(FieldInfo item)
        {
            for (int i = 0; i < item.GetCustomAttributes(false).Count(); i++)
            {
                if (item.GetCustomAttributes(false)[i] is RdfNonSerialized)
                    return (RdfNonSerialized)item.GetCustomAttributes(false)[i];
            }
            return null;
        }
        /// <summary>
        /// list转datatable
        /// </summary>
        /// <returns></returns>
        private static DataTable ListToDataTable(List<object> list)
        {
            DataTable dt = new DataTable { TableName = "DefaultName" };
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Dictionary<string, object> headDic = (Dictionary<string, object>)list[i];
                    if (i == 0)
                    {
                        foreach (string coulmn in headDic.Keys)
                        {
                            dt.Columns.Add(coulmn);
                        }
                    }
                    DataRow row = dt.NewRow();
                    foreach (string coulmn in headDic.Keys)
                    {
                        row[coulmn] = headDic[coulmn];
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        /// <summary>
        /// DataTable属性转json
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        private static string DataTableToJson(object obj, string dateFormat)
        {
            var table = (DataTable)obj;
            if (table == null || table.Rows.Count == 0)
                return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j > 0)
                        sb.Append(",");
                    sb.Append("\"" + table.Columns[j].ColumnName + "\":");
                    if (table.Rows[i][j] == DBNull.Value)
                    {
                        sb.Append("null");
                        continue;
                    }
                    sb.Append(ObjToJson(table.Rows[i][j], dateFormat));
                }
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 序列化时转义符的处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string Formart(string obj)
        {
            return obj.Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t").Replace("'", "\\u0027");
        }
        /// <summary>
        /// 反序列化时转义符的处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string FormartTo(string obj)
        {
            return obj.Replace("\"", "").Replace("\\\\", "\\").Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\u0027", "'");
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="message"></param>
        private static void ExceptionHandler(string message)
        {
            Exception ex = new Exception(message);
            RdfLog.WriteException(ex, "ExceptionHandler");
            throw ex;
        }
    }
}
