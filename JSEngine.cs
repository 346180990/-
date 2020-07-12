using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SpiderMusic.model;
using Nancy.Json;
using System.Collections;

namespace SpiderMusic
{
    class JSEngine
    {
        public static string getJSEngine() {
            var phone = new phone
            {
                Phone = "13201661424",
                Password = "asdliuyang123",
                Countrycode = "86",
                RememberLogin = "true"
            };
            JavaScriptSerializer json = new JavaScriptSerializer();
            string phonejson = json.Serialize(phone);
            //获取本地core地址
            string ScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "core_.js");
            //获取js引擎实例
            var switcher = JsEngineSwitcher.Current;
            switcher.EngineFactories.Add(new ChakraCoreJsEngineFactory());
            switcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;
            IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine();
            engine.ExecuteFile(ScriptPath, Encoding.UTF8);
            try
            {
                var result = engine.CallFunction("myFunc", phonejson);
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
            return "1";
        }
    }
}
